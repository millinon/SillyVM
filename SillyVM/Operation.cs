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
            INT = 2,
            DOUBLE = 4,
            STRING = 8,
            VECTOR = 16,
            DICTIONARY = 32,
            REFERENCE = 64,
            TYPE = 128,
            REGISTER = 256,
            FUNCTION = 512,
            INSTRUCTION = 1024,

            ANY = 2048,
            
            NUMERIC = 6,
            COMPARABLE = 15,
            EQUATABLE = 143,
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
