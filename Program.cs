using System.Reflection;
using System.Text;

namespace Homework_7
{
    [AttributeUsage(AttributeTargets.Property)]
    class CustomNameAttribute : Attribute
    {
        public string CustomFieldName { get; private set; }

        public CustomNameAttribute(string customFieldName)
        {
            CustomFieldName = customFieldName;
        }
    }
    class TestClass
    {
        [CustomName("CustomFieldName")]
        public int I { get; set; }

        //[DontSaveAttribute]
        public string S { get; set; }


        public decimal D { get; set; }
        public char[] C { get; set; }

        public TestClass()
        { }
        private TestClass(int i)
        {
            this.I = i;
        }
        public TestClass(int i, string s, decimal d, char[] c) : this(i)
        {
            this.S = s;
            this.D = d;
            this.C = c;
        }

    }

    internal class Program
    {
        static string ObjectToString(object o)
        {
            StringBuilder sb = new StringBuilder();
            Type t = o.GetType();
            sb.Append(t.AssemblyQualifiedName + ":");
            sb.Append(t.Name + "|");
            var properties = t.GetProperties();


            foreach (var p in properties)
            {
                var value = p.GetValue(o);
                var attributeName = p.Name;

                var customNameAttribute = p.GetCustomAttribute<CustomNameAttribute>();
                if (customNameAttribute != null)
                {
                    attributeName = customNameAttribute.CustomFieldName;
                }

                sb.Append(attributeName + ":");
                if (p.PropertyType == typeof(char[]))
                {
                    sb.Append(new string(value as char[]) + "|");
                }
                else
                    sb.Append(value + "|");

            }
            // type| A:1 |S:"fjldsfj".....

            return sb.ToString();

        }




        static void Main(string[] args)
        {
            // Реализуйте атрибут работающий совместно с предыдущим методом сохранения в строку.
            // Помеченное этим атрибутом свойство не должно подлежать сохранению(пропускается).
            // Для проверки пометить атрибутом любой свойство класса и убедитесь что оно не сохраняется. 

            TestClass test = new TestClass(1, "строка", 2.0m, new char[] { 'A', 'B', 'C' });

            string str = ObjectToString(test);

            Console.WriteLine("Наш класс: " + str);
            Console.ReadKey();



        }
    }
}