using CleanArch.Application.ViewModels.Account;
using CleanArch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
    public interface IUserService
    {
        CheckUser CheckUser(string username, string email);
        int Register(User user);
        bool IsExistUser(string email, string password);
    }
}
