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
    
    public partial class cidade
    {
        public cidade()
        {
            this.empresa = new HashSet<empresa>();
            this.endereco = new HashSet<endereco>();
        }
    
        public int id_cidade { get; set; }
        public string desc_cidade { get; set; }
        public Nullable<int> id_estado { get; set; }
        public Nullable<int> ibge_estado { get; set; }
    
        public virtual estado estado { get; set; }
        public virtual ICollection<empresa> empresa { get; set; }
        public virtual ICollection<endereco> endereco { get; set; }
    }
}