using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using CommonObject;

namespace Model
{
    public class SerializationXML
    {
        static string path = "../../../Model/Students.xml";
        private Student[] studentsArray;

        public SerializationXML()
        {

        }

        /// <summary>
        /// Десериализует массив студентов из XML файла в массив.
        /// </summary>
        /// <returns>Возвращает список студентов.</returns>
        public Student[] getDocumentData()
        {
            
            var document = XElement.Load(path);
            var collection = from elem in document.Descendants("Student") select new Student {
                Id = Convert.ToInt32( elem.Attribute("Id").Value),
                FirstName = elem.Element("FirstName").Value,
                Last = elem.Element("Last").Value,
                Age =Convert.ToInt32( elem.Element("Age").Value),
                Gender = elem.Element("Gender").Value
            };
            studentsArray = collection.Cast<Student>().ToArray();
            
            return studentsArray;
        }

        /// <summary>
        /// Сериализует объект в XML файл
        /// </summary>
        /// <param name="student">Объект для сериализации.</param>
        public void getInDocumentAdd(Student student)
        {
            XDocument xdoc = XDocument.Load(path);
            XElement root = xdoc.Element("Students");

            root.Add(new XElement("Student",
            new XAttribute("Id", student.Id),
            new XElement("FirstName", student.FirstName),
            new XElement("Last", student.Last),
            new XElement("Age", student.Age),
            new XElement("Gender", student.Gender)));
            xdoc.Save(path);
        }
   
        /// <summary>
        /// Изменяет значения выбранного объекта в XML файле
        /// </summary>
        /// <param name="student">Объект для изминения.</param>
        public void getInDocumentEdit(Student student)
        {
            XDocument xdoc = XDocument.Load(path);
            XElement root = xdoc.Element("Students");

            foreach (XElement xe in root.Elements("Student").ToList())
            {
                if (xe.Attribute("Id").Value == student.Id.ToString())
                {
                    xe.Attribute("Id").Value = student.Id.ToString();
                    xe.Element("FirstName").Value = student.FirstName;
                    xe.Element("Last").Value = student.Last;
                    xe.Element("Age").Value = student.Age.ToString();
                    xe.Element("Gender").Value = student.Gender;
                }
            }
            xdoc.Save(path);
        }

        /// <summary>
        /// Удаляет объект из XML файла 
        /// </summary>
        /// <param name="student">Удаляемый объект.</param>
        public void getInDocumentDelete(Student student)
        {
            XDocument xdoc = XDocument.Load(path);
            XElement root = xdoc.Element("Students");

            foreach (XElement xe in root.Elements("Student").ToList())
            {
                if (xe.Attribute("Id").Value == student.Id.ToString())
                {
                    xe.Remove();
                }
            }
            xdoc.Save(path);
        }
    }
}
