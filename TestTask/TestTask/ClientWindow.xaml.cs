using CommonObject;
using System.Windows;
using ViewModel;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
            DataContext = new ViewModelM();
        }
        /// <summary>
        /// Click event to add button
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">event information</param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            WindowAdd win = new WindowAdd();
            win.Owner = this;
            win.ShowDialog();

            //Checking the added object
            if (win.studObj != null || win.studObj.FirstName!= null || win.studObj.Last != null || win.studObj.Age != default(int))
            {
                ((ViewModelM)DataContext).Add(win.studObj);
            }
        }
        /// <summary>
        /// Click event to delete button
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">event information</param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Student stud = ((ViewModelM)DataContext).SelectedStudent;

            //Check whether the item in the list is selected for deletion 
            if (stud == null)
            {
                MessageBox.Show("Выберите элемент для удаления");
                return;
            }
            //Confirmation Dialog Box
            var res = MessageBox.Show(this, "Вы уверены в удалении студента?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (MessageBoxResult.Yes == res)
            {
                ((ViewModelM)DataContext).Delete();
            }
        }
        /// <summary>
        /// Click event edit button
        /// </summary>
        /// <param name="sender">objwct</param>
        /// <param name="e">event information</param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Student stud = ((ViewModelM)DataContext).SelectedStudent;

            //Check if the item in the list is selected for editing
            if (stud == null)
            {
                MessageBox.Show("Выберите элемент для редактирования");
                return;
            }
            WindowAdd win = new WindowAdd(stud);
            win.Owner = this;
            win.ShowDialog();

           ((ViewModelM)DataContext).Edit(win.studObj);
        }

    }
}
