using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class ActorService_old : IActorsService
    {
        private readonly AppDbContext _context;

        public ActorService(AppDbContext context)
        {
            _context = context;
        }
        async Task IActorsService.AddAsync(Actor actor)
        {
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
        }

        async Task IActorsService.DeleteAsync(int id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(n => n.Id == id);
            _context.Actors.Remove(result);
            await _context.SaveChangesAsync();

        }

        async Task<IEnumerable<Actor>> IActorsService.GetAllAsync()
        {
            var result = await _context.Actors.ToListAsync();
            return result;
        }

        async Task<Actor> IActorsService.GetByIdAsync(int id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        async Task<Actor> IActorsService.UpdateAsync(int id, Actor newactor)
        {
            _context.Update(newactor);
            await _context.SaveChangesAsync();
            return newactor;
        }
    }
}
