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
    
    public partial class VY_Istoria_Statysa
    {
        public int ID { get; set; }
        public int Statys_VY_ID { get; set; }
        public int VY_ID { get; set; }
        public string Data_smeni_statysa { get; set; }
        public string Kommentariy { get; set; }
    
        public virtual Statys_VY Statys_VY { get; set; }
        public virtual Voditelskoe_ydostoverenie Voditelskoe_ydostoverenie { get; set; }
    }
}