using System;

namespace TKeyChain.Cli
{
    public static class Help
    {
        private readonly static string[] commandsList = new string[]
        {
            "SCHEMA:",
            "<COMMAND> [ARGUMENT] <PARAMETER>",
            string.Empty,
            "COMMANDS:",
            "init - Initialize a new vault.",
            "-h (--help) - Display informations;",
            string.Empty,
            "get (default) - Retrive a password from the vault.",
            "-h (--help) - Display informations;",
            "-l --list - List password names;",
            "-n --no-clipboard - Do not copy the password to clipboard. (Default: false);",
            "-p --print - Print output password to terminal (Default: false);",
            "<password-name>",
            string.Empty,
            "insert - Insert a new password into the vault.",
            "-h (--help) - Display informations;",
            "<password-name>",
            string.Empty,
            "remove - Remove a password from the vault.",
            "-h (--help) - Display informations;",
            "<password-name>"
        };

        public static void Print()
        {
            //Logo
            Console.WriteLine(@$"{Environment.NewLine}__/\\\\\\\\\\\\\\\__/\\\________/\\\________/\\\\\\\\\_        {Environment.NewLine} _\///////\\\/////__\/\\\_____/\\\//______/\\\////////__       {Environment.NewLine}  _______\/\\\_______\/\\\__/\\\//_______/\\\/___________      {Environment.NewLine}   _______\/\\\_______\/\\\\\\//\\\______/\\\_____________     {Environment.NewLine}    _______\/\\\_______\/\\\//_\//\\\____\/\\\_____________    {Environment.NewLine}     _______\/\\\_______\/\\\____\//\\\___\//\\\____________   {Environment.NewLine}      _______\/\\\_______\/\\\_____\//\\\___\///\\\__________  {Environment.NewLine}       _______\/\\\_______\/\\\______\//\\\____\////\\\\\\\\\_ {Environment.NewLine}        _______\///________\///________\///________\/////////__{Environment.NewLine}{Environment.NewLine}");

            //Description
            Console.WriteLine($"(Transparent / Terminal) Key Chain - A secure and minimalistic password management solution.{Environment.NewLine}Created by: Krzysztofz01{Environment.NewLine}");

            //Commands
            foreach (var line in commandsList) Console.WriteLine(line);
        }
    }
}
