using System;

namespace Practical_Work_2
{
    public class DecimalInputValidator : InputValidator
    {
        public  override void validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new FormatException("Bad Format: Empty input");

            bool isNegative = input[0] == '-';
            int startIndex = isNegative ? 1 : 0;

            
            if (isNegative && input.Length == 1)
                throw new FormatException("Bad Format: not valid number");

        
            for (int i = startIndex; i < input.Length; i++)
            {
                if (!char.IsDigit(input[i]))
                {
                    throw new FormatException("Batd Format: not valid integer");
                }
            }
        }
    }
}