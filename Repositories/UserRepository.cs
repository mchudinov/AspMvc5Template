﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Models;

namespace Repositories
{
    [LogException]
    public class UserRepository : IUserRepository
    {
        public IList<User> GetAllUsers()
        {
            using (var db = new AppDbContext())
            {
                return db.Users.AsNoTracking().OrderBy(u=>u.Nickname).ToList();
            }
        }

        public User GetUser(IFormattable id)
        {
            using (var db = new AppDbContext())
            {
                return db.Users.First(u => u.Id == (Guid)id);
            }
        }

        public IList<User> GetUsers(string filter)
        {
            using (var db = new AppDbContext())
            {
                return db.Users.AsNoTracking().Where(u => u.Nickname.ToLower().Contains(filter.ToLower())).OrderBy(u => u.Nickname).ToList();
            }
        }

        public async Task SaveUser(User user)
        {
            using (var db = new AppDbContext())
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
        }
    }
}
