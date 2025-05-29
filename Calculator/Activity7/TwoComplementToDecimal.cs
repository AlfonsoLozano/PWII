using System;
using System.Reflection.PortableExecutable;

namespace Practical_Work_2
{
    public class TwoComplementToDecimal : Conversion
    {
        public TwoComplementToDecimal(string name, string definition) : base(name,definition,new BinaryInputValidator())
        {
        }

        public override string Change(string input)
        {
            int number = Int32.Parse(input);

            int n = input.Length;
            bool isNegative = input[0] == '1';

            int result = 0;

            if (!isNegative) //If the number is not negative, calculate as normal
            {
                for (int i = 0; i < n; i++)
                {
                    int bit = input[i] - '0';
                    result += bit * (int)Math.Pow(2, n - i - 1);
                }
            }else //If the number is negative, change the 1 for the 0, and 0 for 1. Then calculate, and finaly negate the result 
            {
                char[] inverted = new char[n];
                for (int i = 0; i < n; i++)
                {
                    inverted[i] = input[i] == '0' ? '1' : '0'; 
                }

                for (int i = n - 1; i >= 0; i--)
                {
                    if (inverted[i] == '0')
                    {
                        inverted[i] = '1';
                        break;
                    }else
                    {
                        inverted[i] = '0';
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    int bit = input[i] - '0';
                    result += bit * (int)Math.Pow(2, n - i - 1);
                }

                result *= -1;
            }

            return result.ToString();
        }
    }
}