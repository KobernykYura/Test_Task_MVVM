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
using CommonObject;
using ViewModel;
using System.ComponentModel.DataAnnotations;

namespace TestTask
{
    /// <summary>
    /// Логика взаимодействия для WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        public Student studObj;

        /// <summary>
        /// Конструктор для добавления объекта
        /// </summary>
        public WindowAdd()
        {
            InitializeComponent();

            cbGender.ItemsSource = VariableGender.Genders;
            cbGender.DisplayMemberPath = "Key";
            cbGender.SelectedValuePath = "Value";

            DataContext = new Student();
        }
        /// <summary>
        /// Конструктор для редактирования объекта
        /// </summary>
        /// <param name="stud">Объект редактирования</param>
        public WindowAdd(Student stud)
        {
            InitializeComponent();

            cbGender.ItemsSource = VariableGender.Genders;
            cbGender.DisplayMemberPath = "Key";
            cbGender.SelectedValuePath = "Value";

            DataContext = stud;
        }
        /// <summary>
        /// Событие сохранения объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            studObj = (Student)DataContext;

            //Валидация значений
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var context = new ValidationContext(studObj);
            if (!Validator.TryValidateObject(studObj, context, results, true))
            {
                foreach (var error in results)
                {
                    MessageBox.Show(error.ErrorMessage.ToString());
                }
                return;
            }

            this.Close();
        }
    }
}
