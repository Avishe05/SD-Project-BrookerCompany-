using BrookerCompany.Data;
using BrookerCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrookerCompany.Services
{
    public class BrookerService
    {
        private AppDbContext context;
        public string AddProjectTeam(int projectId, List<int> teamId)
        {
            StringBuilder message = new StringBuilder();
            using (context = new AppDbContext())
            {
                Project project = context.Projects.Find(projectId);
                if (project == null)
                {
                    message.AppendLine($"{nameof(Project)} not found!");
                    return message.ToString().TrimEnd();
                }
                List<Brooker> brookers = new List<Brooker>();
                foreach (var id in teamId)
                {
                    Brooker brooker = context.Brookers.Find(id);
                    if (brooker != null)
                    {
                        brookers.Add(brooker);
                    }
                }
                if (brookers.Count == 0)
                {
                    message.AppendLine($"{nameof(Brooker)}s not found!");
                    return message.ToString().TrimEnd();
                }
                message.AppendLine($"{projectId} {project.Name} team: ");
                foreach (var brooker in brookers)
                {
                    context.ProjectBrookers.Add(new ProjectBrooker
                    {
                        Project = project,
                        Brooker = brooker,
                    });
                    message.AppendLine($"\t{brooker.FirstName} {brooker.LastName}");
                }
                context.SaveChanges();
                return message.ToString().TrimEnd();
            }
        }
        public string AddBrooker(string firstName, string lastName, string address, string town, string department, string phoneNumber, string email)
        {
            StringBuilder message = new StringBuilder();
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(firstName))
            {
                message.AppendLine($"Invalid {(nameof(firstName))}");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                message.AppendLine($"Invalid {(nameof(lastName))}");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                message.AppendLine($"Invalid {(nameof(address))}");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(department))
            {
                message.AppendLine($"Invalid {(nameof(department))}");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                message.AppendLine($"Invalid {(nameof(phoneNumber))}");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                message.AppendLine($"Invalid {(nameof(email))}");
                isValid = false;
            }

            Address a = null;
            Department d = null;
            Town t = null;
            using (context = new AppDbContext())
            {
                t = context.Towns.FirstOrDefault(t => t.Name == town);
                a = context.Addresses.FirstOrDefault(a => a.Name == address && a.Town.Name == town);
                d = context.Departments.FirstOrDefault(d => d.Name == department);
                t = context.Towns.FirstOrDefault(t => t.Name == town);
                if (t == null)
                {
                    t = new Town() { Name = town };
                }
                if (d == null)
                {
                    d = new Department() { Name = department };
                }
                if (a == null)
                {
                    a = new Address() { Name = address, Town = t };
                }

                if (isValid)
                {
                    Brooker brooker = new Brooker()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Address = a,
                        Department = d,
                        PhoneNumber = phoneNumber,
                        Email = email
                    };
                    context.Brookers.Add(brooker);
                    context.SaveChanges();
                    message.AppendLine($"Brooker {firstName} {lastName} is added!");

                }
                return message.ToString().TrimEnd();
            }
          

        }
          public List<string> GetBrookerBasicInfo(int page = 1, int count = 10)
            {
                List<string> list = null;
                using (context = new AppDbContext())
                {
                    list = context.Brookers
                        .Skip((page - 1) * count)
                        .Take(count).Select(x => $"{x.Id} - {x.FirstName} {x.LastName}")
                        .ToList();
                }
                return list;
            }
        public string DeleteBrookerById(int id)
        {
            using (context = new AppDbContext())
            {
                Brooker brooker = context.Brookers.Find(id);
                if (brooker == null)
                {
                    return $"{nameof(Brooker)} not found!";
                }
                context.Brookers.Remove(brooker);
                context.SaveChanges();
                return $"{nameof(Brooker)} {brooker.FirstName} {brooker.LastName} was fired!";
            }
        }
        public string GetBrookerInfo(int id)
        {
            Brooker brooker = null;
            using (context = new AppDbContext())
            {
                brooker = context.Brookers.Find(id);
            }
            if (brooker != null)
            {
                StringBuilder masege = new StringBuilder();
                masege.AppendLine($"Employee info: ");
                masege.AppendLine($"\tId: {brooker.Id}");
                masege.AppendLine($"\tFirst name: {brooker.FirstName}");
                masege.AppendLine($"\tLast name: {brooker.LastName}");
                masege.AppendLine($"\tAddres id: {brooker.AddressId}");
                masege.AppendLine($"\tDepartment id: {brooker.DepartmentId}");
                masege.AppendLine($"\tPhone number: {brooker.PhoneNumber}");
                masege.AppendLine($"\tEmail: {brooker.Email}");
                return masege.ToString().TrimEnd();
            }
            else
            {
                return $"{nameof(Brooker)} not found!";
            }
        }
        public Brooker GetBrookerById(int id)
        {
            using (context = new AppDbContext())
            {
                Brooker b = context.Brookers.FirstOrDefault(x => x.Id == id);
                return b;
            }
        }
        public string GetAllBrookersInfo(int page = 1, int count = 10)
        {
            StringBuilder msg = new StringBuilder();
            string firstRow = $"| {"Id",-4} | {"First name",-12} | {"Last name",-12} | {"Adress id",-10} | {"Dpartmenmt id",-15} | {"Phone number",-12} | {"Email",-30} |";

            string line = $"|{new string('-', firstRow.Length - 2)}|";

            using (context = new AppDbContext())
            {
                List<Brooker> employees = context.Brookers.Skip((page - 1) * count).Take(count).ToList();
                msg.AppendLine(firstRow);
                msg.AppendLine(line);
                foreach (var e in employees)
                {
                    string info = $"| {e.Id,-4} | {e.FirstName,-12} | {e.LastName,-12} | {e.AddressId,-10} | {e.DepartmentId,-15} | {e.PhoneNumber,-12} | {e.Email,-30} |";
                    msg.AppendLine(info);
                    msg.AppendLine(line);
                }
                int pageCount = (int)Math.Ceiling(context.Brookers.Count() / (decimal)count);
                msg.AppendLine($"Page: {page} / {pageCount}");
            }

            return msg.ToString().TrimEnd();
     
        }
       
        public List<string> GetBrookerNames()
        {
            using (context = new AppDbContext())
            {
                List<string> brookerNames = context.Brookers.Select(x => x.Address.Name).ToList();
                return brookerNames;
            }
        }
        public string UpdateBrookerPhoneNum(int id, string num)
        {
            using (context = new AppDbContext())
            {
                Brooker e = context.Brookers.Find(id);
                if (e == null)
                {
                    return $"Brooker not found!";
                }
                e.PhoneNumber = num;
                context.Brookers.Update(e);
                context.SaveChanges();
                return $"Brooker {e.FirstName} {e.LastName} has new phone number: {num}";
            }
        }
        public int GetBrookerPagesCount(int count = 10)
        {
            using (context = new AppDbContext())
            {
                return (int)Math.Ceiling(context.Brookers.Count() / (double)count);
            }
        }
    }
}
