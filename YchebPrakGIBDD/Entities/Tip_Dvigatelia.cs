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
    
    public partial class Tip_Dvigatelia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tip_Dvigatelia()
        {
            this.Avtomobil = new HashSet<Avtomobil>();
        }
    
        public int ID { get; set; }
        public string Tip_EN { get; set; }
        public string Tip_RU { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Avtomobil> Avtomobil { get; set; }
    }
}