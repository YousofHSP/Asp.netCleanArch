using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(250)]
        public string Username { get; set; }

        [Required, MaxLength(250), EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(250)]
        public string Password { get; set; }

    }
}
