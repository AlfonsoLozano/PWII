using System;

namespace Practical_Work_2
{
    public class DecimalToBinary : Conversion
    {
        public DecimalToBinary(string name, string definition) : base(name,definition,true, new DecimalInputValidator())
        {
        }

        public override string Change(string input)
        {
            int number = Int32.Parse(input);

            if (number == 0) return "0";

            string binaryString = "";

            while (number > 0)
            {
                int remainder = number % 2;
                binaryString = remainder + binaryString;
                number /= 2;
            }

            return binaryString;
        }

        public override string Change(string input, int bits)
        {
            string binaryString = this.Change(input);

            if (binaryString.Length > bits)
            {
                throw new ArgumentOutOfRangeException(nameof(input), $"Number must fit within {bits} bits.");
            }

            else if (binaryString.Length < bits)
            {
                binaryString = binaryString.PadLeft(bits,'0');
            }
            return binaryString;
        }
    }
}