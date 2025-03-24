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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task1
{
    public struct Person
    {
        private static int counter = 1; // Auto-incrementing ID
        public int ID { get; private set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public Person(string name, int age, string address)
        {
            ID = counter++; // Assign auto-incremented ID
            Name = name;
            Age = age;
            Address = address;
        }
    }

    public partial class MainWindow : Window
    {
        private Person[] people = new Person[100]; // Fixed-size array
        private int count = 0; // Track number of elements

        public MainWindow()
        {
            InitializeComponent();
        }

        // Add a new person
        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtAddress.Text) || !int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Please enter valid data.");
                return;
            }

            people[count++] = new Person(txtName.Text, age, txtAddress.Text);
            MessageBox.Show("Person added successfully!");
            DisplayPeople();
        }

        // Display all people
        private void DisplayPeople()
        {
            lstPeople.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                lstPeople.Items.Add($"Id: {people[i].ID}, Name: {people[i].Name}, Age: {people[i].Age}, Address: {people[i].Address}");
            }
        }

        // Sort by Age
        private void SortByAge_Click(object sender, RoutedEventArgs e)
        {
            Array.Sort(people, 0, count, Comparer<Person>.Create((a, b) => a.Age.CompareTo(b.Age)));
            DisplayPeople();
        }

        // Sort by Name
        private void SortByName_Click(object sender, RoutedEventArgs e)
        {
            Array.Sort(people, 0, count, Comparer<Person>.Create((a, b) => a.Name.CompareTo(b.Name)));
            DisplayPeople();
        }

        // Search by Age
        private void SearchByAge_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtSearchAge.Text, out int searchAge))
            {
                MessageBox.Show("Please enter a valid age.");
                return;
            }

            lstPeople.Items.Clear();
            foreach (var person in people.Take(count).Where(p => p.Age == searchAge))
            {
                lstPeople.Items.Add($"Id: {person.ID}, Name: {person.Name}, Age: {person.Age}, Address: {person.Address}");
            }
        }

        // Search by Name
        private void SearchByName_Click(object sender, RoutedEventArgs e)
        {
            string searchName = txtSearchName.Text.Trim().ToLower();
            lstPeople.Items.Clear();
            foreach (var person in people.Take(count).Where(p => p.Name.ToLower() == searchName))
            {
                lstPeople.Items.Add($"Id: {person.ID}, Name: {person.Name}, Age: {person.Age}, Address: {person.Address}");
            }
        }

        // Delete by Name
        private void DeleteByName_Click(object sender, RoutedEventArgs e)
        {
            string nameToDelete = txtDeleteName.Text.Trim().ToLower();
            int index = Array.FindIndex(people, 0, count, p => p.Name.ToLower() == nameToDelete);

            if (index >= 0)
            {
                for (int i = index; i < count - 1; i++)
                {
                    people[i] = people[i + 1];
                }
                count--;
                MessageBox.Show("Person deleted successfully.");
                DisplayPeople();
            }
            else
            {
                MessageBox.Show("Person not found.");
            }
        }

        // Delete by Age
        private void DeleteByAge_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtDeleteAge.Text, out int ageToDelete))
            {
                MessageBox.Show("Please enter a valid age.");
                return;
            }

            int index = Array.FindIndex(people, 0, count, p => p.Age == ageToDelete);
            if (index >= 0)
            {
                for (int i = index; i < count - 1; i++)
                {
                    people[i] = people[i + 1];
                }
                count--;
                MessageBox.Show("Person deleted successfully.");
                DisplayPeople();
            }
            else
            {
                MessageBox.Show("Person not found.");
            }
        }

        // Display students

        private void DisplayAll_Click(object sender, RoutedEventArgs e)
        {
            lstPeople.Items.Clear(); // Clear the list before displaying

            if (count == 0)
            {
                MessageBox.Show("No students available.");
                return;
            }

            // Sort the array by ID before displaying
            Array.Sort(people, 0, count, Comparer<Person>.Create((a, b) => a.ID.CompareTo(b.ID)));

            for (int i = 0; i < count; i++)
            {
                lstPeople.Items.Add($"Id: {people[i].ID}, Name: {people[i].Name}, Age: {people[i].Age}, Address: {people[i].Address}");
            }
        }

    }
}
