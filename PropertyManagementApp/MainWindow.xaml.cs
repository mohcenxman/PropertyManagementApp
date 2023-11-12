using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PropertyManagementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PropertyManagementEntities manage;
        bool open = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //txtUser.DataContext = manage.Agents.ToList();
            manage = new PropertyManagementEntities();

            // Check if the entered username and password are valid
            string enteredUsername = txtUser.Text;
            string enteredPassword = txtPassword.Password;

            var agent = manage.Agents.FirstOrDefault(a => a.UserName == enteredUsername && a.Password == enteredPassword);

            if (agent != null)
            {
                // Login successful
                open = true;

                // Open PropertyManagementWindow
                PropertyManagement propertyManagement = new PropertyManagement();
                propertyManagement.Show();

                // Close the current login window
                this.Close();
            }
            else
            {
                // Invalid username or password
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtUser_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (txtUser.Text != null)
            {
                btnOk.IsEnabled = true;
            }
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password != null)
            {
                btnOk.IsEnabled = true;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (open)
            {
                // Confirmation pour quitter l'application
                if (MessageBox.Show("Continue to colse the app ?", "Attention !",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
