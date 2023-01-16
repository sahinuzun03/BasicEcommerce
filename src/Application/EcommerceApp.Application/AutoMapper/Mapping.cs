using AutoMapper;
using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using EcommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            //EŞLEŞTİRME İŞLEMLERİ GERÇEKLEŞTİRİLECEK 

            //Hangi türden veri gelirse diğerine otomatik çevir
            //ÖRNEK : CreateMap<T,TResult>().ReverseMap(); 

            CreateMap<Employee, AddManagerDTO>().ReverseMap();
            CreateMap<Employee, ListOfManagerVM>().ReverseMap();
            CreateMap<UpdateManagerVM,UpdateManagerDTO>().ReverseMap();
            CreateMap<UpdateManagerDTO, Employee>().ReverseMap();
            CreateMap<AddManagerDTO,ApiAddManagerDTO>().ReverseMap();   
            CreateMap<ApiAddManagerDTO, Employee>().ReverseMap();


        }
    }
}
