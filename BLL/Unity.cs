using IDAL;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public sealed class Unity
    {
        private static readonly IUnityContainer container = new UnityContainer().LoadConfiguration();
        private Unity() {}

        public static IUnityContainer Container
        {
            get
            {
                return container;
            }
        }

    }
}
