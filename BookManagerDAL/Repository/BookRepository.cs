using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookManagerDAL.Repository
{
    public class BookRepository
    {
        private BookManagementDbContext _context;

        public List<Book> GetAll()
        {
            _context = new();
            return _context.Books.Include("BookCategory").ToList();
        }

        public void Update(Book x)
        {
            _context = new();
            _context.Books.Update(x);
            _context.SaveChanges();
        }


        public void Delete(Book x)
        {
            _context = new();
            _context.Books.Remove(x);
            _context.SaveChanges();
        }


        public void Add(Book x)
        {
            _context = new();
            _context.Books.Add(x);
            _context.SaveChanges();
        }

        public List<Book> Search(string name, string description)
        {
            _context = new();
            List<Book> result = _context.Books.Include("BookCategory").ToList();

            if (name.IsNullOrEmpty() && description.IsNullOrEmpty()) return result;

            if (!name.IsNullOrEmpty() && !description.IsNullOrEmpty())
            {
                return result.Where(x =>
                    x.BookName.ToLower().Contains(name.ToLower()) &&
                    x.Description.ToLower().Contains(description.ToLower())).ToList();
            }

            if (!name.IsNullOrEmpty())
            {
                return result.Where(x => x.BookName.ToLower().Contains(name.ToLower())).ToList();
            }
            else
            {
                return result.Where(x => x.Description.ToLower().Contains(description.ToLower())).ToList();
            }


            // LUU Y: Search theo chuoi thi bat buoc phai doi ve thuong het de search chu khong la loi do con trai !!! 
            /*
           Gio chia 3 tinh huong cu the:
          1. User khong go gi ca  => return full
          2. User go ca 2 o => And hoac Or
          3. User go 1 trong 2 o  => nhan vao cho nao thi WHERE no gium cai !!!!
           👈(⌒▽⌒)👉)
           */

            //List<AirConditioner> result = _context.AirConditioners.Include("Supplier").ToList();

            //_context = new();
            ////1
            //if (feature.IsNullOrEmpty() && quantity == null)
            //{
            //    return result;
            //}

            ////2
            //if (!feature.IsNullOrEmpty() && quantity != null)
            //{
            //    return result.Where(x => x.FeatureFunction.ToLower().Contains(feature.ToLower()) && x.Quantity == quantity).ToList();
            //}
            ////3
            //if (!feature.IsNullOrEmpty())
            //{
            //    return result.Where(x => x.FeatureFunction.ToLower().Contains(feature.ToLower())).ToList();
            //}
            //else
            //{
            //    return result.Where(x => x.Quantity == quantity).ToList();

        }

        public List<Book> SearchByNameAndDate(string name, DateTime publishDate)
        {
            _context = new();
            List<Book> result = _context.Books.Include("BookCategory").ToList();

            // If both name and date are not provided, return the full list
            if (name.IsNullOrEmpty() && publishDate == default(DateTime))
            {
                return result;
            }

            // If both name and date are provided, filter by both
            if (!name.IsNullOrEmpty() && publishDate != default(DateTime))
            {
                return result.Where(x =>
                    x.BookName.ToLower().Contains(name.ToLower()) &&
                    x.PublicationDate.Date == publishDate.Date).ToList();
            }

            // If only name is provided, filter by name
            if (!name.IsNullOrEmpty())
            {
                return result.Where(x => x.BookName.ToLower().Contains(name.ToLower())).ToList();
            }

            // If only date is provided, filter by date
            return result.Where(x => x.PublicationDate.Date == publishDate.Date).ToList();
        }

        public List<Book> SearchByNameAndCategory(string name, int bookCategoryId)
        {
            _context = new();
            List<Book> result = _context.Books.Include("BookCategory").ToList();

            // If both name and category ID are not provided, return the full list
            if (name.IsNullOrEmpty() && bookCategoryId == 0)
            {
                return result;
            }

            // If both name and category ID are provided, filter by both
            if (!name.IsNullOrEmpty() && bookCategoryId != 0)
            {
                return result.Where(x =>
                    x.BookName.ToLower().Contains(name.ToLower()) &&
                    x.BookCategoryId == bookCategoryId).ToList();
            }

            // If only name is provided, filter by name
            if (!name.IsNullOrEmpty())
            {
                return result.Where(x => x.BookName.ToLower().Contains(name.ToLower())).ToList();
            }

            // If only category ID is provided, filter by category ID
            return result.Where(x => x.BookCategoryId == bookCategoryId).ToList();
        }



        //public int BookId { get; set; }

        //public string BookName { get; set; } = null!;

        //public string Description { get; set; } = null!;

        //public DateTime PublicationDate { get; set; }

        //public int Quantity { get; set; }

        //public double Price { get; set; }

        //public string Author { get; set; } = null!;

        //public int BookCategoryId { get; set; }

        //public virtual BookCategory BookCategory { get; set; } = null!;

    }
}
