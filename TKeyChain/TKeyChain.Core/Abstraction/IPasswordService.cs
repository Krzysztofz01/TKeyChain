namespace TKeyChain.Core.Abstraction
{
    public interface IPasswordService
    {
        string GeneratePassword();
        bool CheckPassword(string password);
    }
}
