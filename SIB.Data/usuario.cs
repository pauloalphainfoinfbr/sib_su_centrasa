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
    
    public partial class usuario
    {
        public usuario()
        {
            this.checklist_fase = new HashSet<checklist_fase>();
            this.checklist_fase1 = new HashSet<checklist_fase>();
            this.permissao_usuario = new HashSet<permissao_usuario>();
            this.checklist = new HashSet<checklist>();
        }
    
        public int id_usuario { get; set; }
        public Nullable<int> id_empresa { get; set; }
        public Nullable<int> id_tipo_usuario { get; set; }
        public string nome_usuario { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public Nullable<int> usu_administrador { get; set; }
        public Nullable<int> id_transportadora { get; set; }
        public string application_sessionID { get; set; }
    
        public virtual ICollection<checklist_fase> checklist_fase { get; set; }
        public virtual ICollection<checklist_fase> checklist_fase1 { get; set; }
        public virtual empresa empresa { get; set; }
        public virtual empresa empresa1 { get; set; }
        public virtual ICollection<permissao_usuario> permissao_usuario { get; set; }
        public virtual tipo_usuario tipo_usuario { get; set; }
        public virtual ICollection<checklist> checklist { get; set; }
    }
}