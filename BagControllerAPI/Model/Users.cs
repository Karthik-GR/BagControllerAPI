using System;
using System.ComponentModel.DataAnnotations;

namespace BagControllerAPI.Model
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
