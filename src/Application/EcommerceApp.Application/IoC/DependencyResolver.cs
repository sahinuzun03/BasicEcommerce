using Autofac;
using AutoMapper;
using EcommerceApp.Application.AutoMapper;
using EcommerceApp.Application.Services.AdminService;
using EcommerceApp.Application.Services.LoginService;
using EcommerceApp.Domain.Repositories;
using EcommerceApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.IoC
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Repository ???

            builder.RegisterType<EmployeeRepo>().As<IEmployeeRepo>().InstancePerLifetimeScope();


            //Service ??
            builder.RegisterType<AdminService>().As<IAdminService>().InstancePerLifetimeScope();

            builder.RegisterType<LoginService>().As<ILoginService>().InstancePerLifetimeScope();


            //AUTOMAPPER
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();


            base.Load(builder);
        }
    }
}
