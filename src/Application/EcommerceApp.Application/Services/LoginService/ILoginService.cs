using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services.LoginService
{
    public interface ILoginService
    {
        Task<Employee> Login(LoginDTO loginDTO);
    }
}
