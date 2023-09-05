using BrookerCompany.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrookerCompany.ConsoleApp.Controller
{
    public class BrookerController
    {
        private BrookerService es;
        public BrookerController()
        {
            es = new BrookerService();
            Run();
        }
        private void Run()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Commands();
                    Console.Write("> Enter command:");
                    string cmd = Console.ReadLine();
                    switch (cmd)
                    {
                        case "0":
                            return;
                        case "1":
                            PrintBrookersInfo();
                            break;
                        case "2":
                            HireNewBrooker();
                            break;
                        case "3":
                            DeleteBrooker();
                            break;
                        case "4":
                            UpdateBrookerPhoneNumber();
                            break;
                        case "5":
                            GetExactBrookerInfo();
                            break;
                        default:
                            Console.WriteLine("Invalid command!");
                            WaitPressKey();

                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    WaitPressKey();
                }
            }
        }
        public void PrintBrookersInfo()
        {
            int currentPage = 1;
            int pageCount = es.GetBrookerPagesCount();
            while (true)
            {
                try
                {
                    Console.Clear();
                    string result = es.GetAllBrookersInfo(currentPage);
                    Console.WriteLine(result);
                    Console.WriteLine("Commands: 0:Back, 1:Previous page, 2:Next page ");
                    Console.Write("Enter command: ");
                    string cmd = Console.ReadLine();
                    switch (cmd)
                    {
                        case "0":
                            return;
                        case "1":
                            if (currentPage > 1) { currentPage--; }
                            break;
                        case "2":
                            if (currentPage < pageCount) { currentPage++; }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

        }
        public void HireNewBrooker()
        {
            try
            {
                Console.Write($"> Enter first name: ");
                string name = Console.ReadLine();
                Console.Write($"> Enter last name: ");
                string lastName = Console.ReadLine();
                Console.Write($"> Enter address name: ");
                string address = Console.ReadLine();
                Console.Write($"> Enter town: ");
                string town = Console.ReadLine();
                Console.Write($"> Enter department: ");
                string department = Console.ReadLine();
                Console.Write($"> Enter phone number(it has to be 10 digits only): ");
                string number = Console.ReadLine();
                if (number.Count() != 10)
                {
                    Console.WriteLine("Invalid phone number!");
                    WaitPressKey();
                    return;
                }
                Console.Write($"> Enter email: ");
                string email = Console.ReadLine();

                string result = es.AddBrooker(name, lastName, address, town, department, number, email);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            WaitPressKey();
        }
        public void DeleteBrooker()
        {
            try
            {
                Console.Write($"> Enter brooker's id: ");
                int id = int.Parse(Console.ReadLine());
                string result = es.DeleteBrookerById(id);
                Console.WriteLine(result);
                WaitPressKey();
            }
            catch (Exception)
            {
                Console.WriteLine("This brooker cannot be discharge, his projects are still remainig...");
                WaitPressKey();
            }

        }
        public void UpdateBrookerPhoneNumber()
        {
            Console.Write($"> Enter brooker's id: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write($"> Enter new phone number : ");
            string pN = Console.ReadLine();
            if (pN.Count() != 10)
            {
                Console.WriteLine("Invalid phone number!");
                WaitPressKey();
                return;
            }
            string result = es.UpdateBrookerPhoneNum(id, pN);
            Console.WriteLine(result);
            WaitPressKey();
        }
        public void GetExactBrookerInfo()
        {
            Console.Write($"> Enter brooker's id: ");
            int id = int.Parse(Console.ReadLine());
            try
            {
                string result = es.GetBrookerInfo(id);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            WaitPressKey();
        }
        private static void WaitPressKey()
        {
            Console.WriteLine($"Press any key to continue...");
            Console.ReadKey();
        }
        public void Commands()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Brooker menu:");
            sb.AppendLine($"\t0: Back");
            sb.AppendLine($"\t1: Brookres list");
            sb.AppendLine($"\t2: Hire new brooker");
            sb.AppendLine($"\t3: Discharge brooker");
            sb.AppendLine($"\t4: Update brooker phone number");
            sb.AppendLine($"\t5: Get exact brooker info");
            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}
