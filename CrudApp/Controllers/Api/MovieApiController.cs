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
        [Route, HttpPost]
        public HttpResponseMessage Insert(MovieAddRequest model)
        {
            if (ModelState.IsValid)
            {
                MovieService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, "Success");

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Api insert failed.");
            }
        }

        //not used atm
        [Route("getMovieStatus/{id:int}"), HttpGet]
        public HttpResponseMessage GetMovieStatus(int id)
        {
            if (ModelState.IsValid)
            {
                bool movieStatus = MovieService.CheckIfMoviePresent(id);
                return Request.CreateResponse(HttpStatusCode.OK, movieStatus);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Api movie status failed");
            }
        }

        [Route("deleteMovie/{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                MovieService.Delete(id);
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
            List<Movie> movies = MovieService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, movies);
        }

        [Route("update"), HttpPut]
        public HttpResponseMessage Update(MovieUpdateRequest model)
        {
            if (ModelState.IsValid)
            {
                MovieService.Update(model);
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
                JObject response = await MovieService.GetExternalMovie(id);
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
                JObject response = await MovieService.SearchExternalMovie(value);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "movie search to external moviedb failed");
            }
        }
    }
}
