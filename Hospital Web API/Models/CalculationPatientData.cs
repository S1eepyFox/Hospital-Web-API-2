using Hospital_Web_API.Companion_object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital_Web_API.Models
{
    public class CalculationPatientData
    {
        public static BodyMassIndex CalculationBMI(double mass, double height)
        {
            BodyMassIndex bmi = new BodyMassIndex();

            bmi.BMI = Math.Round(mass / Math.Pow(height / 100, 2));

            for (int i = 0; i < Constants.indicatorsBMI.Length; i++) //Сравнение с вычисленного ИМТ с рекомендациями ВОЗ 
            {
                if (bmi.BMI <= Constants.indicatorsBMI[i].maxIndex && bmi.BMI > Constants.indicatorsBMI[i].minIndex)
                {
                    bmi.Description = Constants.indicatorsBMI[i].description;
                }
            }

            return bmi;
        }
    }
}