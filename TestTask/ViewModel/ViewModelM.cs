using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommonObject;
using Model;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class ViewModelM : INotifyPropertyChanged
    {
        //private int index;
        private Student selectedStudent;
        //private Student newStudent;

        private RelayCommand addCommand;
        private RelayCommand deleteCommand;
        private RelayCommand editCommand;

        public ObservableCollection<Student> Students { get; set; }
        SerializationXML xML;
        IDialogService dialogService;

        /// <summary>
        /// The property of the selected item.
        /// </summary>
        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set { selectedStudent = value; OnPropertyChanged("SelectedStudent"); }
        }

        //public Student NewStudent
        //{
        //    get { return newStudent; }
        //    set { newStudent = value; OnPropertyChanged("NewStudent"); }
        //}

        public ViewModelM()
        {
            Students = new ObservableCollection<Student>();
            xML = new SerializationXML();
            dialogService = new DialogService();

            foreach (var item in xML.getStudents())
            {
                Students.Add(item);
            }
        }
        /// <summary>
        /// Adding to the ObservableCollection
        /// </summary>
        /// <param name="st">Object to add</param>
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      SelectedStudent = obj as Student;

                      if (selectedStudent == null)
                      {
                          dialogService.ShowMessage("Заполните данные нового студента");
                          return;
                      }
                      //Validation of values
                      var results = new List<ValidationResult>();
                      var context = new ValidationContext(SelectedStudent);
                      if (!Validator.TryValidateObject(SelectedStudent, context, results, true))
                      {
                          foreach (var error in results)
                          {
                              dialogService.ShowMessage(error.ErrorMessage.ToString());
                          }
                          return;
                      }

                      //if (newStudent != null)
                      //{
                          // Generating an Id for a new object
                          foreach (var item in Students)
                          {
                              if (SelectedStudent.Id == item.Id)
                              {
                              SelectedStudent.Id++;
                              }
                              else break;
                          }

                          Students.Insert(SelectedStudent.Id, SelectedStudent);
                          SelectedStudent = SelectedStudent;
                          xML.addStudent(SelectedStudent);
                      //}
                  }));
            }
        }


        /// <summary>
        /// Removing the selected object from the model and collection
        /// </summary>
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(obj =>
                  {
                      if (selectedStudent == null)
                      {
                          dialogService.ShowMessage("Выберите элемент для редактирования");
                          return;
                      }
                      if (selectedStudent != null)
                      {
                          xML.deleteStudent(SelectedStudent);
                          Students.Remove(SelectedStudent);
                      }
                  }));
            }
        }

        /// <summary>
        /// Modifying an object in a model and collection
        /// </summary>
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand(obj =>
                  {
                      Student student = obj as Student;

                      if (student == null)
                      {
                          dialogService.ShowMessage("Выберите элемент для редактирования");
                          return;
                      }

                      //Validation of values
                      var results = new List<ValidationResult>();
                      var context = new ValidationContext(student);
                      if (!Validator.TryValidateObject(student, context, results, true))
                      {
                          foreach (var error in results)
                          {
                              dialogService.ShowMessage(error.ErrorMessage.ToString());
                          }
                          return;
                      }

                      int index = Students.IndexOf(student);

                      Students.RemoveAt(index);
                      Students.Insert(index, student);

                      SelectedStudent = student;
                      xML.updateStudent(selectedStudent);
                  }));
            }
        }

        /// <summary>
        /// Change collection event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Method of calling changes.
        /// </summary>
        /// <param name="prop">The name of the property to be changed</param>
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
