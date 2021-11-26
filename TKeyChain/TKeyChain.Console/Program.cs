using System;
using System.Linq;

namespace TKeyChain.Cli
{
    public class Program
    {
        private const int SUCCESS = 0;
        private const int FAILURE = 1;

        static int Main(string[] args)
        {
            try
            {
                if (args.Length == 0) Help.Print();

                if (args.Any(a => a.ToLower() == "-h" || a.ToLower() == "--help")) Help.Print();

                //TODO: Pass data about platform.
                var commandHandler = new CommandHandler();

                switch (args[0].ToLower())
                {
                    case "insert": commandHandler.Insert(args); break;
                    case "remove": commandHandler.Remove(args); break;
                    default: commandHandler.Get(args); break;
                }

                return SUCCESS;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Use the -h (--help) argument to check available interactions.");

                return FAILURE;
            }
        }
    }
}
