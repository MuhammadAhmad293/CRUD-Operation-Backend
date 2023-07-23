namespace Common.PasswordHash
{
    public interface IPasswordHash
    {
        string CreateHash(string password);
        bool ValidatePassword(string password, string hashedPassword);
    }
}
