using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        #region Old Calling Mechanism
        //private readonly AppDbContext _context;

        //public ActorsController(AppDbContext context)
        //{
        //    _context = context;
        //} 
        #endregion

        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        #region Page Load
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
        #endregion
        #region Details of Actor Page
        //GET Request  -> Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }
        #endregion
        #region View Create and Add New Actor
        //GET Request  -> Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST Request  -> Actors/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePicURL,FullName,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index");

        }
        #endregion
        #region Edit Page and Update Actor Details
        //GET Request  -> Actors/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        //POST Request  -> Actors/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(id, actor);
            return RedirectToAction("Index");

        }
        #endregion

        #region Delete Actor Detail
        //GET Request  -> Actors/Delete/id
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var actorDetails = await _service.GetByIdAsync(id);
        //    if (actorDetails == null) return View("NotFound");
        //    return View(actorDetails);
        //}

        //POST Request  -> Actors/Delete
        //[HttpPost,ActionName("Delete")]
       // [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");

        }
        #endregion
    }
}
