using EcommerceApp.Application.Extensions;
using EcommerceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Models.DTOs
{
    public class ApiAddManagerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime BirthDate { get; set; }
        public Status Status { get; set; }
        public Roles Roles { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string? ImagePath { get; set; }
    }
}
