using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital_Web_API.Models;

namespace Hospital_Web_API.Companion_object
{
    public class Constants
    {

       public static readonly IndicatorsBMI_WHO[] indicatorsBMI = new IndicatorsBMI_WHO[] //Интерпретация показателей ИМТ в соответствии с рекомендациями ВОЗ 
       {
           new IndicatorsBMI_WHO() {maxIndex =16, minIndex = 0,description = "Выраженный дефицит массы тела" },
           new IndicatorsBMI_WHO() {maxIndex =18.5, minIndex = 16.1,description = "Недостаточная (дефицит) масса тела" },
           new IndicatorsBMI_WHO() {maxIndex =25, minIndex = 18.6,description = "Норма" },
           new IndicatorsBMI_WHO() {maxIndex =30, minIndex = 25.1,description = "Избыточная масса тела (предожирение)" },
           new IndicatorsBMI_WHO() {maxIndex =35, minIndex = 30.1,description = "Ожирение 1 степени" },
           new IndicatorsBMI_WHO() {maxIndex =40, minIndex = 35.1,description = "Ожирение 2 степени" },
           new IndicatorsBMI_WHO() {maxIndex =10000, minIndex = 40.1 ,description = "Ожирение 3 степени" },
       };

        //Максимальные и минимальные параметры человека 
        public const double minHeight = 50;
        public const double maxHeight = 250;

        public const double minMass = 2.5;
        public const double maxMass = 600;

        public const int minAge = 0;
        public const int maxAge = 120;



        


    }
}