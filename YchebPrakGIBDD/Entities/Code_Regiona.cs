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
    
    public partial class Code_Regiona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Code_Regiona()
        {
            this.Avtomobil = new HashSet<Avtomobil>();
        }
    
        public int ID { get; set; }
        public string Nazvanie_EN { get; set; }
        public string Nazvanie_RU { get; set; }
        public Nullable<int> Code_sybiecta { get; set; }
        public string Cod { get; set; }
        public string OKATO_cod { get; set; }
        public string ISO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Avtomobil> Avtomobil { get; set; }
    }
}