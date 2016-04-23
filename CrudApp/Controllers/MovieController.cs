using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using CrudApp.Models;
using CrudApp.Services;
using CrudApp.Domain;

namespace CrudApp.Controllers
{
    public class MovieController : Controller
    {
        private IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [Route("~/")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Movie/Details/{id:int}"), HttpGet]
        public ActionResult Details(int id)
        {//make service call to check if move present in db, if present redirect to different view
            MovieViewModel movieViewModel = new MovieViewModel();
            movieViewModel.Id = id;
            movieViewModel.MoviePresent = _movieService.CheckIfMoviePresent(id);
            if (movieViewModel.MoviePresent)
            {
                movieViewModel.Rating = _movieService.GetMovie(id).Rating;
            }
            else
            {
                movieViewModel.Rating = 0;
            }
            return View(movieViewModel);
        }

        [Route("MyMovies"), HttpGet]
        public ActionResult List()
        {
            return View();
        }

        [Route("About"), HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}