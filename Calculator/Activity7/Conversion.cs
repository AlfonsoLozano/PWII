using System;
using Microsoft.VisualBasic;

namespace Practical_Work_2
{
    public abstract class Conversion 
    {
        protected string name;
        protected string definition;
        protected bool bitsize;
        protected InputValidator validator;

        public Conversion(string name, string definition, InputValidator validator)
        {
            this.name = name;
            this.definition = definition;
            this.validator = validator;
            this.bitsize = false;
        }

        public Conversion(string name, string definition, bool bitsize, InputValidator validator)
        {
            this.name = name;
            this.definition = definition;
            this.validator = validator;
            this.bitsize = bitsize;
        }


        public abstract String Change(string input);
        
        public virtual string Change(string input, int bits)
        {
            throw new NotImplementedException("This method is not implemented in this subclass.");
        }

        public string GetName()
        {
            return this.name;
        }
        public string GetDefinition()
        {
            return this.definition;
        }
        public bool NeedBitSize()
        {
            return this.bitsize;
        }

        public void validate(string input)
        {
           this.validator.validate(input);
        }
    }
}