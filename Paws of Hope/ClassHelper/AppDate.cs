using Paws_of_Hope.EF;
using System.Collections.Generic;
using System.Linq;

namespace Paws_of_Hope.Class
{
    public class AppDate
    {
        public static AnimalShelterEntities context { get; set; } = new AnimalShelterEntities();

        public static List<string> GetAllAnimalShelter()
        {
            return context.AnimalShelter.Select(p => p.NameAnimalShelter).Distinct().ToList();
        }

        public static List<string> GetAllGender()
        {
            return context.Gender.Select(p => p.NameGender).ToList();
        }

        public static List<string> GetAllStatus()
        {
            return context.StatusClient.Select(p => p.NameStatus).Distinct().ToList();
        }

        public static List<string> GetAllSize()
        {
            return context.SizePet.Select(p => p.NameSizePet).Distinct().ToList();
        }

        public static List<string> GetAllTypePet()
        {
            return context.TypePet.Select(p => p.NameTypePet).Distinct().ToList();
        }

        public static List<Tutor> GetAllTutor()
        {
            return context.Tutor.ToList();
        }

        public static List<Pet> GetAllPet()
        {
            return context.Pet.ToList();
        }

        public static List<ExecutedApplication> GetAllApplication()
        {
            return context.ExecutedApplication.ToList();
        }

        public static List<Client> GetAllClient()
        {
            return context.Client.ToList();
        }
    }
}
