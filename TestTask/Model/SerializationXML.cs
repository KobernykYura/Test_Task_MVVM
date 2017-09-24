using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using CommonObject;

namespace Model
{
    public class SerializationXML
    {
        static string path = "Students.xml"; //../../../Model/
        private Student[] studentsArray;

        public SerializationXML()
        {

        }

        /// <summary>
        /// Deserializes an array of students from an XML file into an array.
        /// </summary>
        /// <returns>Returns the list of students.</returns>
        public Student[] getStudents()
        {
            XElement document;
            try
            {
                document = XElement.Load(path);
            }
            catch (Exception)
            {
                    throw ;
            }
            
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
        /// Serialize an object into an XML file
        /// </summary>
        /// <param name="student">The object for serialization.</param>
        public void addStudent(Student student)
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
        /// Changes the values of the selected object in an XML file
        /// </summary>
        /// <param name="student">The object to modify.</param>
        public void updateStudent(Student student)
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
        /// Removes an object from an XML file 
        /// </summary>
        /// <param name="student">The object to be deleted.</param>
        public void deleteStudent(Student student)
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
