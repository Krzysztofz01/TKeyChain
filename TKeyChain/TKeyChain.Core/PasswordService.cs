using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TKeyChain.Core.Abstraction;

namespace TKeyChain.Core
{
    public class PasswordService : IPasswordService
    {
        // Password rules:
        // Greater than 12 characters
        // One or more uppercase letters
        // One or more lowercase letters
        // One or more numeric values
        // One or more special character
        // About 30 years to brute force
        private const string _regexPattern = @"(?=^.{12,}$)(?=.*\d)(?=.*[!@#$%^&*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

        private const int _passwordLength = 16;
        private const byte _charRangeStart = 33;
        private const byte _charRangeEnd = 125;

        public bool CheckPassword(string password)
        {
            var regex = new Regex(_regexPattern);

            if (regex.IsMatch(password)) return true;

            return false;
        }

        public string GeneratePassword()
        {
            var sb = new StringBuilder(_passwordLength);

            for (int i = 0; i < _passwordLength; i++)
            {
                sb.Append((char)RandomNumberGenerator.GetInt32(_charRangeStart, _charRangeEnd));
            }

            return sb.ToString();
        }
    }
}
