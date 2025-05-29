using System;
using System.Collections;
using System.Collections.Generic;

namespace Practical_Work_2
{
    public class Converter
    {
        private List<Conversion> operations;
        public List<Conversion> Operations => operations;

        public Converter()
        {
            operations = new List<Conversion>
            {
                new DecimalToBinary("Binary", "Decimal to Binary"),
                new DecimalToTwoComplement("TwoComplement", "Decimal to Two's Complement"),
                new DecimalToOctal("Octal", "Decimal to Octal"),
                new DecimalToHexadecimal("Hexadecimal", "Decimal to Hexadecimal"),
                new BinaryToDecimal("Decimal", "Binary to Decimal"),
                new TwoComplementToDecimal("Decimal", "Two's Complement to Decimal"),
                new OctalToDecimal("Decimal", "Octal to Decimal"),
                new HexadecimalToDecimal("Decimal", "Hexadecimal to Decimal")
            };
        }
        
        
        public string PerformConversion(int op, string input)
        {
            this.operations[op-1].validate(input);
            if (this.operations[op-1].NeedBitSize())
            {
                Console.WriteLine("How many bits should i use: ");
                int bits = Int32.Parse(Console.ReadLine());

                return this.operations[op-1].Change(input,bits);
            }
            return this.operations[op-1].Change(input);
        }
    }
}