using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommonObject;
using Model;
using System.Windows;

namespace ViewModel
{
    public class ViewModelM : INotifyPropertyChanged
    {
        //private int index;
        private Student selectedStudent;
        public ObservableCollection<Student> Students { get; set; }
        SerializationXML xML;

        /// <summary>
        /// The property of the selected item.
        /// </summary>
        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set { selectedStudent = value; OnPropertyChanged("SelectedStudent"); }
        }

        public ViewModelM()
        {
            Students = new ObservableCollection<Student>();
            xML = new SerializationXML();

            foreach (var item in xML.getStudents())
            {
                Students.Add(item);
            }
        }
        /// <summary>
        /// Adding to the ObservableCollection
        /// </summary>
        /// <param name="st">Object to add</param>
        public void Add(Student st)
        {
            if (st != null)
            {
                // Generating an Id for a new object
                foreach (var item in Students)
                {
                    if (st.Id == item.Id)
                    {
                        st.Id++;
                    }
                    else break;
                }

                Students.Insert(st.Id, st);
                SelectedStudent = st;
                xML.addStudent(st);
            }
        }
        /// <summary>
        /// Removing the selected object from the model and collection
        /// </summary>
        public void Delete()
        {
            if (selectedStudent != null)
            {
                xML.deleteStudent(SelectedStudent);
                Students.Remove(SelectedStudent);
            }
        }
        /// <summary>
        /// Modifying an object in a model and collection
        /// </summary>
        /// <param name="st"></param>
        public void Edit(Student st)
        {
            int index = Students.IndexOf(st);

            Students.RemoveAt(index);
            Students.Insert(index, st);

            SelectedStudent = st;
            xML.updateStudent(selectedStudent);

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
