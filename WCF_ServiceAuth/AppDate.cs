using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF_ServiceAuth
{
    public class AppDate
    {
        public static Paws_of_Hope.EF.AnimalShelterEntities Context { get; } = new Paws_of_Hope.EF.AnimalShelterEntities();
    }
}
