using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    namespace OpCodes
    {
        public class Util
        {
            public static void Register(VirtualMachine Machine)
            {
                Machine.RegisterOperation("NOOP", new Operation(new ArgumentType[] { }, (VirtualMachine VM, Value[] Args) => 
                            {

                            }));

                Machine.RegisterOperation("JUMP", new Operation(new ArgumentType[] { ArgumentType.INSTRUCTION }, (VirtualMachine VM, Value[] Args) =>
                            {
                            VM.PC = Args[0].Instruction;
                            }));

                Machine.RegisterOperation("JUMPIF", new Operation(new ArgumentType[] { ArgumentType.BOOL, ArgumentType.INSTRUCTION }, (VirtualMachine VM, Value[] Args) =>
                            {
                            if (Args[0].Bool) VM.PC = Args[1].Instruction;
                            }));

                Machine.RegisterOperation("PRINT", new Operation(new ArgumentType[] { ArgumentType.ANY }, (VirtualMachine VM, Value[] Args) =>
                            {
                            Console.WriteLine(Args[0].ToString());
                            }));

                Machine.RegisterOperation("SLEEP", new Operation(new ArgumentType[] { ArgumentType.INT }, (VirtualMachine VM, Value[] Args) =>
                            {
                            Thread.Sleep(Args[0].Int);
                            }));


                Machine.RegisterOperation("HALT", new Operation(new ArgumentType[] { }, (VirtualMachine VM, Value[] Args) =>
                            {
                            VM.Halt();
                            }));

                Machine.RegisterOperation("CALL", new Operation(new ArgumentType[] { ArgumentType.FUNCTION }, (VirtualMachine VM, Value[] Args) =>
                            {
                            VM.CallStack.Push(VM.PC);
                            VM.PC = Args[0].Function.Procedure[0];
                            }));
            }
        }
    }
}
