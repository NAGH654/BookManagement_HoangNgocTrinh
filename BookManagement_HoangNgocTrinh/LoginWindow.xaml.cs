using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookManagerBLL.Services;
using MessageBox = System.Windows.MessageBox;

namespace BookManagement_HoangNgocTrinh
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private UserAccountService _service = new();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var result = _service.CheckLogin(EmailTextBox.Text.ToString(), PasswordTextBox.Text.ToString());

            if (result == null)
            {
                MessageBox.Show("Invalid email or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MainWindow m = new();
            m._user = result;
            m.Show();
            this.Hide();
        }
    }
}
