//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PromoToEvents
{
    using System;
    using System.Collections.Generic;
    
    public partial class Categoria
    {
        public Categoria()
        {
            this.Evento = new HashSet<Evento>();
        }
    
        public int idCategoria { get; set; }
        public string nombreCategoria { get; set; }
        public string imgPathCategoria { get; set; }
        public bool statusCategoria { get; set; }
    
        public virtual ICollection<Evento> Evento { get; set; }
    }
}