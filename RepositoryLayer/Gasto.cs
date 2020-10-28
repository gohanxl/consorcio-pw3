//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repositories
{
    using System;
    using System.Collections.Generic;
    
    public partial class Gasto
    {
        public int IdGasto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaGasto { get; set; }
        public int AnioExpensa { get; set; }
        public int MesExpensa { get; set; }
        public string ArchivoComprobante { get; set; }
        public decimal Monto { get; set; }
        public System.DateTime FechaCreacion { get; set; }
    
        public virtual Consorcio Consorcio { get; set; }
        public virtual TipoGasto TipoGasto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
