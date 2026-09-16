namespace StudentAPI
{
    public class Student : BaseEntity
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Othername { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } 
        public int Age  => DateTime.Now.Year - DateOfBirth.Year;
        public string PhoneNumber { get; set; } = string.Empty;
    }

}
