using EcommerceApp.Application.Extensions;
using EcommerceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Models.DTOs
{
    public class UpdateManagerDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Cannot be Empty")]
        [MaxLength(25, ErrorMessage = "You Cannot Enter More Than 25 Characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Cannot be Empty")]
        [MaxLength(50, ErrorMessage = "You Cannot Enter More Than 25 Characters")]
        public string Surname { get; set; }
        public DateTime? UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
        public Roles Roles => Roles.Manager;
        public string? ImagePath { get; set; }
        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
    }
}
