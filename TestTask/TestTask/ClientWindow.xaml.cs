using CommonObject;
using System.Windows;
using ViewModel;

namespace TestTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
            DataContext = new ViewModelM();
        }
        /// <summary>
        /// Событие нажатия кнопки добавить
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">информация по событию</param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            WindowAdd win = new WindowAdd();
            win.Owner = this;
            win.ShowDialog();

            //Проверка добавляемого объекта
            if (win.studObj != null || win.studObj.FirstName!= null || win.studObj.Last != null || win.studObj.Age != default(int))
            {
                ((ViewModelM)DataContext).Add(win.studObj);
            }
        }
        /// <summary>
        /// Событие нажатия кнопки удалить
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">информация по событию</param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Student stud = ((ViewModelM)DataContext).SelectedStudent;

            //Проверка выбран ли для удаления элемент в списке
            if (stud == null)
            {
                MessageBox.Show("Выберите элемент для удаления");
                return;
            }
            //Диалоговое окно подтвержнеия
            var res = MessageBox.Show(this, "Вы уверены в удалении студента?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (MessageBoxResult.Yes == res)
            {
                ((ViewModelM)DataContext).Delete();
            }
        }
        /// <summary>
        /// Событие нажатия кнопки редактировать
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">информация по событию</param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Student stud = ((ViewModelM)DataContext).SelectedStudent;

            //Проверка выбран ли для редактирования элемент в списке
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
