namespace BrookerCompany.DataSeeder
{
    using BrookerCompany.Models;
    using BrookerCompany.Data;

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Numerics;
    using BrookerCompany.Services;

    namespace Brooker.Company.DataSeeder
    {
        public class Program
        {

            private static BrookerService bService = new BrookerService();
            private static TownService tService = new TownService();
            private static AddressService aService = new AddressService();
            private static ClientService cService = new ClientService();
            private static ProjectService pService = new ProjectService();
            private static DepartmentService dService = new DepartmentService();
            static void Main()
            {
                SeedTowns();
                SeedDepartments();
                SeedAddress();
                SeedBrooker();
                SeedClientAddress();
                SeedClient();

            }
            public static void SeedTowns()
            {
                try
                {
                    List<string> t = new List<string>();
                    t.Add("Rome");
                    t.Add("Bangkok");
                    t.Add("Paris");
                    t.Add("London");
                    t.Add("New York");
                    t.Add("Seoul");
                    t.Add("Oslo");
                    t.Add("Barselona");
                    t.Add("Hong Kong");
                    t.Add("Sofia");
                    t.Add("Istanbul");


                    for (int i = 0; i < t.Count; i++)
                    {
                        string name = t[i];
                        Console.WriteLine(tService.AddTown(name));
                    }
                }
                catch (Exception)
                {

                }

            }
            public static void SeedDepartments()
            {
                try
                {
                    List<string> departmentNames = new List<string>();
                    departmentNames.Add("Sales and Trading");
                    departmentNames.Add("Research and Analysis");
                    departmentNames.Add("Investment Banking");
                    departmentNames.Add("Client Services");
                    departmentNames.Add("Risk Management and Compliance");
                    departmentNames.Add("Technology and Operations");
                    departmentNames.Add("Private Client Services");
                    departmentNames.Add("Marketing and Business Development");
                    departmentNames.Add("Prime Brokerage Services");

                    for (int i = 0; i < departmentNames.Count; i++)
                    {
                        string name = departmentNames[i];
                        Console.WriteLine(dService.AddDepartment(name));
                    }
                }
                catch (Exception)
                {
                    
                }
            }

            public static void SeedAddress()
            {
                try
                {
                    List<string> a = new List<string>();

                    a.Add("Main Street 7");
                    a.Add("Park Avenue 1 ");
                    a.Add("Broadway 12");
                    a.Add("Oak Street 9");
                    a.Add("High Street 7");
                    a.Add("Maple Avenue 28");
                    a.Add("Market Street 9");
                    a.Add("Pine Street 4");
                    a.Add("Church Street 3");
                    a.Add("King Street 5");
                    a.Add("Rodeo Drive 13");
                    a.Add("Mongolia Avenue 14");
                    a.Add("Via del Corso 15");
                    a.Add("Via dei Fori 16");
                    a.Add("Via Vento 8");
                    a.Add("Via Appia Antica 10");

                    for (int i = 0; i < a.Count; i++)
                    {
                        string addressname = a[i];
                        List<string> towns = tService.GetTownsNames();
                        Console.WriteLine(aService.AddAddress(addressname, towns[i]));
                    }
                }
                catch (Exception)
                {

                }

            }
            public static void SeedClientAddress()
            {
                try
                {
                    List<string> a = new List<string>();

                    a.Add("Harleston Street");
                    a.Add("Jetta Way");
                    a.Add("Joiner Place");
                    a.Add("Medford Place");
                    a.Add("Millhorn Loop");
                    a.Add("Odell Circle");
                    a.Add("Eaton Terrace");
                    a.Add("Foxbridge Terrace");
                    a.Add("Franklin Court");
                    a.Add("Guerra Avenue");
                    a.Add("Harston Trail");

                    for (int i = 0; i < a.Count; i++)
                    {
                        string addressname = a[i];
                        List<string> towns = tService.GetTownsNames();
                        Console.WriteLine(aService.AddAddress(addressname, towns[i]));
                    }
                }
                catch (Exception)
                {

                }

            }
            public static void SeedClient()
            {
                try
                {
                    Console.WriteLine(cService.AddClient("Daniel", "Gregory", "Abana Path", "Paris", "0888552200", "daniel_12@gmail.com"));
                    Console.WriteLine(cService.AddClient("Olivia", "Rodrigo", "Odell Circle", "Barcelona", "0833333333", "oliviarodrigo1@abv.bg"));
                    Console.WriteLine(cService.AddClient("Rudy", "Pankow", "Harleston Street", "Berlin", "0777777777", "pankow@abv.bg"));
                    Console.WriteLine(cService.AddClient("Maddison", "Beyly", " Guerra Avenue", "Paris", "0123456789", "mady@abv.bg"));
                    Console.WriteLine(cService.AddClient("Chase", "Stocks", "Foxbridge Terrace", "Sofia", "7894561230", "stocks@abv.bg"));
                    Console.WriteLine(cService.AddClient("Tom", "Pedres", "Eaton Terrace", "Bangkok", "0111111111", "tpedres@abv.bg"));
                    Console.WriteLine(cService.AddClient("Carlos", "Stone", "Jetta Way", "Paris", "0222225555", "carlos2gmail.com"));
                    Console.WriteLine(cService.AddClient("Jansy", "Dere", "Harston Trail", "Istanbul", "9023201562", "dere@gmail.com"));


                }
                catch (Exception)
                {

                }

            }
            public static void SeedBrooker()
            {
                try
                {
                    Console.WriteLine(bService.AddBrooker("Kiara", "Cariera", "Rodeo Drive 13", "New York", "Sales and Trading", "0054703620", "kiara@gmail.com"));
                    Console.WriteLine(bService.AddBrooker("Shara", "Cameron", "Main Street 7", "Seoul", "Research and Analysis", "0829654785", "scameron@gmail.com"));
                    Console.WriteLine(bService.AddBrooker("JJ", "Maybank", "High Street 7", "Barcelona", "Investment Banking", "0874856921", "maybank@abv.bg"));
                    Console.WriteLine(bService.AddBrooker("Jhonatan Daivis", " Pine Street 4", "Park Avenue 9", "Hong Kong", "Client Services", "0874793624", "jonaton_dv@gmail.com"));
                    Console.WriteLine(bService.AddBrooker("Rafe", "Cameron", "Broadway 12", "Oslo", "Risk Management and Compliance", "0857936241", "rafecamaron_j@gmail.com"));
                    Console.WriteLine(bService.AddBrooker("Drew", "Starkey", "Oak Street 9", " Sofia", "Technology and Operations", "0147936248", "drew_s@gmail.com"));
                    Console.WriteLine(bService.AddBrooker("Madelyn", "Clain", "Maple Avenue 28", "Rome", "Private Client Services", "0547936248", "madelin_clain@gmail.com"));
                    Console.WriteLine(bService.AddBrooker("Selena", "Beer", "Market Street 9", "Bangkok", "Marketing and Business Development", "3547936248", "selena_beer@gmail.com"));
                    Console.WriteLine(bService.AddBrooker(" Tate", "MacRea", "Via del Corso 15", "Istanbul", "Prime Brokerage Services", "55547936248", "tate_mc@gmail.com"));
                    Console.WriteLine(bService.AddBrooker("Sebastian", "Garcia", "Church Street 3", "Parise", "Residential Architecture", "8547936111", "architect_garcia@gmail.com"));
                    Console.WriteLine(bService.AddBrooker("Zane", "Miller", "Mongolia Avenue", "London", "Sales and Trading", "085479388", "architect_miller@gmail.com"));

                }
                catch (Exception)
                {


                }
            }
          


        }




    }
}






