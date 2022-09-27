using CleanArch.Application.Interfaces;
using CleanArch.Application.Security;
using CleanArch.Application.ViewModels.Account;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public CheckUser CheckUser(string username, string email)
        {
            bool usernameValid = _userRepository.IsExistUsername(username);
            bool emailValid = _userRepository.IsExistEmail(email.Trim().ToLower());

            if (usernameValid)
                return ViewModels.Account.CheckUser.UsernameNotValid;
            else if (emailValid)
                return ViewModels.Account.CheckUser.EmailNotValid;
            return ViewModels.Account.CheckUser.Ok;

        }

        public bool IsExistUser(string email, string password)
        {
            return _userRepository.IsExistUser(email.Trim().ToLower(), PasswordHelper.EncodePasswordMd5(password));
        }

        public int Register(User user)
        {
            _userRepository.AddUser(user);
            _userRepository.Save();
            return user.Id;
        }
    }
}
