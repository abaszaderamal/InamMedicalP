
namespace Med.Shared.Dtos.User
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool StayLogged { get; set; }
    }
}
