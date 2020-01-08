using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.BindingModels;
using Api.Data;
using Api.Security;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class ReservationsRepository
    {
        private readonly ApiContext context;

        public ReservationsRepository(ApiContext context)
        {
            this.context = context;
        }

        public IQueryable<Reservation> AsQueryable()
        {
            return context.Reservation.AsQueryable();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            var res = await context.Reservation.FindAsync(id);
            return res;
        }

        public async Task<IEnumerable<Reservation>> ListAsync()
        {
            return await context.Reservation
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> ListAsync(Expression<Func<Reservation, bool>> predicate)
        {
            return await context.Reservation.Where(predicate)
                .ToListAsync();
        }
        public async Task AddAsync(Reservation res)
        {
            context.Reservation.Add(res);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(Reservation res)
        {
            context.Entry(res).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Reservation> DeleteAsync(int id)
        {
            var res = await context.Reservation.FindAsync(id);
            if (res != null)
            {
                context.Reservation.Remove(res);
                await context.SaveChangesAsync();
            }
            return res;
        }

        public bool Exists(int id)
        {
            return context.Advertisement.Any(e => e.Id == id);
        }
    }
}
