using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagerDAL.Entities;
using BookManagerDAL.Repository;

namespace BookManagerBLL.Services
{
    public class UserAccountService
    {
        private UserAccountRepository _repo = new();


        public UserAccount CheckLogin(string email, string pass) => _repo.Login(email, pass);
    }
}
