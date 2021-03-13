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

            string movieFilePath = Directory.GetCurrentDirectory() + "\\movies.scrubbed.csv";
            MovieFile movieFile = new MovieFile(movieFilePath);

            string userChoice;
            do {
                //Menu options
                Console.WriteLine("1.) Add movie\n2.) Display movie\nType 'Enter' to quit");
                userChoice = Console.ReadLine();

                if (userChoice == "1") {
                    //Add movie
                    Movie movie = new Movie();

                    string userResponse;
                    Console.WriteLine("Do you want to enter a movie? ");
                    userResponse = Console.ReadLine().ToUpper();
                    if (userResponse == "Y") {
                        //Movie information
                        Console.WriteLine("Enter movie title: ");
                        movie.title = Console.ReadLine();
                        
                        string userInput;
                        do {
                            Console.WriteLine("Enter genre (or type 'done' to quit) ");
                            userInput = Console.ReadLine();

                            if (userInput != "done" && userInput.Length > 0) {
                                movie.genres.Add(userInput);
                            }
                        } while (userInput != "done");

                        if (movie.genres.Count == 0) {
                            movie.genres.Add("(no genres listed)");
                        }
                        
                        Console.WriteLine("Enter movie director: ");
                        movie.director = Console.ReadLine();

                        // Console.WriteLine("Enter running time: (h:m:s) ");
                        // string runTime = null;
                        // runTime = movie.runningTime.ToString()

                        movieFile.AddMovie(movie);
                    }
                } else if (userChoice == "2") {
                    //Display movies

                    foreach (Movie m in movieFile.Movies) {
                        Console.WriteLine(m.Display());
                    }
                }
            } while (userChoice == "1" || userChoice == "2");

            logger.Info("Program ended");
        }
    }
}
