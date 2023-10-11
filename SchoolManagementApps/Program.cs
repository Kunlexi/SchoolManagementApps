using System;
using System.Collections.Generic;
using SchoolManagementApps.BusinessLogic;
using SchoolManagementApps.DTO;
using SchoolManagementApps.Entity;
using SchoolManagementApps.ModelMappers;
using SchoolManagementApps.Utilities;

namespace SchoolManagementApps.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UserBusinessLogic userBusinessLogic = new UserBusinessLogic();

            Console.WriteLine("Welcome to the School Management Application!");

            while (true)
            {
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Register a New User");
                Console.WriteLine("2. View Registered Users and Courses");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterNewUser(userBusinessLogic);
                        break;
                    case "2":
                        ViewRegisteredUsersAndCourses(userBusinessLogic);
                        break;
                    case "3":
                        Console.WriteLine("Exiting the application. Goodbye!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void RegisterNewUser(UserBusinessLogic userBusinessLogic)
        {
            Console.WriteLine("\nRegister a New User:");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Role (1 for Teacher, 2 for Student, 3 for Admin): ");
            if (!Enum.TryParse(Console.ReadLine(), out Role role))
            {
                Console.WriteLine("Invalid role. Please enter a valid role.");
                return;
            }

            Console.Write("Address - Postal Code: ");
            string postalCode = Console.ReadLine();

            Console.Write("Address - Street Name: ");
            string streetName = Console.ReadLine();

            Console.Write("Address - House Number: ");
            string houseNumber = Console.ReadLine();

            Console.Write("Address - Town: ");
            string town = Console.ReadLine();

            Console.Write("Address - City: ");
            string city = Console.ReadLine();

            Console.Write("Address - State: ");
            string state = Console.ReadLine();

            Console.Write("Address - Country: ");
            string country = Console.ReadLine();

            var userDto = new UserRegistrationDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Role = role
            };

            var addressDto = new AddressDto
            {
                PostalCode = postalCode,
                StreetName = streetName,
                HouseNumber = houseNumber,
                Town = town,
                City = city,
                State = state,
                Country = country
            };

            CourseDto courseDto = null;

            if (role != Role.Admin)
            {
                Console.Write("Create a New Course (Y/N): ");
                string createCourseOption = Console.ReadLine();

                if (createCourseOption.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("Course Title: ");
                    string courseTitle = Console.ReadLine();

                    Console.Write("Course Code: ");
                    if (!int.TryParse(Console.ReadLine(), out int courseCode))
                    {
                        Console.WriteLine("Invalid course code. Please enter a valid integer.");
                        return;
                    }

                    Console.Write("Course Units: ");
                    if (!int.TryParse(Console.ReadLine(), out int courseUnits))
                    {
                        Console.WriteLine("Invalid course units. Please enter a valid integer.");
                        return;
                    }

                    courseDto = new CourseDto
                    {
                        CourseTitle = courseTitle,
                        CourseCode = courseCode,
                        CourseUnits = courseUnits
                    };
                }
            }

            var address = MapAddressDtoToEntity(addressDto);

            if (userBusinessLogic.CreateUser(userDto, courseDto, address, role))
            {
                Console.WriteLine("User registration successful!");
            }
            else
            {
                Console.WriteLine("User registration failed. Please check the input data.");
            }
        }

        private static Address MapAddressDtoToEntity(AddressDto addressDto)
        {
            return new Address
            {
                PostalCode = addressDto.PostalCode,
                StreetName = addressDto.StreetName,
                HouseNumber = addressDto.HouseNumber,
                Town = addressDto.Town,
                City = addressDto.City,
                State = addressDto.State,
                Country = addressDto.Country
            };
        }

        static void ViewRegisteredUsersAndCourses(UserBusinessLogic userBusinessLogic)
        {
            Console.WriteLine("\nRegistered Users and Courses:");

            foreach (var user in userBusinessLogic.GetRegisteredUsers())
            {
                Console.WriteLine($"User ID: {user.Id}");
                Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Phone Number: {user.PhoneNumber}");
                Console.WriteLine($"Role: {user.Role}");

                if (user.Course != null && user.Course.Count > 0)
                {
                    Console.WriteLine("Courses:");
                    foreach (var course in user.Course)
                    {
                        Console.WriteLine($"Course Title: {course.CourseTitle}");
                        Console.WriteLine($"Course Code: {course.CourseCode}");
                        Console.WriteLine($"Course Units: {course.CourseUnits}");
                        Console.WriteLine("------------");
                    }
                }

                Console.WriteLine("-------------------------------------------------------");
            }
        }
    }
}
