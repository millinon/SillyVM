using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    namespace OpCodes
    {
        public class Stack
        {
            public static void Register(VirtualMachine Machine)
            {
                Machine.RegisterFunction("PUSH", new Function(new ArgumentType[] { ArgumentType.ANY }, (VirtualMachine VM, Value[] Args) =>
                        {
                        VM.Stack.Push(Args[0]);
                        }));

                Machine.RegisterFunction("POP", new Function(new ArgumentType[] { ArgumentType.REGISTER }, (VirtualMachine VM, Value[] Args) =>
                        {
                        Args[0].Register.Contents = VM.Stack.Pop();
                        }));

                Machine.RegisterFunction("STACKSIZE", new Function(new ArgumentType[] { ArgumentType.REGISTER }, (VirtualMachine VM, Value[] Args) =>
                        {
                        Args[0].Register.Contents = new Value(VM.Stack.Count);
                        }));

            }
        }
    }
}
