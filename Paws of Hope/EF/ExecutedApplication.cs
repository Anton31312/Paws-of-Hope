//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Paws_of_Hope.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExecutedApplication
    {
        public int IDExecutedApplication { get; set; }
        public System.DateTime DateExecutedApplication { get; set; }
        public int IDApplication { get; set; }
        public int IDAnimalShelter { get; set; }
        public int IDPet { get; set; }
        public bool IsDone { get; set; }
    
        public virtual AnimalShelter AnimalShelter { get; set; }
        public virtual Application Application { get; set; }
        public virtual Pet Pet { get; set; }
    }
}
