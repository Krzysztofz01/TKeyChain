using System;
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

        public bool CheckPassword(string password)
        {
            var regex = new Regex(_regexPattern);

            if (regex.IsMatch(password)) return true;

            return false;
        }

        public string GeneratePassword()
        {
            throw new NotImplementedException();
        }
    }
}
