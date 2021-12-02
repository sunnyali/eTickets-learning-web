using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class ActorService : IActorsService
    {
        private readonly AppDbContext _context;

        public ActorService(AppDbContext context)
        {
            _context = context;
        }
        void IActorsService.Add(Actor actor)
        {
            throw new NotImplementedException();
        }

        void IActorsService.Delete(int id)
        {
            throw new NotImplementedException();
        }

      async Task< IEnumerable<Actor>> IActorsService.GetAll()
        {
            var result = await _context.Actors.ToListAsync();
            return result;
        }

        Actor IActorsService.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Actor IActorsService.Update(int id, Actor actor)
        {
            throw new NotImplementedException();
        }
    }
}
