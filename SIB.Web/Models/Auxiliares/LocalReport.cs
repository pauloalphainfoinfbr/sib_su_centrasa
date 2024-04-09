using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIB.Web.Models.Auxiliares
{
    class LocalReport
    {
        internal void Render(string p, string deviceInfo, Func<string, string, Encoding, string, bool, System.IO.Stream> CreateStream, out Warning[] warnings)
        {
            throw new NotImplementedException();
        }

        public string ReportPath { get; set; }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
