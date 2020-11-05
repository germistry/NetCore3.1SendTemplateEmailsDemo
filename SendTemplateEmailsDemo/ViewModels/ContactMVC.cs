using System.ComponentModel.DataAnnotations;

namespace SendTemplateEmailsDemo.ViewModels
{
    public class ContactMVC
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
