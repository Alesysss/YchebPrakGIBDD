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
    
    public partial class Statys_VY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Statys_VY()
        {
            this.VY_Istoria_Statysa = new HashSet<VY_Istoria_Statysa>();
        }
    
        public int ID { get; set; }
        public string Nazvanie { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VY_Istoria_Statysa> VY_Istoria_Statysa { get; set; }
    }
}
