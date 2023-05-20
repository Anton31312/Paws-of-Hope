﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paws_of_Hope.EF
{
    public partial class ExecutedApplication
    {
        public string GetWishes { get => $"Пожелание: {Application.Wishes}"; }
        public string GetFullNameClient { get => $"{Application.Client.LastName} {Application.Client.FirstName} {Application.Client.Patronymic}"; }
        public string GetTypePet { get => $"Тип животного: {Application.TypePet.NameTypePet}"; }
        public string GetDateApp { get => $"Дата заявки: {DateExecutedApplication.Date.Date}"; }
        public string GetStatus { get => $"Статус заявки: {Application.StatusApplication.NameStatus}"; }
        public string GetGender { get => $"Пол животного: {Pet.Gender.NameGender}"; }
        public string GetAge { get => $"Возраст животного: {Pet.Age}"; }
    }
}
