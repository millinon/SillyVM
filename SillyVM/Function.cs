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
        }

    public class Function
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

        public bool IsNative;
        private List<Instruction> Procedure;
        private Action<VirtualMachine,Value[]> NativeProcedure;

        private Function(ArgumentType[] Args)
        {
            Arity = Args.Length;
            ArgumentTypes = (ArgumentType[])Args.Clone();
        }

        public Function(ArgumentType[] Args, List<Instruction> Procedure) : this(Args)
        {
            this.Procedure = new List<Instruction>(Procedure);
            IsNative = false;
        }

        public Function(ArgumentType[] Args, Action<VirtualMachine,Value[]> NativeProcedure) : this(Args)
        {
            this.NativeProcedure = NativeProcedure;
            IsNative = true;
        }

        public void Invoke(VirtualMachine VM, Value[] Parameters)
        {
            var validated = ValidateArguments(Parameters);

            if (IsNative)
            {
                this.NativeProcedure.Invoke(VM, validated);
            } else
            {

            }
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
