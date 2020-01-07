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
    public class AdvertisementsRepository
    {
        private readonly ApiContext context;

        public AdvertisementsRepository(ApiContext context)
        {
            this.context = context;
        }

        public async Task<Advertisement> GetByIdAsync(int id)
        {
            var ad = await context.Advertisement.FindAsync(id);
            return ad;
        }

        public async Task<IEnumerable<Advertisement>> ListAsync()
        {
            return await context.Advertisement
                .Include(ad => ad.Owner)
                .ToListAsync();
        }

        public async Task<IEnumerable<Advertisement>> ListAsync(Expression<Func<Advertisement, bool>> predicate)
        {
            return await context.Advertisement.Where(predicate)
                .Include(ad => ad.Owner)
                .ToListAsync();
        }

        public async Task<AdvertisementsBindingModel> ListAsync(Filter filter)
        {
            var ads = context.Advertisement.AsQueryable();

            // Price filter
            if (filter.MinPrice != null && filter.MaxPrice != null)
            {
                ads = ads.Where(ad => ad.Price >= filter.MinPrice && ad.Price <= filter.MaxPrice);
            }

            // Transmission filter
            if (filter.Transmission != null)
            {
                ads = ads.Where(ad => ad.Transmission == filter.Transmission);
            }

            // Fuel Type filter
            if (filter.FuelType != null)
            {
                ads = ads.Where(ad => ad.FuelType == filter.FuelType);
            }

            // Order by CreationDate
            ads = ads.OrderByDescending(ad => ad.CreatedAt);

            int totalCount = ads.Count();

            ads = ads.Skip(filter.Start)
                .Take(filter.Count);

            return new AdvertisementsBindingModel
            {
                Count = totalCount,
                Advertisements = await ads.ToListAsync()
            };
        }

        public async Task AddAsync(Advertisement ad)
        {
            context.Advertisement.Add(ad);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(Advertisement ad)
        {
            context.Entry(ad).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Advertisement> DeleteAsync(int id)
        {
            var ad = await context.Advertisement.FindAsync(id);
            if (ad != null)
            {
                context.Advertisement.Remove(ad);
                await context.SaveChangesAsync();
            }
            return ad;
        }

        public bool Exists(int id)
        {
            return context.Advertisement.Any(e => e.Id == id);
        }
    }
}
