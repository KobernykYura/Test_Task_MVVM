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
        /// Property of the id field;
        /// </summary>
        public int Id {
            get { return id; }
            set { id = value;  OnPropertyChanged("Id"); }
        }
        /// <summary>
        /// Property of the firstname field;
        /// </summary>
        public string FirstName {
            get { return firstname; }
            set { firstname = value; OnPropertyChanged("FirstName"); }
        }
        /// <summary>
        /// Property of the last field;
        /// </summary>
        public string Last {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged("LastName"); }
        }
        /// <summary>
        /// Property of the age field;
        /// </summary>
        public int Age {
            get { return age; }
            set { age = value; OnPropertyChanged("Age"); }
        }
        /// <summary>
        /// Property of the gender field;
        /// </summary>
        public string Gender {
            get { return gender; }
            set { gender = value; OnPropertyChanged("Gender"); }
        }

        /// <summary>
        /// The representation property with a single-line output of the name and surname
        /// </summary>
        public string FullName { get { return (firstname +" "+ lastName); } }
        /// <summary>
        /// Age representation property
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
        /// The property of gender representation in man.
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

        /// <summary>
        /// Method of object validation.
        /// </summary>
        /// <param name="validationContext">The validation context (object for validation) by the interface layout</param>
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
