using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Data;
using Api.Security;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class UsersRepository
    {
        private readonly ApiContext context;

        public UsersRepository(ApiContext context)
        {
            this.context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await context.User.FindAsync(id);
            return user;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await context.User.ToListAsync();
        }

        public async Task<IEnumerable<User>> ListAsync(Expression<Func<User, bool>> predicate)
        {
            return await context.User.Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> ListAsync(Expression<Func<User, bool>> predicate, int start, int count)
        {
            return await context.User.Where(predicate)
                .Skip(start)
                .Take(count)
                .ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            IHasher hasher = new BCryptHasher();
            user.Username = user.Username.ToLower();
            user.Email = user.Email.ToLower();
            user.Password = hasher.HashPassword(user.Password);
            context.User.Add(user);
            await context.SaveChangesAsync();
        }

        public User ValidateUser(String username, String password)
        {
            User user = context.User.FirstOrDefault(
                u => u.Username.Equals(username.ToLower()) || u.Email.Equals(username.ToLower()));

            if (user != null)
            {
                IHasher hasher = new BCryptHasher();
                bool passwordValid = hasher.CheckPassword(password, user.Password);

                if (passwordValid)
                    return user;
            }
            return null;
        }

        public async Task EditAsync(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<User> DeleteAsync(int id)
        {
            var user = await context.User.FindAsync(id);
            if (user != null)
            {
                context.User.Remove(user);
                await context.SaveChangesAsync();
            }
            return user;
        }

        public bool Exists(int id)
        {
            return context.User.Any(e => e.Id == id);
        }
    }
}
