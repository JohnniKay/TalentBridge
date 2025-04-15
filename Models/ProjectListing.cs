using System.ComponentModel.DataAnnotations;

namespace ClientConnect.Models
{
    /// <summary>
    /// Represents a freelance project posted by a client on the ClientConnect platform.
    /// Includes key details such as title, description, budget, client information,
    /// and the date the project was posted. Each listing is linked to a client.
    /// </summary>
    public class ProjectListing
    {
        // Primary key identifier for the project listing.
        [Key]
        public int Id { get; set; }

        // Title of the project, required and limited to 100 characters.
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        // Detailed description of the project, shown in a multiline format.
        [Required(ErrorMessage = "Description is required.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;

        // Budget amount for the project; must be a non-negative value.
        [Required(ErrorMessage = "Budget is required.")]
        [Range(0, 1000000, ErrorMessage = "Budget must be a positive number.")]
        public decimal Budget { get; set; }

        // Name of the client who posted the project.
        [Required(ErrorMessage = "Client Name is required.")]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; } = string.Empty;

        // Date the project was posted; auto-set to current date/time.
        [DataType(DataType.Date)]
        [Display(Name = "Posted Date")]
        public DateTime PostedDate { get; set; } = DateTime.Now;
        public int ClientId { get; set; }
    }
}
