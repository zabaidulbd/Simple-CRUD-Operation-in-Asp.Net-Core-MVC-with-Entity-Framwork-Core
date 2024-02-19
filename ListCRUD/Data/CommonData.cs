using ListCRUD.Models;
using System;
using System.Collections.Generic;

namespace ListCRUD.Data
{
    public class CommonData
    {
        public IEnumerable<PersonalInfo> GetPersonalInfoList()
        {
            List<PersonalInfo> listPersonalInfo = new List<PersonalInfo>();


            for (int i = 0; i < 105000; i++)
            {
                Random _Random = new Random();
                PersonalInfo _PersonalInfo = new PersonalInfo
                {
                    FirstName = "Tom-" + GenerateString(6),
                    LastName = GenerateString(5),
                    DateOfBirth = DateTime.Now.AddDays(-_Random.Next(52)),
                    City = GenerateString(4),
                    Country = GenerateString(4),
                    MobileNo = _Random.Next(1000, 100000).ToString(),
                    Email = "dev@" + GenerateString(6),
                    PasportNo = _Random.Next(1000, 1000000).ToString(),
                    NID = _Random.Next(1000, 1000000).ToString(),

                    CreatedDate = DateTime.Now.AddDays(-_Random.Next(30)),
                    ModifiedDate = DateTime.Now.AddDays(-_Random.Next(30)),
                    CreatedBy = "Admin",
                    ModifiedBy = "Admin",
                    Cancelled = false
                };
                listPersonalInfo.Add(_PersonalInfo);
            }
            return listPersonalInfo;
        }

        Random _Random = new Random();
        public const string Alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public string GenerateString(int size)
        {
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = Alphabet[_Random.Next(Alphabet.Length)];
            }
            return new string(chars);
        }
    }
}