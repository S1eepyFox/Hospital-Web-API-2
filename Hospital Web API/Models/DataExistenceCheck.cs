using Hospital_Web_API.Companion_object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Hospital_Web_API.Models
{
    public class DataExistenceCheck //Проверка на реалистичность полученных параметров человека
    {
        public static bool GetAge(double age)
        {
            bool check = false;

            if (age < Constants.minAge || age > Constants.maxAge) 
                check = true;
            
            return check;
        }
        public static bool GetMass(double mass)
        {
            bool check = false;

            if (mass < Constants.minMass || mass > Constants.maxMass) 
                check = true;

            return check;
        }
        public static bool GetHeight(double height)
        {
            bool check = false;

            if (height < Constants.minHeight || height > Constants.maxHeight) 
                check = true;

            return check;
        }

       
    }
}