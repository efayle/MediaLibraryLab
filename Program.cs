using System;
using NLog.Web;
using System.IO;

namespace MediaLibraryLab
{
    class Program
    {
         private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            

            string userChoice;
            do {
                //Menu options
                Console.WriteLine("1.) Add movie\n2.) Display movie\nType 'Enter' to quit");
                userChoice = Console.ReadLine();

                if (userChoice == "1") {
                    //Add movie
                } else if (userChoice == "2") {
                    //Display movies
                }
            } while (userChoice == "1" || userChoice == "2");

            logger.Info("Program ended");
        }
    }
}
