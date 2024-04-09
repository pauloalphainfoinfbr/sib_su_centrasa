using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIB.Web.Controllers
{
    class ManagementObjectSearcher
    {
        private System.Data.Objects.ObjectQuery objectQuery;

        public ManagementObjectSearcher(System.Data.Objects.ObjectQuery objectQuery)
        {
            // TODO: Complete member initialization
            this.objectQuery = objectQuery;
        }
        internal Models.Auxiliares.ManagementObjectCollection Get()
        {
            throw new NotImplementedException();
        }
    }
}
