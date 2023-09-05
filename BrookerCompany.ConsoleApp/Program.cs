using BrookerCompany.ConsoleApp.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrookerCompany.ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                Console.Clear();
                Commands();
                Console.Write("Enter command: ");
                string cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "0":
                        return;
                    case "1":
                        new BrookerController();
                        break;
                    case "2":
                        new ClientController();
                        break;
                    case "3":
                        new ProjectController();
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
            }
        }


        public static void Commands()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" The menu:");
            sb.AppendLine($"\t0: Back");
            sb.AppendLine($"\t1: Employees");
            sb.AppendLine($"\t2: Clients");
            sb.AppendLine($"\t3: Projects");
            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}

