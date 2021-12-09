using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        #region Old Calling Mechanism
        //private readonly AppDbContext _context;

        //public MoviesController(AppDbContext context)
        //{
        //    _context = context;
        //} 
        #endregion

        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        #region Search Movie 
        public async Task<IActionResult> Filter(string searchString)
        {
            var data = await _service.GetAllAsync((n => n.Cinema));
            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = data.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();
                return View("Index", filterResult);
            }
            return View("Index", data);
        }
        #endregion
        #region Page Load
        public async Task<IActionResult> Index()
        {
            //var data =await _context.Movies.Include(n=> n.Cinema).OrderBy(n=>n.Name).ToListAsync();
            //return View(data);

            var data = await _service.GetAllAsync((n => n.Cinema));
            return View(data);
        }
        #endregion

        #region Details
        //GET Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            return View(movieDetails);
        }
        #endregion

        #region View and Create New Movie
        //GET Movies/Create
        public async Task<IActionResult> Create()
        {
            var movieDropDownData = await _service.GetNewMovieDropdDownsValues();
            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            var movieDropDownData = await _service.GetNewMovieDropdDownsValues();
            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit  Movie
        //GET Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {

            var movieDetails = await _service.GetMovieByIdAsync(id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                ImageURL = movieDetails.ImageURL,
                CinemaId = movieDetails.CinemaId,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                MovieCategory = movieDetails.MovieCategory,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actor_Movies.Select(n => n.ActorId).ToList(),

            };

            var movieDropDownData = await _service.GetNewMovieDropdDownsValues();
            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");

            var movieDropDownData = await _service.GetNewMovieDropdDownsValues();
            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
