using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PropertyManagementApp
{
    /// <summary>
    /// Interaction logic for PropertyManagement.xaml
    /// </summary>
    public partial class PropertyManagement : Window
    {
        PropertyManagementEntities manage;
        Agent loggedAgent;
        public PropertyManagement(Agent agent)
        {
            InitializeComponent();
            loggedAgent = agent;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            manage = new PropertyManagementEntities();
            if (loggedAgent != null)
            {
                // Filter properties for the logged-in agent
                var agentProperties = manage.Properties.Where(p => p.AgentID == loggedAgent.AgentID).ToList();

                cboPropertyList.ItemsSource = agentProperties;
                cboPropertyList.DisplayMemberPath = "Name";
            }
        }

        private void cboPropertyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if an item is selected in the ComboBox
            if (cboPropertyList.SelectedItem != null)
            {
                // Get the selected property
                var selectedProperty = (Property)cboPropertyList.SelectedItem;

                // Update TextBoxes with the information of the selected property
                txtID.Text = selectedProperty.PropertyID.ToString();
                txtName.Text = selectedProperty.Name;
                txtAdress.Text = selectedProperty.Address;
                txtPrice.Text = selectedProperty.Price.ToString();

                rbSold.IsChecked = selectedProperty.ListingStatus == 0;
                rbSale.IsChecked = selectedProperty.ListingStatus == 1;
                rbRent.IsChecked = selectedProperty.ListingStatus == 2;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Check if the textboxes are not empty
            if (!string.IsNullOrWhiteSpace(txtID.Text) && !string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(txtAdress.Text) && 
                !string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                // Create a new Property object with the entered information
                Property newProperty = new Property
                {
                    PropertyID = int.Parse(txtID.Text),
                    Name = txtName.Text,
                    Address = txtAdress.Text,
                    Price = int.Parse(txtPrice.Text),
                    ListingStatus = 1,
                    AgentID = loggedAgent.AgentID,
                };

                // Add the new property to the database
                manage.Properties.Add(newProperty);
                manage.SaveChanges();

                // Refresh the list of properties to show the newly added property
                cboPropertyList.ItemsSource = manage.Properties.Where(p => p.AgentID == loggedAgent.AgentID).ToList();

                ClearFeilds();
            }
            else
            {
                MessageBox.Show("Please fill in all the required information.", "Incomplete Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearFeilds()
        {
            txtID.Clear();
            txtName.Clear();
            txtAdress.Clear();
            txtPrice.Clear();
            rbSold.IsChecked = false;
            rbSale.IsChecked = false;
            rbRent.IsChecked = false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Check if an item is selected
            if(cboPropertyList.SelectedItem != null)
            {
                // Get the selected property
                Property selectedProperty = (Property)cboPropertyList.SelectedItem;

                // Remove property from database
                manage.Properties.Remove(selectedProperty);
                manage.SaveChanges();

                // Refresh the list of properties to show the newly added property
                cboPropertyList.ItemsSource = manage.Properties.Where(p => p.AgentID == loggedAgent.AgentID).ToList();
            }
            else
            {
                MessageBox.Show("Please select a property to delete.", "No Property Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



    }
}
