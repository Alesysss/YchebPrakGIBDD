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
    
    public partial class Mesto_raboti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mesto_raboti()
        {
            this.Voditel = new HashSet<Voditel>();
        }
    
        public int ID { get; set; }
        public string Kompania { get; set; }
        public string Dolznost { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Voditel> Voditel { get; set; }
    }
}