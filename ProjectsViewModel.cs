using BrookerCompany.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrookerCompany.ViewModels.Projects
{

    public class ProjectsViewModel
    {
        public List<ProjectIndexViewModel> Projects { get; set; } = new List<ProjectIndexViewModel>();
    }
}
