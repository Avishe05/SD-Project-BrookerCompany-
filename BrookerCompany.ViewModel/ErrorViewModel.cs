using System;
using System.Collections.Generic;
using System.Text;

namespace BrookerCompany.ViewModel.WebApp.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
