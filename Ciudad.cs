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
    
    public partial class Ciudad
    {
        public Ciudad()
        {
            this.Evento = new HashSet<Evento>();
            this.Afiliado = new HashSet<Afiliado>();
        }
    
        public int idCiudad { get; set; }
        public string nombreCiudad { get; set; }
        public int idPais { get; set; }
        public int idEstado { get; set; }
    
        public virtual Estado Estado { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual ICollection<Evento> Evento { get; set; }
        public virtual ICollection<Afiliado> Afiliado { get; set; }
    }
}