# PropertyManagementApp

This real estate management application was developed using WPF (.NET Entity Framework) with a Database First approach.
The application is a platform for agents to manage properties, with the ability to add, modify, and delete properties.

Login:
The MainWindow class handles the login functionality. Users (agents) enter their username and password, and the application checks if the provided credentials match any entries in the database (PropertyManagementEntities). If the login is successful, it opens the PropertyManagement window.

Property Management:
The PropertyManagement window allows the logged-in agent to manage properties. The properties are associated with agents, and the application filters the properties based on the logged-in agent's ID.
The window displays a ComboBox (cboPropertyList) containing properties associated with the logged-in agent.
When a property is selected from the ComboBox, its details are displayed in TextBoxes, and radio buttons indicate the listing status (sold, for sale, or for rent).
