using AutoMapper;
using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Enums;
using EcommerceApp.Domain.Repositories;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepo _employeeRepo;
        public AdminService(IMapper mapper,IEmployeeRepo employeeRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
        }

        public async Task CreateManager(ApiAddManagerDTO apiAddManagerDTO)
        {
            var addEmployee = _mapper.Map<Employee>(apiAddManagerDTO);
            await _employeeRepo.Create(addEmployee);
        }

        public async Task<List<ListOfManagerVM>> GetManagers()
        {
            var managers = await _employeeRepo.GetFilteredList(
                select: x => new ListOfManagerVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Roles = x.Roles,
                    ImagePath = x.ImagePath,
                },
                where: x => ((x.Status == Status.Active || x.Status == Status.Modified) && x.Roles == Roles.Manager),
                orderBy: x => x.OrderBy(x => x.Name));

            return managers;
        }

        public async Task<UpdateManagerDTO> GetManager(Guid id)
        {
            var manager = await _employeeRepo.GetFilteredFirstOrDefault(
                select: x => new UpdateManagerVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    ImagePath = x.ImagePath,
                },
                where : x => x.Id == id);

            var updateManagerDTO = _mapper.Map<UpdateManagerDTO>(manager);

            return updateManagerDTO;
        }

        public async Task UpdateManager(UpdateManagerDTO updateManagerDTO)
            {
            var model = await _employeeRepo.GetDefault(x => x.Id == updateManagerDTO.Id);

            model.Name = updateManagerDTO.Name;
            model.Surname = updateManagerDTO.Surname;
            model.UpdateDate = updateManagerDTO.UpdateDate;
            model.Status = updateManagerDTO.Status;
        
            using var image = Image.Load(updateManagerDTO.UploadPath.OpenReadStream());
            //Dosyayı yolunu okuduk

            image.Mutate(x => x.Resize(600, 560));//Resim boyutu ayarladık

            Guid guid = Guid.NewGuid();
            image.Save($"wwwroot/images/{guid}.jpg");

            model.ImagePath = ($"/images/{guid}.jpg");

            await _employeeRepo.Update(model);

        }

        public async Task DeleteManager(Guid id)
        {
            var model = await _employeeRepo.GetDefault(x => x.Id == id);

            model.DeleteDate = DateTime.Now;
            model.Status = Status.Passive;

            await _employeeRepo.Delete(model);
        }

        public ApiAddManagerDTO GetApiAddManagerDTO(AddManagerDTO addManagerDTO)
        {
            var apiAddManagerDTO = _mapper.Map<ApiAddManagerDTO>(addManagerDTO);
            if (addManagerDTO.UploadPath != null)
            {
                var stream = addManagerDTO.UploadPath.OpenReadStream();
                using var image = Image.Load(stream);
                //Dosyayı yolunu okuduk

                image.Mutate(x => x.Resize(600, 560));//Resim boyutu ayarladık

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");

                apiAddManagerDTO.ImagePath = ($"/images/{guid}.jpg");

            }
            else
            {
                apiAddManagerDTO.ImagePath = ($"/images/default.jpeg");
            }

            return apiAddManagerDTO;
        }
    }
}
