﻿using SchoolManagementApps.Utilities;

namespace SchoolManagementApps.Entity
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int AddressId { get; set; }

        public Role Role { get; set; }

        public Address Address { get; set; }

        public int CourseId { get; set; }
        public List<Course> Course { get; set; }

    }
}
