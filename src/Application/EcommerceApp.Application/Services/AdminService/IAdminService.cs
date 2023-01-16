using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services.AdminService
{
    public interface IAdminService
    {
        Task CreateManager(ApiAddManagerDTO apiAddManagerDTO);
        Task<List<ListOfManagerVM>> GetManagers();
        Task<UpdateManagerDTO> GetManager(Guid id);
        Task UpdateManager(UpdateManagerDTO updateManagerDTO);
        Task DeleteManager(Guid id);
        ApiAddManagerDTO GetApiAddManagerDTO(AddManagerDTO addManagerDTO);

    }
}
