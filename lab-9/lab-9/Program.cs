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
    delegate void menu();
    menu[] menuOption;
    List<String> menuRegistroCivil;
    List<Boolean> optionMenu;
    int primeraPersona;
    List<ClassLibrary1.Person> people;
    List<ClassLibrary1.Address> addresses;
    List<ClassLibrary1.Car> cars;


    static void Main(string[] args)
    {
      Console.WriteLine("Desea escanear ClassLibrary1.dll? (s/n)");
      if (Console.ReadLine() == "s")
        ScanDLL();
      else{
        Program temp = new Program();
        temp.Lab9();
      }
    }

    public Program(){
      
      menuRegistroCivil = new List<String>() { "Inscribir persona", "Inscribir propiedad", "Inscribir automóvil", "Mostrar las personas", "Salir" };
      optionMenu = CreateList(menuRegistroCivil.Count);
      menuOption = new menu[] { InscribirPersona, InscribirPropiedad, InscribirAuto, ShowPeople };
      primeraPersona = 1;
      people = new List<ClassLibrary1.Person>();
      addresses = new List<ClassLibrary1.Address>();
      cars = new List<ClassLibrary1.Car>();
    }

    public void Lab9(){
      int option = LoadMenu(menuRegistroCivil);
      while(option != menuOption.Length){
        if (option >= menuOption.Length)
          Console.WriteLine("Opción no permitida.");
        else{
          menuOption[option]();
        }
        option = LoadMenu(menuRegistroCivil);
      }
    }

    public int LoadMenu(List<String> menuRegistroCivil){
      int i = 1;
      Console.WriteLine("Seleccione una opción:");
      foreach (String index in menuRegistroCivil)
        Console.Write("(" + (i++) + ") " + index + "\n");
      return Int32.Parse(Console.ReadLine()) - 1;
    }

    void InscribirPersona(){
      Console.Write("Ingrese el nombre de la persona ");
      String first_name = Console.ReadLine();
      Console.Write("Ingrese el apellido de la persona ");
      String last_name = Console.ReadLine();
      Console.Write("Ingrese el rut de la persona ");
      String rut = Console.ReadLine();
      Console.Write("Ingrese el año de nacimiento ");
      int ano = Int32.Parse(Console.ReadLine());
      Console.Write("Ingrese el mes de nacimiento ");
      int mes = Int32.Parse(Console.ReadLine());
      Console.Write("Ingrese el dia de nacimiento ");
      int dia = Int32.Parse(Console.ReadLine());
      DateTime birth_date = new DateTime(ano, mes, dia);
      if (primeraPersona == 1){
        // No tiene parents
        people.Add(new ClassLibrary1.Person(first_name, last_name, birth_date, null, rut, null, null));
      }
      else // Falta implementar pedir padres
        people.Add(new ClassLibrary1.Person(first_name, last_name, birth_date, null, rut, null, null));
    }

    void InscribirPropiedad(){
      Console.Write("Ingrese la calle ");
      String street = Console.ReadLine();
      Console.Write("Ingrese el número ");
      int nro = Int32.Parse(Console.ReadLine());
      Console.Write("Ingrese la comuna ");
      String commune = Console.ReadLine();
      Console.Write("Ingrese la ciudad ");
      String city = Console.ReadLine();
      Console.Write("Ingrese el año de construcción ");
      int year_of_construction = Int32.Parse(Console.ReadLine());
      Console.Write("Ingrese la cantidad de baños ");
      int bathrooms = Int32.Parse(Console.ReadLine());
      Console.Write("Ingrese la cantidad de piezas ");
      int bedrooms = Int32.Parse(Console.ReadLine());
      Console.Write("Tiene patio trasero? (s/n)");
      Boolean backyard = false;
      if (Console.ReadLine() == "s")
        backyard = true;
      Console.Write("Tiene piscina? (s/n)");
      Boolean pool = false;
      if (Console.ReadLine() == "s")
        pool = true;
      addresses.Add(new ClassLibrary1.Address(street, nro, commune, city, null, year_of_construction, bedrooms, bathrooms, backyard, pool));
    }

    void InscribirAuto(){
      
    }

    private void ShowPeople(){
      foreach(ClassLibrary1.Person p in people){
        Console.WriteLine(p.First_name + " " + p.Last_name);
      }
    }

    private List<Boolean> CreateList(int Length){
      List<Boolean> options = new List<bool>(Length);
      for (int i = 0; i < Length; i++)
        options.Add(false);
      return options;
    }

    static void ScanDLL()
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
