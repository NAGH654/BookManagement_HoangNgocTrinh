using System;
using System.Windows;
using BookManagerBLL.Services;
using BookManagerDAL.Entities;

namespace BookManagement_HoangNgocTrinh
{
    public partial class BookDetailWindow : Window
    {
        private BookService _service = new();
        private BookCategoryService _bookcateService = new();
        public Book EditedBook { get; set; }

        public BookDetailWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Disable BookIdTextBox to prevent editing
            BookIdTextBox.IsEnabled = false;

            // Check if any required fields are missing
            if (string.IsNullOrWhiteSpace(AuthorTextBox.Text) ||
                string.IsNullOrWhiteSpace(BookNameTextBox.Text) ||
                PublicationDateDatePicker.SelectedDate == null ||
                BookCategoryIdComboBox.SelectedValue == null)
            {
                MessageBox.Show("Error: All fields are required.");
                return; // Exit the method if any field is missing
            }

            // Validate the publication date
            DateTime publicationDate = (DateTime)PublicationDateDatePicker.SelectedDate;
            if (publicationDate >= new DateTime(2020, 1, 1))
            {
                MessageBox.Show("Error: Publication date must be before January 1, 2020.");
                return; // Exit the method if date is invalid
            }

            // Validate the book name
            string bookName = BookNameTextBox.Text;
            if (!IsBookNameValid(bookName))
            {
                MessageBox.Show("Error: Book name must start with a capital letter and contain only letters and spaces.");
                return; // Exit the method if book name is invalid
            }

            // Create a new Book object
            Book x = new()
            {
                Author = AuthorTextBox.Text,
                BookName = bookName,
                Description = DescriptionTextBox.Text,
                Price = double.Parse(PriceTextBox.Text),
                Quantity = int.Parse(QuantityTextBox.Text),
                PublicationDate = publicationDate,
                BookCategoryId = int.Parse(BookCategoryIdComboBox.SelectedValue.ToString())
            };

            // Add or update the book record
            if (EditedBook == null)
            {
                _service.Add(x);
            }
            else
            {
                x.BookId = int.Parse(BookIdTextBox.Text);
                _service.UpdateBook(x);
            }

            // Close the window
            this.Close();
        }

        // Method to validate the book name
        private bool IsBookNameValid(string bookName)
        {
            // Check if the name starts with a capital letter and contains only letters and spaces
            var words = bookName.Split(' ');
            foreach (var word in words)
            {
                if (string.IsNullOrEmpty(word) || !char.IsUpper(word[0]) || !System.Text.RegularExpressions.Regex.IsMatch(word, @"^[a-zA-Z]+$"))
                {
                    return false;
                }
            }
            return true;
        }


        private void FillComBoBox()
        {
            BookCategoryIdComboBox.ItemsSource = _bookcateService.GetAllCategory();
            BookCategoryIdComboBox.SelectedValuePath = "BookCategoryId";
            BookCategoryIdComboBox.DisplayMemberPath = "BookGenreType";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComBoBox();
            if (EditedBook != null) FillElements();
        }

        private void FillElements()
        {
            BookIdTextBox.Text = EditedBook.BookId.ToString();
            BookIdTextBox.IsEnabled = false;
            BookNameTextBox.Text = EditedBook.BookName;
            DescriptionTextBox.Text = EditedBook.Description;
            PriceTextBox.Text = EditedBook.Price.ToString();
            QuantityTextBox.Text = EditedBook.Quantity.ToString();
            BookCategoryIdComboBox.SelectedValue = EditedBook.BookCategory.BookCategoryId;
            PublicationDateDatePicker.SelectedDate = EditedBook.PublicationDate;
            AuthorTextBox.Text = EditedBook.Author;
        }
    }
}
