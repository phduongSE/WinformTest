using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformTester.Models
{
    public class Person
    {
        public Person(int i)
        {
            Id = i;
            ToInitString(i);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
        public string Col4 { get; set; }
        public string Col5 { get; set; }
        public string Col6 { get; set; }
        public string Col7 { get; set; }
        public string Col8 { get; set; }
        public string Col9 { get; set; }
        public string Col10 { get; set; }
        public string Col11 { get; set; }
        public string Col12 { get; set; }
        public string Col13 { get; set; }
        public string Col14 { get; set; }
        public string Col15 { get; set; }
        public string Col16 { get; set; }
        public string Col17 { get; set; }
        public string Col18 { get; set; }
        public string Col19 { get; set; }
        public string Col20 { get; set; }
        public string Col21 { get; set; }
        public string Col22 { get; set; }
        public string Col23 { get; set; }
        public string Col24 { get; set; }
        public string Col25 { get; set; }
        public string Col26 { get; set; }
        public string Col27 { get; set; }
        public string Col28 { get; set; }
        public string Col29 { get; set; }
        public string Col30 { get; set; }

        public void ToUpperString()
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    var toUpper = prop.PropertyType.GetMethod("ToUpper", new Type[] { });
                    prop.SetValue(this, toUpper.Invoke(prop.GetValue(this), null));
                }
            }
        }

        public void ToLowerString()
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    var toLower = prop.PropertyType.GetMethod("ToLower", new Type[] { });
                    prop.SetValue(this, toLower.Invoke(prop.GetValue(this), null));
                }
            }
        }

        private void ToInitString(int i)
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(this, $"{prop.Name} - {i}");
                }
            }
        }
    }
}
