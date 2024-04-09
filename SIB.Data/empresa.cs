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
    
    public partial class empresa
    {
        public empresa()
        {
            this.endereco1 = new HashSet<endereco>();
            this.usuario = new HashSet<usuario>();
            this.usuario1 = new HashSet<usuario>();
            this.checklist = new HashSet<checklist>();
        }
    
        public int id_empresa { get; set; }
        public Nullable<int> id_tipo_empresa { get; set; }
        public string razao_social { get; set; }
        public string nome_fantasia { get; set; }
        public string cpfcnpj { get; set; }
        public Nullable<byte> idc_horario_verao { get; set; }
        public string inscricao_estadual { get; set; }
        public string email { get; set; }
        public Nullable<byte> ordem { get; set; }
        public string endereco { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string telefone { get; set; }
        public Nullable<int> id_cidade { get; set; }
        public Nullable<int> id_estado { get; set; }
        public string codigo { get; set; }
    
        public virtual cidade cidade { get; set; }
        public virtual estado estado { get; set; }
        public virtual tipo_empresa tipo_empresa { get; set; }
        public virtual ICollection<endereco> endereco1 { get; set; }
        public virtual ICollection<usuario> usuario { get; set; }
        public virtual ICollection<usuario> usuario1 { get; set; }
        public virtual ICollection<checklist> checklist { get; set; }
    }
}
