using BookManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerDAL.Repository
{
    public class UserAccountRepository
    {
        private BookManagementDbContext _context;


        public UserAccount Login(string email, string pass)
        {
            _context = new();

            return _context.UserAccounts.FirstOrDefault(x => x.Email == email && x.Password == pass);
        }
    }
}
