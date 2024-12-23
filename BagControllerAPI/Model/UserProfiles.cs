using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BagControllerAPI.Model
{
    public class UserProfiles
    {
        [Key]
        public int UserId { get; set; } // Primary Key

        // Navigation property for the related 'Users' entity
        [ForeignKey("UserId")]
        public Users User { get; set; } // Reference to the 'Users' table

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; }

        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(20)]
        public string ZipCode { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
