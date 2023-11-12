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
    }
}
