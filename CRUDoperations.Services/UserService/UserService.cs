using Common.Dto;
using Common.Enums;
using Common.Notification.Mail;
using Common.PasswordHash;
using CRUDoperations.DataModel.Entities;
using CRUDoperations.Dto.DTOs;
using CRUDoperations.IRepositories.UnitOfWork;
using CRUDoperations.IServices.IService;
using CRUDoperations.Repositories.UnitOfWork;
using CRUDoperations.Services.Base;
using CRUDoperations.Services.CustomExceptions;
using CRUDoperations.Services.Localization;
using CRUDoperations.Services.Setting;
using Mapster;
using MapsterMapper;
using System.Security.Cryptography;

namespace CRUDoperations.Services.UserService
{
    public class UserService : BaseService, IUserService
    {
        private readonly IPasswordHash PasswordHash;
        private IMailSender MailSender;
        private MailSettings MailSetting;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILocalizationService localization,
            IPasswordHash passwordHash, IMailSender mailSender, MailSettings mailSetting) : base(unitOfWork, mapper, localization)
        {
            PasswordHash = passwordHash;
            MailSender = mailSender;
            MailSetting = mailSetting;
        }
        #region Public Methods
        public async Task<ResponseDto<EmptyResponseDto>> Add(UserDto request)
        {
            ResponseDto<EmptyResponseDto> response = ValidateUser(request);
            if (response.Status is ResponseStatus.Error)
                return response;

            User user = Mapper.Map<User>(request);
            user.Password = PasswordHash.CreateHash(request.Password);

            UnitOfWork.UserRepository.CreateAsyn(user);

            Mail mail = new()
            {
                Subject = MailSetting.Subject,
                To = request.Email,
                Body = string.Format(MailSetting.Body, request.Email, request.Password),
                MailStatusId = (int)MailStatusEnum.New,
                MailTypeId = (int)MailTypeEnum.WelcomeMail,
            };
            (MailDto mailDto, MailSettingDto mailSetting) = PrepareMailDtos(mail);
            await MailSender.SendMail(mailDto, mailSetting);

            UnitOfWork.MailRepository.CreateAsyn(mail);

            if(await UnitOfWork.CommitAsync() > default(int))
            {
                
                return response.GetSuccessResponse(Localization.GeneralSuccess);
            }

            return response.GetErrorResponse(Localization.GeneralError);
        }
        public async Task<ResponseDto<EmptyResponseDto>> Update(UserDto userDto)
        {
            ResponseDto<EmptyResponseDto> response = ValidateUser(userDto, true);
            if (response.Status is ResponseStatus.Error)
                return response;

            User user = await UnitOfWork.UserRepository.FirstOrDefaultAsync(user => user.Id == userDto.Id);
            if (user is null)
                return response.GetErrorResponse("User not found !");

            MapUser(user, userDto);
            UnitOfWork.UserRepository.Update(user);

            return await UnitOfWork.CommitAsync() > default(int)
                ? response.GetSuccessResponse()
                : response.GetErrorResponse();
        }

        public async Task<ResponseDto<EmptyResponseDto>> Delete(int id)
        {
            ResponseDto < EmptyResponseDto > response = new ResponseDto<EmptyResponseDto>().GetErrorResponse();
            if (id is default(int))
                return response.GetErrorResponse("Invalid Request");

            User user = await UnitOfWork.UserRepository.FirstOrDefaultAsync(user => user.Id == id);
            if (user is null)
                return response.GetErrorResponse("User not found");

            UnitOfWork.UserRepository.Delete(user);

            return await UnitOfWork.CommitAsync() > default(int)
                            ? response.GetSuccessResponse()
                            : response.GetErrorResponse();
        }

        public async Task<ResponseDto<UserDto>> GetById(int id)
        {
            ResponseDto<UserDto> response = new ResponseDto<UserDto>().GetErrorResponse();
            if (id is default(int))
                return response.GetErrorResponse("Invalid Request");

            User user = await UnitOfWork.UserRepository.FirstOrDefaultAsync(user => user.Id == id);
            if (user is null)
                return response.GetErrorResponse("User not found");

            return response.GetSuccessResponse(MapUserDto(user));
        }
        public async Task<ResponseDto<List<UserDto>>> GetAll()
        {
            ResponseDto<List<UserDto>> response = new ResponseDto<List<UserDto>>().GetErrorResponse();
            List<User> users = await UnitOfWork.UserRepository.GetAllAsync();
            if (users?.Count <= default(int))
                return response.GetErrorResponse("No data found");

            return response.GetSuccessResponse(MapUserDtos(users));
        }

        #endregion

        #region Private Methods
        private ResponseDto<EmptyResponseDto> ValidateUser(UserDto userDto, bool isUpdate = false)
        {
            ResponseDto<EmptyResponseDto> response = new ResponseDto<EmptyResponseDto>().GetErrorResponse(Localization.GeneralError);
            if (userDto is null)
                return response.GetErrorResponse("Invalid Request");

            if (string.IsNullOrWhiteSpace(userDto.FirstName))
                throw new NameRequiredException("Please enter first name");

            if (string.IsNullOrWhiteSpace(userDto.LastName))
                throw new NameRequiredException("Please enter last name");

            if (string.IsNullOrWhiteSpace(userDto.Email))
                throw new InvalidRequestException("Please enter email");

            if (string.IsNullOrWhiteSpace(userDto.UserName))
                throw new NameRequiredException("Please enter user name");

            if (string.IsNullOrWhiteSpace(userDto.Password) && !isUpdate)
                throw new NameRequiredException("Please enter password");

            //if (Regex.IsMatch(userDto.Email, @"^[^\s@]+@([^\s@.,]+\.)+[^\s@.,]{2,}$"))
            //    throw new NameRequiredException("Please enter valid email");

            return response.GetSuccessResponse();
        }
        private static User MapUser(UserDto userDto)
        {
            return new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                UserName = userDto.UserName,
                Password = HashPassword(userDto.Password)
            };
        }
        private static void MapUser(User user, UserDto userDto)
        {
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            if (!string.IsNullOrWhiteSpace(userDto.Password))
                user.Password = HashPassword(userDto.Password);
        }
        private static UserDto MapUserDto(User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password
            };
        }
        private static List<UserDto> MapUserDtos(List<User> users)
        {
            List<UserDto> usersDto = new();
            foreach (User user in users)
            {
                usersDto.Add(new UserDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName
                });
            }
            return usersDto;
        }
        private static string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
        private (MailDto, MailSettingDto) PrepareMailDtos(Mail mail)
        {
            MailDto mailDto = new()
            {
                Id = mail.MailId,
                MailTo = new List<string> { mail.To },
                Subject = mail.Subject,
                Body = mail.Body,
                IsBodyHtml = false,
            };
            MailSettingDto mailSetting = new()
            {
                EmailAddress = MailSetting.EmailAddress,
                Username = MailSetting.Username,
                Password = MailSetting.Password,
                SmtpServer = MailSetting.SmtpServer,
                EmailSmtpPort = MailSetting.EmailSmtpPort,
                SmtpTimeOut = MailSetting.SmtpTimeOut
            };

            return (mailDto, mailSetting);
        }
        #endregion
    }
}
