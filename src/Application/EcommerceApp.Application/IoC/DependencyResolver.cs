using Autofac;
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

            //IoC --> Yani interface çağırdığım zaman bana onun concrete yapısını getirmesi gerektiği işlemi burada söylüyordum!!

            //ÖRNERK : builder.RegisterType<BaseRepo>().As<IBaseRepo>().InsatancePerLifeTimeScope();


            //program.cs tarafında yapacağım eklemeleri buradan yapabilirim.

            //Örnek olarak automapper eklenmesi buradan yapabilirim.
            base.Load(builder);
        }
    }
}
