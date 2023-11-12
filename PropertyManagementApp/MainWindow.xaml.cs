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
        Agent loggedAgent;
        bool open = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            manage = new PropertyManagementEntities();

            // Check if the entered username and password are valid
            string enteredUsername = txtUser.Text;
            string enteredPassword = txtPassword.Password;

            var loggedAgent = manage.Agents.FirstOrDefault(a => a.UserName == enteredUsername && a.Password == enteredPassword);

            if (loggedAgent != null)
            {
                // Login successful
                open = true;

                // Open Property Management UI
                PropertyManagement propertyManagement = new PropertyManagement(loggedAgent);
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
            // btnOk.IsEnabled = !string.IsNullOrWhiteSpace(txtUser.Text) && !string.IsNullOrWhiteSpace(txtPassword.Password);

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
                // Confirmation before colsing the app
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
