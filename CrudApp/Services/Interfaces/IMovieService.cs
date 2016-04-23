using System.Collections.Generic;
using System.Threading.Tasks;
using CrudApp.Domain;
using CrudApp.Models.Requests;
using Newtonsoft.Json.Linq;

namespace CrudApp.Services
{
    public interface IMovieService
    {
        bool CheckIfMoviePresent(int id);
        void Delete(int id);
        List<Movie> GetAll();
        Task<JObject> GetExternalMovie(int id);
        Movie GetMovie(int id);
        void Insert(MovieAddRequest model);
        Task<JObject> SearchExternalMovie(string value);
        void Update(MovieUpdateRequest model);
    }
}