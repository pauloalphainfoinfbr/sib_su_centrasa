//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIB.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tipo_empresa
    {
        public tipo_empresa()
        {
            this.empresa = new HashSet<empresa>();
        }
    
        public int id_tipo_empresa { get; set; }
        public string desc_tipo_empresa { get; set; }
    
        public virtual ICollection<empresa> empresa { get; set; }
    }
}
