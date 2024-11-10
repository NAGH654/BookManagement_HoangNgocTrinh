using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagerDAL.Entities;
using BookManagerDAL.Repository;

namespace BookManagerBLL.Services
{
    public class BookCategoryService
    {
        private BookCategoryRepo _repo = new();

        public List<BookCategory> GetAllCategory() => _repo.GetAll();
    }
}
