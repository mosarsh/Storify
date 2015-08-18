using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storify.Data;

namespace Storify.Business
{
    public class NinjectRegistration
    {
        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IStorifyRepository>().To<StorifyRepository>();
            kernel.Bind<StorifyEntities>().To<StorifyEntities>();
        }

    }
}
