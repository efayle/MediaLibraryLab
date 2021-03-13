using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace MediaLibraryLab
{
    public class MovieFile
    {
        public string scrubbedFile { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Media> Medias { get; set; }

        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public MovieFile (string movieScrubbedFile) 
        {
            scrubbedFile = movieScrubbedFile;
            Movies = new List<Movie>();

            try
            {
                StreamReader sr = new StreamReader(scrubbedFile);
                while (!sr.EndOfStream) {
                    string line = sr.ReadLine();
                    string[] array = line.Split(',');
                    
                    Movie movie = new Movie();

                    movie.mediaId = UInt64.Parse(array[0]);
                    movie.title = array[1];
                    movie.genres = array[2].Split('|').ToList();
                    movie.director = array[3];
                    movie.runningTime = TimeSpan.Parse(array[4]);

                    Movies.Add(movie);

                }
                sr.Close();
            } catch (Exception ex) {
                logger.Error(ex.Message);
            }
        }

        public void AddMovie (Movie movie)
        {

            try {
                movie.mediaId = Movies.Max(m => m.mediaId) + 1;
                StreamWriter sw = new StreamWriter(scrubbedFile, append: true);
                sw.WriteLine("{0},{1},{2},{3},{4}", movie.mediaId, movie.title, string.Join("|", movie.genres), movie.director, movie.runningTime);
                sw.Close();

                Movies.Add(movie);

            } catch (Exception ex) {
                logger.Error(ex.Message);
            }
        }
    }
}