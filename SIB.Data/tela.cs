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
    
    public partial class tela
    {
        public tela()
        {
            this.permissao_usuario = new HashSet<permissao_usuario>();
        }
    
        public int id_tela { get; set; }
        public string desc_tela { get; set; }
        public string hyperlink_nome { get; set; }
    
        public virtual ICollection<permissao_usuario> permissao_usuario { get; set; }
    }
}
