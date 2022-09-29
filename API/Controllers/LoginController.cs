using API.Base;
using API.Models;
using API.Repositories.Data;
using API.Service;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UserRepository userRepository;
        private IConfiguration iconfiguration;
        public LoginController(UserRepository userRepository, IConfiguration iconfiguration)
        {
            this.userRepository = userRepository;
            this.iconfiguration = iconfiguration;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return BadRequest();
            }
            var check = userRepository.GetEmployee(login.Email);
            if (check == null)
            {
                return NotFound();
            }
            var result = userRepository.login(login.Email, login.Password);
            if (result != null)
            {
                var jwt = new JwtService(iconfiguration);

                string FullName = result.Employees.FirstName + " " + result.Employees.LastName;
                string role = userRepository.GetRolesByUserId(result.Employee_Id);
                var token = jwt.GenerateSecurityToken(result.Employee_Id, result.Employees.Email, FullName, role);
                return Ok(new { status = 200, message = "login successful!", token = token });
            }
            return BadRequest();

        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(Register register)
        {
            if (!string.IsNullOrWhiteSpace(register.Email) && !string.IsNullOrWhiteSpace(register.Password))
            {
                if (ModelState.IsValid)
                {
                    var result = userRepository.Register(register);
                    if (result > 0)
                        return Ok(new { result = 200, message = "Successfully Registered" });
                    else if (result == -1)
                        return BadRequest(new { result = 400, message = "Email is already registered" });
                }
            }
            return BadRequest();
        }

        [HttpPut("Change-Password/{id}")]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            if (string.IsNullOrWhiteSpace(changePassword.Email) ||
                string.IsNullOrWhiteSpace(changePassword.OldPassword) ||
                string.IsNullOrWhiteSpace(changePassword.NewPassword))
            {
                return BadRequest();
            }
            var check = userRepository.GetUserByEmail(changePassword.Email);
            if (check == null)
            {
                return NotFound();
            }
            int result = userRepository.ChangePassword(changePassword.Email, changePassword.OldPassword, changePassword.NewPassword);
            if (result > 0)
                return Ok(new { status = 200, message = "password-change successful!" });
            return BadRequest();
        }
        [HttpPut("Forgot-Password/{username}")]
        public IActionResult ForgotPassword(ForgetPassword forgetPassword)
        {
            if (string.IsNullOrWhiteSpace(forgetPassword.Email) && string.IsNullOrWhiteSpace(forgetPassword.NewPassword))
            {
                return BadRequest();
            }
            var check = userRepository.GetUserByEmail(forgetPassword.Email);
            if (check == null)
            {
                return NotFound();
            }
            int result = userRepository.ForgetPassword(forgetPassword.Email, forgetPassword.NewPassword);
            if (result > 0)
                return Ok(new { status = 200, message = "password-change successful!" });
            return BadRequest();
        }
    }
}
