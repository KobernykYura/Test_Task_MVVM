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
        private int index;
        private Student selectedStudent;
        public ObservableCollection<Student> Students { get; set; }
        SerializationXML xML;

        /// <summary>
        /// Свойство выбранного элемента.
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

            foreach (var item in xML.getDocumentData())
            {
                Students.Add(item);
            }
        }
        /// <summary>
        /// Добавление в коллекцию ObservableCollection
        /// </summary>
        /// <param name="st">Объект для добавления</param>
        public void Add(Student st)
        {
            if (st != null)
            {
                // Генерация Id для нового объекта
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
                xML.getInDocumentAdd(st);
            }
        }
        /// <summary>
        /// Удаление выбранного объекта из модели и коллекции
        /// </summary>
        public void Delete()
        {
            if (selectedStudent != null)
            {
                xML.getInDocumentDelete(SelectedStudent);
                Students.Remove(SelectedStudent);
            }
        }
        /// <summary>
        /// Изменение объекта в модели и коллекции
        /// </summary>
        /// <param name="st"></param>
        public void Edit(Student st)
        {
            int index = Students.IndexOf(st);

            Students.RemoveAt(index);
            Students.Insert(index, st);

            SelectedStudent = st;
            xML.getInDocumentEdit(selectedStudent);

        }

        /// <summary>
        /// Событие изменения коллекции
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Метод вызова изменений.
        /// </summary>
        /// <param name="prop">Имя изменяемого свойства</param>
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
