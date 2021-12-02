using eTickets.Data;
using eTickets.Data.Services;
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
        public async Task<IActionResult> Index()
        {
            var data =  await _service.GetAll();
            return View(data);
        }
    }
}
