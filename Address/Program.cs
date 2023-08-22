using System;

namespace Address
{
  
  
    using System.ComponentModel.DataAnnotations;
 

    public class Program
    {
        public static void Main()
        {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public int TownId { get; set; }

    }
}

