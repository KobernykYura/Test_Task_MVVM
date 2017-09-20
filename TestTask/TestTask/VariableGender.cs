using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    class VariableGender
    {
        public static Dictionary<string, string> Genders
        {
            get { return dicGenders; }
        }
        private static Dictionary<string, string> dicGenders = new Dictionary<string, string>()
        {
            { "male","0" },
            { "female","1" }            
        };
    }
}
