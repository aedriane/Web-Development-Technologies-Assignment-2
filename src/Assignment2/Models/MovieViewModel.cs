using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    public class MovieViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<MovieComingSoon> MoviesSoon { get; set; }

        SqlConnection con = new SqlConnection();

        Movie m = null;
        MovieComingSoon mcs = null;

        public List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>();

            con.ConnectionString = @"Server=DESKTOP-R9FQAAL\SQLEXPRESS;Database=Assignment2;Trusted_Connection=True;";


             con.Open();

            using (con)
            {

                SqlCommand cmd = new SqlCommand("Select * from Movie", con);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())

                {

                    m = new Movie();
                    m.Title = Convert.ToString(rd.GetSqlValue(1));
                    m.ShortDescription = Convert.ToString(rd.GetSqlValue(2));
                    m.LongDescription = Convert.ToString(rd.GetSqlValue(3));
                    m.ImageUrl = Convert.ToString(rd.GetSqlValue(4));
                    m.Price = rd.GetDecimal(5);

                    movies.Add(m);

                }
            }
            return movies;
        }

        public List<MovieComingSoon> GetMovieComingSoon()
        {
            List<MovieComingSoon> movieSoon = new List<MovieComingSoon>();

            con.ConnectionString = "Server=DESKTOP-R9FQAAL\\SQLEXPRESS;Database=Assignment2;Trusted_Connection=True;";


            con.Open();

            using (con)
            {

                SqlCommand cmd = new SqlCommand("Select * from MovieComingSoon", con);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())

                {

                    mcs = new MovieComingSoon();
                    mcs.Title = Convert.ToString(rd.GetSqlValue(1));
                    mcs.ShortDescription = Convert.ToString(rd.GetSqlValue(2));
                    mcs.LongDescription = Convert.ToString(rd.GetSqlValue(3));
                    mcs.ImageUrl = Convert.ToString(rd.GetSqlValue(4));
                    movieSoon.Add(mcs);

                }
            }
            return movieSoon;
        }

    }


}
