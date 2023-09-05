using BrookerCompany.Data;
using BrookerCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrookerCompany.Services
{
    public class ProjectService
    {
        private AppDbContext context;
        public string AddProject(string name, string builidingType, int capacity, DateTime releaseDate, int totalFloorArea, int numberOfFloors, string address, string town)
        {
            StringBuilder message = new StringBuilder();
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(name))
            {
                message.AppendLine($"Invalid {(nameof(name))}");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(builidingType))
            {
                message.AppendLine($"Invalid {(nameof(builidingType))}");
                isValid = false;
            }
            if (capacity < 0)
            {
                message.AppendLine("Capacity cannot be less than zero");
                isValid = false;
            }
            if (totalFloorArea < 0)
            {
                message.AppendLine("Floor Area cannot be less than zero");
                isValid = false;
            }
            if (numberOfFloors < 0)
            {
                message.AppendLine("Floor number cannot be less than zero");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                message.AppendLine($"Invalid {(nameof(address))}");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(builidingType))
            {
                message.AppendLine($"Invalid {(nameof(builidingType))}");
                isValid = false;
            }
            Project p = GetProjectByName(name);
            Address a = null;
            Town t = null;
           
            using (context = new AppDbContext())
            {
                if (p != null)
                {
                    message.AppendLine($"Project {name} already exists!");
                }
                t = context.Towns.FirstOrDefault(x => x.Name == town);
                a = context.Addresses.FirstOrDefault(x => x.Name == address&& x.Town.Name==town);
               
                if (t == null)
                {
                    t = new Town() { Name = town };
                }
                if (a == null)
                {
                    a = new Address() { Name = address, Town = t };
                }
               if (isValid)
                {
                    p = new Project()
                    {
                        Name = name,
                        ReleaseDate = releaseDate,
                        Address = a
                    };
                    this.context.Projects.Add(p);
                    context.SaveChanges();
                    message.AppendLine($"Project {name} is added");

                }
            }
            return message.ToString().TrimEnd();
        }
        public Project GetProjectByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid project name.");
            }
            using (context = new AppDbContext())
            {
                Project p = this.context.Projects.FirstOrDefault(x => x.Name == name);
                return p;
            }
        }
        public Project GetProjectById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Invalid project id!");
            }
            Project project = this.context.Projects.Find(id);
            return project;
        }
        public int GetProjectIdByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid project name...");
            }
            using (context = new AppDbContext())
            {
                Project p = this.context.Projects.FirstOrDefault(x => x.Name == name);
                return p.Id;
            }
        }
        public string DeleteProjectById(int id)
        {
            Project project = GetProjectById(id);
            if (project == null)
            {
                return $"Project not found!";
            }
            context.Projects.Remove(project);
            context.SaveChanges();
            return "Project is suspended!";
        }
        public string GetProjectInfo(int id)
        {
            Project project = null;
            using (context = new AppDbContext())
            {
                project = context.Projects.Find(id);
            }
            if (project != null)
            {
                StringBuilder message = new StringBuilder();
                message.AppendLine($"{nameof(project)} info: ");
                message.AppendLine($"\tId: {project.Id}");
                message.AppendLine($"\tName: {project.Name}");
               message.AppendLine($"\tRelease date: {project.ReleaseDate.ToString("dd-MM-yyyy")}");
                
                message.AppendLine($"\tAddres id: {project.AddressId}");
                return message.ToString().TrimEnd();
            }
            else
            {
                return $"{nameof(Project)} not found!";
            }
        }
        public string GetAllProjectsInfo(int page = 1, int count = 10)
        {
            StringBuilder msg = new StringBuilder();
            string firstRow = $"| {"Id",-4} | {"Name",-25} | {"Type Id: ",-9} | {"Capacity",-12} | {"Release date",-12} | {"Floor area",-9} | {"Floors",-9} | {"Address id",-12} |";

            string line = $"|{new string('-', firstRow.Length - 2)}|";

            using (context = new AppDbContext())
            {
                List<Project> projects = context.Projects.Skip((page - 1) * count).Take(count).ToList();
                msg.AppendLine(firstRow);
                msg.AppendLine(line);
                foreach (var c in projects)
                {
                    string info = $"| {c.Id,-4} | {c.Name,-25} | {c.ReleaseDate.ToString("yyyy-MM-dd"),-12} | {c.AddressId,-12} |";
                    msg.AppendLine(info);
                    msg.AppendLine(line);
                }
                int pageCount = (int)Math.Ceiling(context.Projects.Count() / (decimal)count);
                msg.AppendLine($"Page: {page} / {pageCount}");
            }

            return msg.ToString().TrimEnd();
        }
        public int GetProjectsPagesCount(int count = 10)
        {
            using (context = new AppDbContext())
            {
                return (int)Math.Ceiling(context.Projects.Where(x => !x.ProjectClients.Any()).Count() / (double)count);
            }
        }
        public string AddImageToProject(string projectName, string url)
        {
            using (context = new AppDbContext())
            {
                Project p = context.Projects.FirstOrDefault(x => x.Name==projectName);
                if (p == null)
                {
                    return "Project not found";
                }
                Image i = new Image() { Url = url, ProjectId=p.Id };
                context.Images.Add(i);
                context.SaveChanges();
                return "Image added successfully!";
            }
        }
        public string DeleteImageToProject(int projectId, string url)
        {
            using (context = new AppDbContext())
            {
                Project p = context.Projects.Find(projectId);
                Image i = context.Images.FirstOrDefault(x => x.Url == url);
                if (p == null)
                {
                    return "Project not found";
                }
                if (i==null)
                {
                    return "Image not found!";
                }
                context.Images.Remove(i);
                context.SaveChanges();
                return "Image added successfully!";
            }
        }
        
    }
}
