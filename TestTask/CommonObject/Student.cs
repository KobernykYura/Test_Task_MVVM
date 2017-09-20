using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using CommonObject;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CommonObject
{
    public class Student : StudentModel, INotifyPropertyChanged, IValidatableObject
    {

        /// <summary>
        /// Свойство поля id;
        /// </summary>
        public int Id {
            get { return id; }
            set { id = value;  OnPropertyChanged("Id"); }
        }
        /// <summary>
        /// Свойство поля firstname;
        /// </summary>
        public string FirstName {
            get { return firstname; }
            set { firstname = value; OnPropertyChanged("FirstName"); }
        }
        /// <summary>
        /// Свойство поля last.
        /// </summary>
        public string Last {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged("LastName"); }
        }
        /// <summary>
        /// Свойство поля age.
        /// </summary>
        public int Age {
            get { return age; }
            set { age = value; OnPropertyChanged("Age"); }
        }
        /// <summary>
        /// Свойство поля gender.
        /// </summary>
        public string Gender {
            get { return gender; }
            set { gender = value; OnPropertyChanged("Gender"); }
        }

        /// <summary>
        /// Свойство представления однострочным выводом имени и фамилии
        /// </summary>
        public string FullName { get { return (firstname +" "+ lastName); } }
        /// <summary>
        /// Свойство представления возраста
        /// </summary>
        public string AgePostfix
        {
            get
            {
                string strAge = age.ToString();
                Regex reg1 = new Regex(@"\d1");
                Regex reg2 = new Regex(@"\d[2-4]");

                if (reg1.IsMatch(strAge)) return (strAge + " год. ");
                else if (reg2.IsMatch(strAge)) return (strAge + " года. ");
                else return(strAge + " лет. ");
            }
        }
        /// <summary>
        /// Свойство представления пола у человека.
        /// </summary>
        public string GenderString
        {
            get
            {
                string g;

                switch (gender)
                {
                    case "0":
                        g = " Мужчина ";
                        break;
                    case "1":
                        g = " Женщина ";
                        break;
                    case "male":
                        g = " Мужчина "; Gender = "0";
                        break;
                    case "female":
                        g = " Женщина "; Gender = "1";
                        break;
                    default:
                        g = " Неизвестный гендер "; Gender = "2";
                        break;
                }
                return g;
            }
        }

        public Student()
        {

        }

        public Student(string firstname, string last, int age, string gender)
        {
            this.FirstName = firstname;
            this.Last = last;
            this.Age = age;
            this.Gender = gender;
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

        /// <summary>
        /// Метод валидациии объекта.
        /// </summary>
        /// <param name="validationContext">Контекст валидации(объект для валидации) по макету интерфейса</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.FirstName))
                errors.Add(new ValidationResult("Не указано имя"));

            if (string.IsNullOrWhiteSpace(this.Last))
                errors.Add(new ValidationResult("Не указана фамилия"));

            if (this.Age <= 0)
                errors.Add(new ValidationResult("Не может быть равным или меньше нуля"));
            else if (this.Age < 16 || this.Age > 100)
                errors.Add(new ValidationResult("Возраст должен быть в промежутке от 16 до 100 лет"));
            
            if (string.IsNullOrWhiteSpace(this.Gender))
                errors.Add(new ValidationResult("Не указан пол"));
            return errors;
        }
    }

}
