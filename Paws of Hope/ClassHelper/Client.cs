using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paws_of_Hope.EF
{
    public partial class Client
    {
        public string GetAge { get => $"Дата рождения: {Birthday:dd/MM/yyyy}"; }
        public string GetGender { get => $"Пол: {Gender.NameGender}"; }
        public string GetFullName { get => $"{LastName} {FirstName}"; }
        public string GetStatus { get => $"Статус: {StatusClient.NameStatus}"; }
        public string GetPhone { get => $"Телефон: {Phone}"; }
        public string GetEmail { get => $"Email: {Email}"; }

    }
}
