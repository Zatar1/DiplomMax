//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DaliAftotrans
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sredstva
    {
        public int IdZapisi { get; set; }
        public int IdVoditeli { get; set; }
        public int IdUser { get; set; }
        public decimal Money { get; set; }
        public System.DateTime Data { get; set; }
    
        public virtual User User { get; set; }
        public virtual Voditeli Voditeli { get; set; }
    }
}