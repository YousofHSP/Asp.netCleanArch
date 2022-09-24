using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using CleanArch.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private UniversityDBContext _ctx;
        public UserRepository(UniversityDBContext ctx)
        {
            _ctx = ctx;
        }

        public void AddUser(User user)
        {
            _ctx.Users.Add(user);
        }

        public bool IsExistEmail(string email)
        {
            return _ctx.Users.Any(u => u.Email == email);
        }

        public bool IsExistUsername(string username)
        {
            return _ctx.Users.Any(u => u.Username == username);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
