using CrudApp.Domain;
using CrudApp.Models.Requests;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace CrudApp.Services
{
    public class MovieService
    {
        private static readonly string _apiKey = WebConfigurationManager.AppSettings["MovieDbKey"];

        public static void Insert(MovieAddRequest model)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MovieConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Movie_Insert";
                cmd.Parameters.AddWithValue("@MovieId", model.Id);
                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@Rating", model.Rating);
                cmd.Parameters.AddWithValue("@Image", model.Poster_path);
                cmd.ExecuteNonQuery();
            }
        }

        public static bool CheckIfMoviePresent(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MovieConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetMovieByMovieId";
                cmd.Parameters.AddWithValue("@MovieId", id);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static Movie GetMovie(int id)
        {
            Movie movie = null;
            string connectionString = ConfigurationManager.ConnectionStrings["MovieConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetMovieByMovieId";
                cmd.Parameters.AddWithValue("@MovieId", id);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    movie = new Movie();
                    movie.Id = dataReader.GetInt32(dataReader.GetOrdinal("MovieId"));
                    movie.Title = dataReader["Title"].ToString();
                    movie.Rating = dataReader.GetInt32(dataReader.GetOrdinal("Rating"));
                    movie.Poster_path = dataReader["Image"].ToString();
                }
            }
            return movie;
        }

        public static void Delete(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MovieConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Movie_Delete";
                cmd.Parameters.AddWithValue("@MovieId", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Movie> GetAll()
        {
            List<Movie> movies = null;
            Movie movie = null;
            string connectionString = ConfigurationManager.ConnectionStrings["MovieConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Movie_GetAll";
                SqlDataReader dataReader = cmd.ExecuteReader();
                movies = new List<Movie>();
                while (dataReader.Read())
                {
                    movie = new Movie();
                    movie.Id = dataReader.GetInt32(dataReader.GetOrdinal("MovieId"));
                    movie.Title = dataReader["Title"].ToString();
                    movie.Rating = dataReader.GetInt32(dataReader.GetOrdinal("Rating"));
                    movie.Poster_path = dataReader["Image"].ToString();
                    movies.Add(movie);
                }
            }
            return movies;
        }

        public static void Update(MovieUpdateRequest model)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MovieConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Movie_Update";
                cmd.Parameters.AddWithValue("@MovieId", model.Id);
                cmd.Parameters.AddWithValue("@Rating", model.Rating);
                cmd.ExecuteNonQuery();
            }
        }

        //calls to TheMovieDB api
        public static async Task<JObject> GetExternalMovie(int id)
        {
            
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("3/movie/" + id + "?api_key=" + _apiKey);

                JObject data = await response.Content.ReadAsAsync<JObject>();
                return data;

            }
        }

        public static async Task<JObject> SearchExternalMovie(string value)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response=await client.GetAsync("3/search/movie?api_key="+_apiKey+"&query="+value);

                JObject data = await response.Content.ReadAsAsync<JObject>();
                return data;
            }
        }
    }
}




