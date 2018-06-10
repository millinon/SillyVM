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
                Machine.RegisterOperation("PUSH", new Operation(new ArgumentType[] { ArgumentType.ANY }, (VM, Args) =>
                        {
                        VM.Stack.Push(Args[0]);
                        }));

                Machine.RegisterOperation("POP", new Operation(new ArgumentType[] { ArgumentType.REGISTER }, (VM, Args) =>
                        {
                        Args[0].Register.Contents = VM.Stack.Pop();
                        }));

                Machine.RegisterOperation("STACKSIZE", new Operation(new ArgumentType[] { ArgumentType.REGISTER }, (VM, Args) =>
                        {
                        Args[0].Register.Contents = new Value(VM.Stack.Count);
                        }));

            }
        }
    }
}
