using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagerDAL.Entities;

namespace BookManagerDAL.Repository
{
    public class BookCategoryRepo
    {
        private BookManagementDbContext _context;

        public List<BookCategory> GetAll()
        {
            _context = new();
            return _context.BookCategories.ToList();
        }
    }
}
