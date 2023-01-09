using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly IEmployeeRepo _employeeRepo;
        public LoginService(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<Employee> Login(LoginDTO loginDTO)
        {
            var employee = await _employeeRepo.GetDefault(x => x.EmailAddress == loginDTO.EmailAddress && x.Password == loginDTO.Password);

            return employee;
        }
    }
}
