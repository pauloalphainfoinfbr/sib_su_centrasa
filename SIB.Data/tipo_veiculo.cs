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
    
    public partial class tipo_veiculo
    {
        public tipo_veiculo()
        {
            this.checklist = new HashSet<checklist>();
        }
    
        public int id_tipo_veiculo { get; set; }
        public string desc_tipo_veiculo { get; set; }
    
        public virtual ICollection<checklist> checklist { get; set; }
    }
}
