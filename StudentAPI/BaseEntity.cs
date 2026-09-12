using System.ComponentModel.DataAnnotations;

namespace StudentAPI
{
    public class BaseEntity
    {

    public class Student
    {
        [Key]
        public int Id { get; set; }            

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Range(1, 100)]
        public int Age { get; set; }

        [Required]
        [MaxLength(100)]
        public string Course { get; set; } = string.Empty;
    }
}
}
