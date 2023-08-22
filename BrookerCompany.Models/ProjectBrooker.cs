
namespace BrookerCompany.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class ProjectBrooker
    {
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int BrookerId { get; set; }
        public virtual Brooker Brooker { get; set; }
    }
}
