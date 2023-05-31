using Paws_of_Hope.EF;
using System.Collections.Generic;
using System.Linq;

namespace Paws_of_Hope.Class
{
    public class AppDate
    {
        public static EF.AnimalShelterEntities Context { get; } = new EF.AnimalShelterEntities();

        public static List<string> GetAllAnimalShelter()
        {
            return Context.AnimalShelter.Select(p => p.NameAnimalShelter).Distinct().ToList();
        }

        public static List<string> GetAllGender()
        {
            return Context.Gender.Select(p => p.NameGender).Distinct().ToList();
        }

        public static List<string> GetAllStatus()
        {
            return Context.StatusClient.Select(p => p.NameStatus).Distinct().ToList();
        }

        public static List<string> GetAllSize()
        {
            return Context.SizePet.Select(p => p.NameSizePet).Distinct().ToList();
        }

        public static List<string> GetAllTypePet()
        {
            return Context.TypePet.Select(p => p.NameTypePet).Distinct().ToList();
        }

        public static List<Tutor> GetAllTutor()
        {
            return Context.Tutor.ToList();
        }

        public static List<Pet> GetAllPet()
        {
            return Context.Pet.ToList();
        }

        public static List<ExecutedApplication> GetAllApplication()
        {
            return Context.ExecutedApplication.ToList();
        }

        public static List<Client> GetAllClient()
        {
            return Context.Client.ToList();
        }
    }
}
