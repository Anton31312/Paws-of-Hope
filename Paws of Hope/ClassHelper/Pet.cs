using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paws_of_Hope.EF
{
    public partial class Pet
    {
        public string GetAge { get => $"Возраст: {Age}"; }
        public string GetGender { get => $"Пол: {Gender.NameGender}"; }
        public string GetAnimalShelter { get => $"Приют: {AnimalShelter}"; }
        public string GetClient { get => $"Владелец: {Client.LastName}, {Client.FirstName}, {Client.Patronymic}"; }
        public string GetTutor { get => $"Наставник: {Tutor}"; }
        public string GetDisas { get => $"Болезни: {Diseases}"; }
        public string GetHistory { get => $"История: {History}"; }

    }
}
