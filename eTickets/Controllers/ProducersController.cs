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
    public class ProducersController : Controller
    {
        #region Old Calling Mechanism

        //private readonly AppDbContext _context;

        //public ProducersController(AppDbContext context)
        //{
        //    _context = context;
        //} 
        #endregion


        private readonly IProducerService _service;

        public ProducersController(IProducerService service)
        {
            _service = service;
        }
        #region PageLoad
        public async Task<IActionResult> Index()
        {
            //var allProducers =await _service.Producers.ToListAsync();
            //return View(allProducers);

            var data = await _service.GetAllAsync();
            return View(data);
        }
        #endregion

        #region Delete Producer
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");

        }
        #endregion

        #region Edit Producer and Update Producer Details
        //GET Request  -> Producer/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        //POST Request  -> Producer/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.UpdateAsync(id, producer);
            return RedirectToAction("Index");

        }
        #endregion

        #region View Producer and Add New Producer
        //GET Request  -> Producer/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST Request  -> Producer/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePicURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.AddAsync(producer);
            return RedirectToAction("Index");

        }
        #endregion
    }
}
