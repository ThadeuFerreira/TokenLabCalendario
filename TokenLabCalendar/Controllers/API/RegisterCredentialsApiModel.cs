﻿namespace TokenLabCalendar.Controllers.API
{
    public class RegisterCredentialsApiModel
    {
        public string Username { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Email { get;  set; }
        public string Password { get;  set; }

        public RegisterCredentialsApiModel()
        {

        }
    }
}