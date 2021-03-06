﻿using System.Collections.Generic;
using System.Windows;
using CommonObject;
using System.ComponentModel.DataAnnotations;

namespace TestTask
{
    /// <summary>
    /// Logic of interaction for WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        public Student studObj;

        /// <summary>
        /// Constructor for adding an object
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
        /// Constructor for editing an object
        /// </summary>
        /// <param name="stud">Editing Object</param>
        public WindowAdd(Student stud)
        {
            InitializeComponent();

            cbGender.ItemsSource = VariableGender.Genders;
            cbGender.DisplayMemberPath = "Key";
            cbGender.SelectedValuePath = "Value";

            DataContext = stud;
        }
        /// <summary>
        /// Object save event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            studObj = (Student)DataContext;

            //Validation of values
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
