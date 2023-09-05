

namespace BrookerCompany.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
 
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
        
        public decimal Budget { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<ProjectBrooker> ProjectBrookers { get; set; } = new List<ProjectBrooker>();
        public virtual ICollection<ProjectClient> ProjectClients { get; set; } = new List<ProjectClient>();
    }

}
