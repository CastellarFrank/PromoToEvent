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
    
    public partial class Pais
    {
        public Pais()
        {
            this.Afiliado = new HashSet<Afiliado>();
            this.Ciudad = new HashSet<Ciudad>();
            this.Estado = new HashSet<Estado>();
            this.Evento = new HashSet<Evento>();
        }
    
        public int idPais { get; set; }
        public string nombrePais { get; set; }
    
        public virtual ICollection<Afiliado> Afiliado { get; set; }
        public virtual ICollection<Ciudad> Ciudad { get; set; }
        public virtual ICollection<Estado> Estado { get; set; }
        public virtual ICollection<Evento> Evento { get; set; }
    }
}
