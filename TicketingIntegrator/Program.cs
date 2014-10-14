using System;
using System.Configuration;


namespace TicketingIntegrator
{
    class Program
    {
        static string _fogBugzApiUrl; // The API Url for FogBugz instance
        static string _fogBugzApiToken; // The username / email address for the login to FogBugz
        static readonly Processor Processor = new Processor(); // Logic class


        static void Main(string[] args)
        {
            string input = "";

            // Parsing configurationa and returns if false indicating an error
            if (!ReadConfig())
            {
                return;
            }

            // Getting commandline input and clean up
            input = Environment.CommandLine;
            input = input.Substring(input.IndexOf(@"{"));
            input.TrimEnd('\"');

            // Calling CreateCase passing in the commandline arguments and then writting this output to the console (JSON string)
            //Console.WriteLine(@"""{0}""", processor.CreateCase(input, fogBugzAPIUrl, fogBugzAPIToken));
            Console.Write(Processor.CreateCase(input, _fogBugzApiUrl, _fogBugzApiToken));
        }


        static bool ReadConfig()
        {
            // Parsing configuration file
            try
            {
                _fogBugzApiUrl = ConfigurationManager.AppSettings["FogBugzAPIUrl"];
                _fogBugzApiToken = ConfigurationManager.AppSettings["FogBugzAPIToken"];
            }
            catch
            {
                var jsonHelper = new JsonHelper();
                Console.Write(jsonHelper.CreateFailureResponse("Unable to read TicketIntegrator application settings"));
                return false;
            }

            return true;
        }

    }
}
