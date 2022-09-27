using API.Context;
using API.Middleware;
using API.Models;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class UserRepository 
    {
        MyContext myContext;
        public UserRepository(MyContext myContext) 
        {
            this.myContext = myContext;
        }

        public User Get(string username)
        {
            var checkUsername = myContext.User.SingleOrDefault(user => user.UserName.Equals(username));
            return checkUsername;
        }
        public User Get(int id)
        {
            var checkUsername = myContext.User.Find(id);
            return checkUsername;
        }

        public User login(string email, string password)
        {
            var checkEmployee = GetEmployee(email);
            if (checkEmployee != null)
            {
                var checkUser = Get(checkEmployee.Id);
                if (checkUser != null)
                {
                    if (Hashing.ValidatePassword(password, checkUser.Password))
                        return checkUser;
                }
            }
            return null;

        }
        public int Register(Register register)
        {
            // preparation default role (get the lowest Level Role)
            //var level = myContext.Levels.Min(x => x.Value);
            //var roleDefault = myContext.Roles.SingleOrDefault(x => x.Level.Value.Equals(level)).Id;

            // check duplicate email
            var checking = myContext.Employees.SingleOrDefault(x => x.Email.Equals(register.Email));
            if (checking != null)
                return -1;

            // mapping Model Employee from Register
            //Employee employee = new Employee(register.FirstName, register.Email, register.PhoneNumber, register.HireDate, register.Salary);
            Employees employee = new Employees()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                HireDate = register.HireDate,
                Salary = register.Salary,
                Job_Id = register.Job_Id,
                Department_Id = register.Department_Id
            };

            // insert data Employee to database
            myContext.Employees.Add(employee);
            var registeringEmployee = myContext.SaveChanges();

            // if inserted
            if (registeringEmployee > 0)
            {
                // -> preparation assigning role
                var registeredEmployee = myContext.Employees.SingleOrDefault(x => x.Email.Equals(register.Email)).Id;

                var passwordHashing = Hashing.HashPassword(register.Password);

                // -> mapping Model User from Register
                User user = new User()
                {
                    Employee_Id = registeredEmployee,
                    UserName = register.Username,
                    Password = passwordHashing
                };

                // -> insert data User to database
                myContext.User.Add(user);
                var registeringUser = myContext.SaveChanges();

                // -> if inserted
                if (registeringUser > 0)
                {
                    // --> mapping Model UserRole
                    UserRole userRole = new UserRole();
                    userRole.RoleId = 1;
                    userRole.UserId = registeredEmployee;

                    // --> insert data UserRole to database
                    myContext.UserRoles.Add(userRole);
                    var assigningRole = myContext.SaveChanges();
                    if (assigningRole > 0)
                    {
                        return assigningRole;
                    }
                }
                else
                {
                    // else clear data registered
                    var dataEmployee = myContext.Employees.Find(registeredEmployee);
                    myContext.Employees.Remove(dataEmployee);
                    var deletingEmployee = myContext.SaveChanges();
                }
            }
            return 0;
        }
        public int ChangePassword(string email, string oldPasswordInput, string newPassword)
        {
            var checkEmail = myContext.Employees.SingleOrDefault(_employee => _employee.Email.Equals(email));
            if (checkEmail != null)
            {
                var checkUser = myContext.User.Find(checkEmail.Id);
                if (checkUser != null)
                {
                    if (oldPasswordInput == checkUser.Password)
                    {
                        checkUser.Password = newPassword;
                        int result = myContext.SaveChanges();
                        return result;
                    }
                }
            }
            return 0;
        }
        public User GetUserByEmail(string email)
        {
            var getEmployee = myContext.Employees.SingleOrDefault(c => c.Email.Equals(email));
            if (getEmployee != null)
            {
                var getUser = myContext.User.Find(getEmployee.Id);
                return getUser;
            }
            return null;
        }
        public Employees GetEmployee(string email)
        {
            var checkEmployee = myContext.Employees.SingleOrDefault(user => user.Email.Equals(email));
            return checkEmployee;
        }
        public string GetRolesByUserId(int id)
        {
            var user = myContext.User.Find(id);
            UserRole[] userRole = myContext.UserRoles.Where(x => x.UserId.Equals(id)).ToArray();
            Role role;
            if (userRole.Length > 1)
            {
                role = myContext.Role.Find(1);
            }
            role = myContext.Role.Find(userRole[0].RoleId);
            return role.Name;
        }
        public int ForgetPassword(string email, string password)
        {
            //cek dari username
            var checkUser = GetUserByEmail(email);
            if (checkUser != null)
            {
                checkUser.Password = password;
                int result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }

        public UserRole GetRoleById(int UserId)
        {
            var user = myContext.UserRoles.FirstOrDefault(x => x.RoleId == UserId);

            return user;

        }
    }
}
