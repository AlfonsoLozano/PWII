using System;

namespace Practical_Work_2
{
    public class BinaryToDecimal : Conversion
    {
        public BinaryToDecimal(string name, string definition) : base(name,definition, new BinaryInputValidator())
        {
        }

        public override string Change(string input)
        {
            int number = Convert.ToInt32(input, 2);

            if (number == 0) return "0";

            int decimalString = 0;

            for (int i = input.Length - 1; i >= 0; i--) 
            {
                double power = (input[i] - '0') * Math.Pow(2 , input.Length - 1 - i);
                decimalString += (int)power;
            }

            return decimalString.ToString();
        }
    }
}