﻿namespace AbsenceManagementSystem.Model.DTOs
{
    public class AuthenticatedUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
