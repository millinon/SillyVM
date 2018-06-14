using System;
using System.Collections.Generic;

namespace SillyVM.OpCodes
{
    public class Vector
    {
        public static void Register(VirtualMachine Machine)
        {
            Machine.RegisterOperation("APPEND", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.VECTOR, ArgumentType.ANY }, (VM, Args) =>
            {
                var list = new List<Value>(Args[1].Vector);
                list.Add(Args[2]);
                Args[0].Register.Contents = list;
            }));

            Machine.RegisterOperation("LEN", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.STRING | ArgumentType.VECTOR }, (VM, Args) =>
            {
                if (Args[1].ValueType == ValueType.STRING) Args[0].Register.Contents = Args[1].String.Length;
                else if (Args[1].ValueType == ValueType.VECTOR) Args[0].Register.Contents = Args[1].Vector.Count;
                else throw new InvalidOperationException();
            }));

            Machine.RegisterOperation("LOOKUP", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.VECTOR | ArgumentType.DICTIONARY, ArgumentType.INT | ArgumentType.STRING }, (VM, Args) =>
            {
                switch (Args[1].ValueType)
                {
                    case ValueType.VECTOR:
                        Args[0].Register.Contents = Args[1].Vector[Args[2].Int];
                        break;
                    case ValueType.DICTIONARY:
                        Args[0].Register.Contents = Args[1].Dictionary[Args[2].String];
                        break;
                    default: throw new InvalidOperationException();
                }
            }));
        }
    }
}
