using System;

namespace DivideConquer
{
    internal class Option
    {
        public string optionName;

        public int yesAmount;
        public int noAmount;

        public double informationValue;

        public Option(string optionName)
        {
            this.optionName = optionName;
        }

        public void setInformationValue()
        {
            double tempYes = (double)this.yesAmount / (this.yesAmount + this.noAmount);
            double tempNo = (double)this.noAmount / (this.yesAmount + this.noAmount);

            this.informationValue = -(tempYes * Math.Log(tempYes, 2) + tempNo * Math.Log(tempNo, 2));
        }
    }
}
