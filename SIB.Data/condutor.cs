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
    
    public partial class condutor
    {
        public condutor()
        {
            this.checklist = new HashSet<checklist>();
        }
    
        public string cpf_condutor { get; set; }
        public string nome_condutor { get; set; }
        public string cnh { get; set; }
        public Nullable<System.DateTime> data_nascimento { get; set; }
    
        public virtual ICollection<checklist> checklist { get; set; }
    }
}
