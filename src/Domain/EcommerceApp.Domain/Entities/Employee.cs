using EcommerceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Domain.Entities
{
    public class Employee : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime BirthDate { get; set; }
        public Status Status { get; set; }
        public Roles Roles { get; set; }

        public string? ImagePath { get; set; }
        [NotMapped]
        public IFormFile UploadPath { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        //Navigation Property
        public Guid? MallId { get; set; }
        public Mall? Mall { get; set; }


        //Navigation Property For Managers
        public Guid? ManagerId { get; set; }
        public Employee? Manager { get; set; }


        public List<Employee> Employees  { get; set; }

        public Employee()
        {
            Employees = new List<Employee>();
        }

    }
}
