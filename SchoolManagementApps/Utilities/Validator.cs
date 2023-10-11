using SchoolManagementApps.DTO;
using SchoolManagementApps.Entity;
using SchoolManagementApps.ModelMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolManagementApps.Utilities
{
    public class Validator
    {
        public static bool IsValidPhoneNUmber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }
            if (phone.StartsWith("+") && phone.Any(x => !char.IsDigit(x)) && phone.Length != 14)
            {
                return false;
            }
            if (phone.StartsWith("0") && phone.Any(x => !char.IsDigit(x)) && phone.Length != 11)
            {
                return false;
            }

            return true;
        }

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            if (name.Any(x => !char.IsLetter(x)) && name.Length > 50)
            {
                return false;
            }

            return true;
        }

        internal static bool IsValidAddress(Address addressDto)
        {
            if (addressDto == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(addressDto.PostalCode) ||
                string.IsNullOrWhiteSpace(addressDto.StreetName) ||
                string.IsNullOrWhiteSpace(addressDto.HouseNumber) ||
                string.IsNullOrWhiteSpace(addressDto.Town) ||
                string.IsNullOrWhiteSpace(addressDto.City) ||
                string.IsNullOrWhiteSpace(addressDto.State) ||
                string.IsNullOrWhiteSpace(addressDto.Country))
            {
                return false;
            }

            return true;
        }

        internal static bool IsValidUserRegistration(UserRegistrationDto userDto)
        {
            if (userDto == null)
            {
                return false;
            }

            if (!IsValidEmail(userDto.Email))
            {
                return false; 
            }

            return true;
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        internal static bool IsValidCourse(CourseDto courseDto)
        {
            if (courseDto == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(courseDto.CourseTitle) ||
                courseDto.CourseCode <= 0 ||
                courseDto.CourseUnits <= 0)
            {
                return false;
            }

            return true;
        }

    }
}

