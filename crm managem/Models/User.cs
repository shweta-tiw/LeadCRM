using System;
using System.ComponentModel.DataAnnotations;

namespace LeadManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required] public string FullName { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Role { get; set; } // Admin / Sales
        public DateTime CreatedAt { get; set; }
    }
}