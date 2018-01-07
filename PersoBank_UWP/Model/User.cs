using System;

namespace PersoBank_UWP.Model
{
    public class User
    {
        public string UserId { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime InscriptionDate { get; set; }
        public bool Sex { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }

    public class UserLogIn
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserEmailModel
    {
        public string EmailAddress { get; set; }
    }

    public class UserNameModel
    {
        public string UserName { get; set; }
    }
}
