using System;
using System.Collections.Generic;

namespace DivideConquer
{
    internal class Attribute
    {
        public string attributeName;

        public List<Option> options;

        public double averageInformationValue;

        public Attribute(string attributeName, List<Option> options)
        {
            this.attributeName = attributeName;

            this.options = options;
        }

        public void setAverageInformationValue()
        {
            int overallYes = 0;
            int overallNo = 0;

            foreach (Option option in this.options)
            {
                overallYes += option.yesAmount;
                overallNo += option.noAmount;
            }

            double tempYes = (double)overallYes / (overallYes + overallNo);
            double tempNo = (double)overallNo / (overallYes + overallNo);

            double tempInformationValue = -(tempYes * Math.Log(tempYes, 2) + tempNo * Math.Log(tempNo, 2));

            int overallYesNo = 0;
            double tempResult = 0;

            foreach (Option option in this.options)
            {
                overallYesNo = option.yesAmount + option.noAmount;

                tempResult += (double)overallYesNo / (overallYes + overallNo) * option.informationValue;
            }

            this.averageInformationValue = tempInformationValue - tempResult;

            Console.WriteLine("   " + this.averageInformationValue);
        }
    }
}
