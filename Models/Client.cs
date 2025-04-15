using System.ComponentModel.DataAnnotations;

namespace ClientConnect.Models
{
    /// <summary>
    /// Represents a client in the ClientConnect platform.
    /// Clients can post project listings and are identified by name, email, and optionally their company and contact number.
    /// </summary>
    public class Client
    {
        // Unique identifier for each client (Primary Key)
        [Key]
        public int Id { get; set; }

        // Full name of the client (required, max 100 characters)
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // Email address of the client (required, must be valid format)
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = string.Empty;

        // Client's phone number (must be a valid phone number format)
        [Phone(ErrorMessage = "Invalid phone number.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        // Company the client is associated with (optional, max 200 characters)
        [StringLength(200)]
        public string Company { get; set; } = string.Empty;
    }
}

