using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paws_of_Hope.EF
{
    public partial class VW_PetTutor
    {
        public string GetAge { get => $"Возраст: {AgePet}"; }
        public string GetGender { get => $"Пол: {GenderPet}"; }
        public string GetAnimalShelter { get => $"Приют: {AnimalShelteFullName}"; }
        public string GetClient { get => $"Владелец: {ClientFullName}"; }
        public string GetTutor { get => $"Наставник: {TutorFullName}"; }
        public string GetDisas { get => $"Болезни: {DiseasesPet}"; }
        public string GetHistory { get => $"История: {HistoryPet}"; }

    }
}
