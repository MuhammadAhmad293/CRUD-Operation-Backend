using Common.Enums;
using Common.ExtensionMethods;
using CRUDoperations.DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDoperations.Repositories.Context
{
    public static class InitialDataSeeding
    {
        public static void SeedInitialData(this ModelBuilder modelBuilder)
        {
            #region MailStatus
            modelBuilder.Entity<MailStatus>().HasData(
                        new MailStatus
                        {
                            MailStatusId = (int)MailStatusEnum.New,
                            ArName = MailStatusEnum.New.GetDisplayName(),
                            EnName = MailStatusEnum.New.GetDisplayShortName(),
                            ArDescription = MailStatusEnum.New.GetDisplayName(),
                            EnDescription = MailStatusEnum.New.GetDisplayShortName(),
                            CreationTime = new DateTime(2023, 01, 1)
                        },
                        new MailStatus
                        {
                            MailStatusId = (int)MailStatusEnum.Sent,
                            ArName = MailStatusEnum.Sent.GetDisplayName(),
                            EnName = MailStatusEnum.Sent.GetDisplayShortName(),
                            ArDescription = MailStatusEnum.Sent.GetDisplayName(),
                            EnDescription = MailStatusEnum.Sent.GetDisplayShortName(),
                            CreationTime = new DateTime(2023, 01, 1)
                        },
                        new MailStatus
                        {
                            MailStatusId = (int)MailStatusEnum.Processing,
                            ArName = MailStatusEnum.Processing.GetDisplayName(),
                            EnName = MailStatusEnum.Processing.GetDisplayShortName(),
                            ArDescription = MailStatusEnum.Processing.GetDisplayName(),
                            EnDescription = MailStatusEnum.Processing.GetDisplayShortName(),
                            CreationTime = new DateTime(2023, 01, 1)
                        },
                        new MailStatus
                        {
                            MailStatusId = (int)MailStatusEnum.Failed,
                            ArName = MailStatusEnum.Failed.GetDisplayName(),
                            EnName = MailStatusEnum.Failed.GetDisplayShortName(),
                            ArDescription = MailStatusEnum.Failed.GetDisplayName(),
                            EnDescription = MailStatusEnum.Failed.GetDisplayShortName(),
                            CreationTime = new DateTime(2023, 01, 1)
                        });
            #endregion

            #region MailType

            modelBuilder.Entity<MailType>().HasData(
                       new MailType
                       {
                           MailTypeId = (int)MailTypeEnum.ForgetPassword,
                           ArName = MailTypeEnum.ForgetPassword.GetDisplayName(),
                           EnName = MailTypeEnum.ForgetPassword.GetDisplayShortName(),
                           ArDescription = MailTypeEnum.ForgetPassword.GetDisplayName(),
                           EnDescription = MailTypeEnum.ForgetPassword.GetDisplayShortName(),
                           CreationTime = new DateTime(2023, 01, 1)
                       },
                       new MailType
                       {
                           MailTypeId = (int)MailTypeEnum.WelcomeMail,
                           ArName = MailTypeEnum.WelcomeMail.GetDisplayName(),
                           EnName = MailTypeEnum.WelcomeMail.GetDisplayShortName(),
                           ArDescription = MailTypeEnum.WelcomeMail.GetDisplayName(),
                           EnDescription = MailTypeEnum.WelcomeMail.GetDisplayShortName(),
                           CreationTime = new DateTime(2023, 01, 1)
                       },
                       new MailType
                       {
                           MailTypeId = (int)MailTypeEnum.VerificationMail,
                           ArName = MailTypeEnum.VerificationMail.GetDisplayName(),
                           EnName = MailTypeEnum.VerificationMail.GetDisplayShortName(),
                           ArDescription = MailTypeEnum.VerificationMail.GetDisplayName(),
                           EnDescription = MailTypeEnum.VerificationMail.GetDisplayShortName(),
                           CreationTime = new DateTime(2023, 01, 1),
                       }
                       );

            #endregion

        }
    }
}
