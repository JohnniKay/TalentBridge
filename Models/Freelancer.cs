using System.ComponentModel.DataAnnotations;

namespace ClientConnect.Models
{
    /// <summary>
    /// Represents a freelancer on the ClientConnect platform.
    /// Freelancers can apply to project listings and are identified by their name, email, and skill set.
    /// Additional details include experience, availability, phone number, and a personal bio.
    /// </summary>
    public class Freelancer
    {
        // Unique identifier for each freelancer (Primary Key)
        [Key]
        public int Id { get; set; }

        // Freelancer's full name (required, max 100 characters)
        [Required]
        [Display(Name = "Full Name")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // Freelancer's email address (required, must follow valid email format)
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Freelancer's phone number (optional, must be a valid phone number format)
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        // Summary of freelancer's skills (optional, can be a comma-separated string)
        [Display(Name = "Skill Set")]
        public string Skills { get; set; } = string.Empty;

        // Number of years of professional experience (0–50 range for validation)
        [Display(Name = "Years of Experience")]
        [Range(0, 50)]
        public int ExperienceYears { get; set; }

        // Indicates whether the freelancer is currently available for hire
        [Display(Name = "Available for Hire")]
        public bool IsAvailable { get; set; }
        public string Bio { get; set; } = string.Empty;
    }
}
