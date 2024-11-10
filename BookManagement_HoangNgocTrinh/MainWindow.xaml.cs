using System.Windows;
using BookManagerBLL.Services;
using BookManagerDAL.Entities;

namespace BookManagement_HoangNgocTrinh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookService _service = new();
        public UserAccount _user { get; set; }
        private BookCategoryService _bookcateService = new();
        public MainWindow()

        {
            InitializeComponent();
        }

        private void BookMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            HelloMsgLabel.Content = "Hello " + _user.FullName;
            FillDataGrid();
            if (_user.Role == 2)
            {
                CreateButton.IsEnabled = false;
                UpdateButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }


        }

        private void FillDataGrid()
        {
            BookListDataGrid.ItemsSource = null;

            BookListDataGrid.ItemsSource = _service.GetAllBooks();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            BookDetailWindow d = new();
            d.ShowDialog();
            FillDataGrid();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {



            Book selected = BookListDataGrid.SelectedItem as Book;
            if (selected == null)
            {
                MessageBox.Show("You have to select one book to update it", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            BookDetailWindow b = new();
            b.EditedBook = selected;
            b.ShowDialog();
            FillDataGrid();

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //if (BookNameTextBox.Text == null || DescriptionTextBox.Text == null)
            //{
            //    MessageBox.Show("You have to enter book's name or book's description to search", "Error",
            //        MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //    return;
            //}

            //______________________________________________________________________
            //neu truong hop search co nhap so thi xai cai duoi de bat loi 

            //bool r = int.TryParse(QuantityTextBox.Text, out int quant);
            //int? quantity = null;
            //if (r == false && !QuantityTextBox.Text.IsNullOrEmpty()) // go bay ba khong doi duoc
            //{
            //    MessageBox.Show("Quantity must be a number ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //if (r == false) // khong go gi ca
            //{
            //    quantity = null;
            //}
            //else
            //{
            //    quantity = quant;
            //}



            var result = _service.SearchBooks(BookNameTextBox.Text, DescriptionTextBox.Text);
            BookListDataGrid.ItemsSource = null;
            BookListDataGrid.ItemsSource = result;

            //.............................................................................
            //neu nhu search bang thoi gian thi xai cai duoi day !!!


            //var result = _service.SearchByNameAndDate(BookNameTextBox.Text, Convert.ToDateTime(DatePublishTextBox.Text));
            //BookListDataGrid.ItemsSource = null;
            //BookListDataGrid.ItemsSource = result;

            //............................................................................
            //neu truong hop search bang categoryId thi xai cai nay

            //var categoryId = int.Parse(BookCategoryIdComboBox.SelectedValue.ToString());
            //var result = _service.SearchByNameAndCategory(BookNameTextBox.Text, categoryId);
            //BookListDataGrid.ItemsSource = null;
            //BookListDataGrid.ItemsSource = result;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Book selected = BookListDataGrid.SelectedItem as Book;
            if (selected == null)
            {
                MessageBox.Show("You have to select one book to delete it", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult answer = MessageBox.Show("Do you really want to delete this book", "Confirm",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (answer == MessageBoxResult.No) return;
            _service.DeleteBook(selected);
            FillDataGrid();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Do you really want to quit ?", "Confirm", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (answer == MessageBoxResult.No) return;

            Application.Current.Shutdown();
        }


    }
}