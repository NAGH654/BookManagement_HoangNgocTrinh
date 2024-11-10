using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagerDAL.Entities;
using BookManagerDAL.Repository;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BookManagerBLL.Services
{
    public class BookService
    {
        private BookRepository _repo = new();


        public List<Book> GetAllBooks() => _repo.GetAll();


        public void Add(Book x) => _repo.Add(x);
        public void UpdateBook(Book x) => _repo.Update(x);

        public void DeleteBook(Book x) => _repo.Delete(x);

        public List<Book> SearchBooks(string name, string des) => _repo.Search(name, des);

        public List<Book> SearchByNameAndDate(string name, DateTime date) =>
            _repo.SearchByNameAndDate(name, date);

        public List<Book> SearchByNameAndCategory(string name, int id) => _repo.SearchByNameAndCategory(name, id);
    }
}
