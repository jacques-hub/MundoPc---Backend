namespace Proyect.Interface.DTOs
{
    using Project.Domain.Entities;

    public class UserDto: BaseDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
        public RoleType Role { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }
    }
}
