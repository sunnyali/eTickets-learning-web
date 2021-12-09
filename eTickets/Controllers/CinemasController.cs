using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        #region Old Calling Mechanism
        //private readonly AppDbContext _context;

        //public CinemasController(AppDbContext context)
        //{
        //    _context = context;
        //} 
        #endregion

        private readonly ICinemaService _service;

        public CinemasController(ICinemaService service)
        {
            _service = service;
        }
        #region PageLoad
        public async Task<IActionResult> Index()
        {

            var data = await _service.GetAllAsync();
            return View(data);

        }
        #endregion

        #region Delete Cinema
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");

        }
        #endregion

        #region Edit Cinema and Update Cinema Details
        //GET Request  -> Cinema/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        //POST Request  -> Cinema/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction("Index");

        }


        #endregion

        #region View Create and Add New Cinema
        //GET Request  -> Cinema/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST Request  -> Cinema/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.AddAsync(cinema);
            return RedirectToAction("Index");

        }
        #endregion
    }
}
