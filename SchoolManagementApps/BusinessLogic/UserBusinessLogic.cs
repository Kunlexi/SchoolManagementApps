using System;
using SchoolManagementApps.DTO;
using SchoolManagementApps.Entity;
using SchoolManagementApps.MockDatabase;
using SchoolManagementApps.Utilities;
using System.Collections.Generic;
using System.Linq;
using SchoolManagementApps.ModelMappers;

namespace SchoolManagementApps.BusinessLogic
{
    public class UserBusinessLogic
    {
        private static int nextUserId = 1;

        public bool CreateUser(UserRegistrationDto userDto, CourseDto courseDto, Address addressDto, Role role)
        {
            if (!Validator.IsValidUserRegistration(userDto) || !Validator.IsValidAddress(addressDto))
            {
                return false;
            }

            Course newCourse = null;

            if (role != Role.Admin)
            {
                if (courseDto == null)
                {
                    return false;
                }

                if (!Validator.IsValidCourse(courseDto))
                {
                    return false;
                }

                newCourse = CreateCourse(courseDto);
            }

            var newAddress = CreateAddress(addressDto);

            if (newAddress == null)
            {
                return false;
            }

            if (newCourse != null)
            {
                userDto.CourseId = newCourse.Id;
                userDto.Course = new List<Course> { newCourse };
            }

            var newUser = new User()
            {
                Id = nextUserId++,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                Role = role,
                Address = newAddress,
                Course = userDto.Course
            };

            SchoolManagementDataBase.UserDb.Add(newUser);
            return true;
        }

        public IEnumerable<User> GetRegisteredUsers()
        {
            return SchoolManagementDataBase.UserDb;
        }

        private Address CreateAddress(Address addressDto)
        {
            if (!Validator.IsValidAddress(addressDto))
            {
                return null;
            }

            var newAddress = new Address()
            {
                PostalCode = addressDto.PostalCode,
                StreetName = addressDto.StreetName,
                HouseNumber = addressDto.HouseNumber,
                Town = addressDto.Town,
                City = addressDto.City,
                State = addressDto.State,
                Country = addressDto.Country
            };

            SchoolManagementDataBase.AddressDb.Add(newAddress);

            return newAddress;
        }

        private Course CreateCourse(CourseDto courseDto)
        {
            if (!Validator.IsValidCourse(courseDto))
            {
                return null;
            }

            var newCourse = new Course()
            {
                CourseTitle = courseDto.CourseTitle,
                CourseCode = courseDto.CourseCode,
                CourseUnits = courseDto.CourseUnits
            };

            SchoolManagementDataBase.CourseDb.Add(newCourse);

            return newCourse;
        }
    }
}
