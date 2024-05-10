namespace Hexagonal.Services
{
    public interface IHashService
    {
        string HashValue(string value);
        bool VerifyValue(string enteredValue, string storedHash);
    }
}