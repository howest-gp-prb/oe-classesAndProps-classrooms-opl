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
using Prb.Classrooms.Core;
namespace Prb.Classrooms.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<ClassRoom> classRooms;
        bool isNew;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateFloors();
            PopulateWings();
            DoSeedings();
            PopulateClassRooms();
            ClearControls();
            ViewDefault();
        }
        private void PopulateFloors()
        {
            cmbFloor.Items.Clear();
            cmbFloor.Items.Add((sbyte)-1);
            cmbFloor.Items.Add((sbyte)0);
            cmbFloor.Items.Add((sbyte)1);
            cmbFloor.Items.Add((sbyte)2);
        }
        private void PopulateWings()
        {
            cmbWing.Items.Clear();
            cmbWing.Items.Add('A');
            cmbWing.Items.Add('B');
            cmbWing.Items.Add('C');
            cmbWing.Items.Add('D');
        }
        private void DoSeedings()
        {
            classRooms = new List<ClassRoom>();
            classRooms.Add(new ClassRoom("Practicum scheikunde", 0, 'A', 50, false));
            classRooms.Add(new ClassRoom("Geschiedenis graad 1", 1, 'A', 30, false));
            classRooms.Add(new ClassRoom("Geschiedenis graad 2+3", 1, 'A', 30, false));
            classRooms.Add(new ClassRoom("Informaticalokaal 1", 0, 'B', 50, true));
            classRooms.Add(new ClassRoom("Informaticalokaal 2", 1, 'B', 40, true));
            classRooms.Add(new ClassRoom("Informaticalokaal 3", 2, 'B', 45, true));
            classRooms.Add(new ClassRoom("Refter", 0, 'C', 230, false));

        }
        private void PopulateClassRooms()
        {
            lstClassRooms.ItemsSource = null;
            lstClassRooms.ItemsSource = classRooms;
        }
        private void ClearControls()
        {
            txtName.Text = "";
            txtCapacity.Text = "";
            cmbFloor.SelectedIndex = -1;
            cmbWing.SelectedIndex = -1;
            chkComputerRoom.IsChecked = false;
        }
        private void ViewDefault()
        {
            grpDetails.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
            btnNew.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Visible;
            lstClassRooms.IsEnabled = true;
        }
        private void ViewNewEdit()
        {
            grpDetails.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            btnNew.Visibility = Visibility.Hidden;
            btnEdit.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
            lstClassRooms.IsEnabled = false;
        }



        private void lstClassRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if (lstClassRooms.SelectedItem == null)
                return;

            ClassRoom classRoom = (ClassRoom)lstClassRooms.SelectedItem;
            txtName.Text = classRoom.Name;
            txtCapacity.Text = classRoom.Capacity.ToString();
            cmbFloor.SelectedItem = classRoom.Floor;
            cmbWing.SelectedItem = classRoom.Wing;
            chkComputerRoom.IsChecked = classRoom.IsComputerClassRoom;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            ClearControls();
            ViewNewEdit();
            txtName.Focus();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstClassRooms.SelectedItem == null)
                return;

            isNew = false;
            ViewNewEdit();
            txtName.Focus();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstClassRooms.SelectedItem == null)
                return;

            if(MessageBox.Show("Wissen","Opgepast",MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                classRooms.Remove((ClassRoom)lstClassRooms.SelectedItem);
                PopulateClassRooms();
                ClearControls();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            sbyte floor;
            if (cmbFloor.SelectedItem == null)
                floor = (sbyte)0;
            else
                floor = sbyte.Parse(cmbFloor.SelectedItem.ToString());
            char wing = ' ';
            if(cmbWing.SelectedItem != null)
                wing = char.Parse(cmbWing.SelectedItem.ToString());
            int capacity ;
            int.TryParse(txtCapacity.Text, out capacity);
            bool isComputerClassRoom = (bool)chkComputerRoom.IsChecked;

            ClassRoom classRoom;
            if (isNew)
            {
                classRoom = new ClassRoom(name, floor, wing, capacity, isComputerClassRoom);
                classRooms.Add(classRoom);
            }
            else
            {
                classRoom = (ClassRoom)lstClassRooms.SelectedItem;
                classRoom.Name = name;
                classRoom.Floor = floor;
                classRoom.Capacity = capacity;
                classRoom.Wing = wing;
                classRoom.IsComputerClassRoom = isComputerClassRoom;
            }
            PopulateClassRooms();
            lstClassRooms.SelectedItem = classRoom;
            ViewDefault();
            ClearControls();
            if (lstClassRooms.SelectedIndex > -1)
            {
                lstClassRooms_SelectionChanged(null, null);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ViewDefault();
            ClearControls();
            if(lstClassRooms.SelectedIndex > -1)
            {
                lstClassRooms_SelectionChanged(null, null);
            }
        }
    }
}
