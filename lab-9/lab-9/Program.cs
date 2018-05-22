using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace lab_9
{
    class Program
    {
        static void Main(string[] args)
        {
            String fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ClassLibrary1.dll");
            Assembly archivosAssembly = Assembly.LoadFile(fileName);
            foreach (Type type in archivosAssembly.GetTypes())
            {

                //get the PropertyInfo array for all properties
                PropertyInfo[] properties = type.GetProperties();
                MethodInfo[] methods = type.GetMethods();

                string sb = "";

                sb += ("================================================================\n");
                sb += (String.Format("Type Name: {0}\n", type.Name));
                if (type.IsClass)
                    sb += "CLASS\n";
                else if (type.IsInterface)
                    sb += "INTERFACE\n";
                sb += ("================================================================\n");

                //iterate the properties and write output
                foreach (PropertyInfo property in properties)
                {
                    sb += ("----------------------------------------------------------------\n");
                    sb += (String.Format("Property Name: {0}\n", property.Name));
                    sb += ("----------------------------------------------------------------\n");

                    sb += (String.Format("Property Type Name: {0}\n", property.PropertyType.Name));
                    sb += (String.Format("Property Type FullName: {0}\n", property.PropertyType.FullName));

                    sb += (String.Format("Can we read the property?: {0}\n", property.CanRead.ToString()));
                    sb += (String.Format("Can we write the property?: {0}\n", property.CanWrite.ToString()));
                }
                sb += ("******************************\n");
                //iterate the methods and write output
                if (methods.Length > 0)
                {
                    foreach (MethodInfo method in methods)
                    {
                        try
                        {
                            sb += (String.Format("Method info: {0}\n", type.GetMethod(method.Name)));
                        }
                        catch
                        {
                            sb += (String.Format("Method info: {0} {1} ()\n", method.ReturnType, method.Name.ToString()));
                        }
                        sb += ("***\n");
                    }
                }
                Console.WriteLine(sb);
            }
            Console.ReadKey();
        }
    }
}
