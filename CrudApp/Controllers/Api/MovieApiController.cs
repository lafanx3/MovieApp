using CrudApp.Domain;
using CrudApp.Models.Requests;
using CrudApp.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CrudApp.Controllers.Api
{
    [RoutePrefix("api/movie")]
    public class MovieApiController : ApiController
    {
        private IMovieService _movieService;

        public MovieApiController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [Route, HttpPost]
        public HttpResponseMessage Insert(MovieAddRequest model)
        {
            if (ModelState.IsValid)
            {
                _movieService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, "Success");

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Api insert failed.");
            }
        }

        [Route("deleteMovie/{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _movieService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "failed");
            }
        }

        [Route("getAll"), HttpGet]
        public HttpResponseMessage GetAll()
        {
            List<Movie> movies = _movieService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, movies);
        }

        [Route("update"), HttpPut]
        public HttpResponseMessage Update(MovieUpdateRequest model)
        {
            if (ModelState.IsValid)
            {
                _movieService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, "movie updated successfully");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "update failed");
            }
        }

        [Route("getExternalMovie/{id:int}"), HttpGet]
        public async Task<HttpResponseMessage> GetMovie(int id)
        {
            try
            {
                JObject response = await _movieService.GetExternalMovie(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "get movies from external moviedb failed");
            }
        }

        [Route("searchExternalMovie/{value}"), HttpGet]
        public async Task<HttpResponseMessage> SearchMovie(string value)
        {
            try
            {
                JObject response = await _movieService.SearchExternalMovie(value);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "movie search to external moviedb failed");
            }
        }
    }
}
