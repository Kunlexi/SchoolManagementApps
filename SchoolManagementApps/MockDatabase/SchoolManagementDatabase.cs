using SchoolManagementApps.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementApps.MockDatabase
{
    public class SchoolManagementDataBase
    {
        public static List<User> UserDb = new List<User>();
        public static List<Address> AddressDb = new List<Address>();
        public static List<Course> CourseDb = new List<Course>();
    }
}
