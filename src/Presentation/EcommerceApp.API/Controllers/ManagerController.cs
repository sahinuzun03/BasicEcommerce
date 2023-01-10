using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using EcommerceApp.Application.Services.AdminService;
using EcommerceApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public ManagerController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("GetManagers")]
        public async Task<ActionResult<List<ListOfManagerVM>>> GetAllManagers()
        {
            var managers = await _adminService.GetManagers();
            if (managers == null)
            {
                return NotFound();
            }
            return Ok(managers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UpdateManagerDTO>> GetManager([FromRoute] Guid id)
        {
            var manager = await _adminService.GetManager(id);
            if (manager == null)
            {
                return NotFound();
            }
            return Ok(manager);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UpdateManagerDTO>> DeleteManager([FromRoute] Guid id)
        {
            await _adminService.DeleteManager(id);
            return Ok();
        }


        //CREATE VE UPDATE İÇİN BAKIP GELELİM !!!!! 


        [HttpPost]
        public async Task<ActionResult> CreateManager([FromForm] AddManagerDTO addManagerDTO)
        {
            if (ModelState.IsValid)
            {
                await _adminService.CreateManager(addManagerDTO);
                return CreatedAtAction("GetManager", new { Id = addManagerDTO.Id }, addManagerDTO);
            }

            return BadRequest();

        }
    }
}

