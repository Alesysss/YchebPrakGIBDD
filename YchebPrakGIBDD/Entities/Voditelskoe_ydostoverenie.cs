//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YchebPrakGIBDD.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Voditelskoe_ydostoverenie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voditelskoe_ydostoverenie()
        {
            this.Shtrafi = new HashSet<Shtrafi>();
            this.VY_Istoria_Statysa = new HashSet<VY_Istoria_Statysa>();
            this.Kategoria = new HashSet<Kategoria>();
        }
    
        public int ID { get; set; }
        public string Data_polych { get; set; }
        public string Data_okonch { get; set; }
        public int Voditel_ID { get; set; }
        public int Seria { get; set; }
        public int Nomer { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shtrafi> Shtrafi { get; set; }
        public virtual Voditel Voditel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VY_Istoria_Statysa> VY_Istoria_Statysa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kategoria> Kategoria { get; set; }
    }
}
