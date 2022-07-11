﻿
namespace Med.Shared.Dtos.User
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string? ProjectManagerId { get; set; }
        public string? GroupManagerId { get; set; }
    }
}
