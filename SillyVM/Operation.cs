using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    [Flags]
        public enum ArgumentType
        {
            VOID = 0,
            BOOL = 1,
            CHAR = 2,
            INT = 4,
            DOUBLE = 8,
            STRING = 16,
            VECTOR = 32,
            DICTIONARY = 64,
            REFERENCE = 128,
            TYPE = 256,
            REGISTER = 512,
            FUNCTION = 1024,
            INSTRUCTION = 2048,

            ANY = 4095,
            
            NUMERIC = 2+4+8,
            COMPARABLE = 1+2+4+8+16,
            CONVERTIBLE = 1+2+4+8+16+32+64+256,
            EQUATABLE = 1+2+4+8+16+256,
        }

    public class Operation
    {
        private static ArgumentType vt2at(ValueType VT)
        {
            ArgumentType AT;
            if(! Enum.TryParse(VT.ToString(), out AT)) throw new ArgumentException();
            return AT;
        }

        public readonly string Name;

        public readonly int Arity;
        public readonly ArgumentType[] ArgumentTypes;

        private Action<VirtualMachine,Value[]> NativeProcedure;

        public Operation(ArgumentType[] Args, Action<VirtualMachine,Value[]> NativeProcedure)
        {
            Arity = Args.Length;
            ArgumentTypes = (ArgumentType[])Args.Clone();
            this.NativeProcedure = NativeProcedure;
        }

        public void Invoke(VirtualMachine VM, Value[] Parameters)
        {
            var validated = ValidateArguments(Parameters);

            this.NativeProcedure.Invoke(VM, validated);
        }

        public Value[] ValidateArguments(Value[] Parameters)
        {
            if (Parameters.Length != Arity) throw new InvalidOperationException();

            var validated = new Value[Arity];

            for(int i = 0; i < Arity; i++)
            {
                var AT = ArgumentTypes[i];
                var VT = Parameters[i].ValueType;

                /* dereference register contents */
                if(VT == ValueType.REGISTER && AT != ArgumentType.REGISTER){
                    validated[i] = Parameters[i].Register.Contents;
                } else {
                    validated[i] = Parameters[i];
                }

                if((ArgumentTypes[i] & ArgumentType.ANY) == 0 && (vt2at(validated[i].ValueType) & ArgumentTypes[i]) == 0){
                    throw new ArgumentException();
                }
            }

            return validated;
        }
    }
}
