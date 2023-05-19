using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paws_of_Hope.EF
{
    public partial class Tutor
    {
        public string GetTutor { get => $"Наставник: {TutorFIO.LastName} {TutorFIO.FirstName} {TutorFIO.Patronymic}"; }
    }

   
}
