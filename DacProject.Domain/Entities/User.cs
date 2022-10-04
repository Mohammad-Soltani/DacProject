using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DacProject.Domain.Entities
{
    public class User : UserLogin
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string FirstName { get; set; } = String.Empty;

        [StringLength(100)]
        [Required]
        public string LastName { get; set; } = String.Empty;

        [StringLength(30)]
        [Required]
        public string Role { get; set; } = String.Empty;
    }
}
