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
                Machine.RegisterFunction("NOOP", new Function(new ArgumentType[] { }, (VirtualMachine VM, Value[] Args) => 
                            {

                            }));

                Machine.RegisterFunction("JUMP", new Function(new ArgumentType[] { ArgumentType.INSTRUCTION }, (VirtualMachine VM, Value[] Args) =>
                            {
                            VM.PC = Args[0].Instruction;
                            }));

                Machine.RegisterFunction("JUMPIF", new Function(new ArgumentType[] { ArgumentType.BOOL, ArgumentType.INSTRUCTION }, (VirtualMachine VM, Value[] Args) =>
                            {
                            if (Args[0].Bool) VM.PC = Args[1].Instruction;
                            }));

                Machine.RegisterFunction("PRINT", new Function(new ArgumentType[] { ArgumentType.ANY }, (VirtualMachine VM, Value[] Args) =>
                            {
                            Console.WriteLine(Args[0].ToString());
                            }));

                Machine.RegisterFunction("SLEEP", new Function(new ArgumentType[] { ArgumentType.INT }, (VirtualMachine VM, Value[] Args) =>
                            {
                            Thread.Sleep(Args[0].Int);
                            }));


                Machine.RegisterFunction("HALT", new Function(new ArgumentType[] { }, (VirtualMachine VM, Value[] Args) =>
                            {
                            VM.Halt();
                            }));
            }
        }
    }
}
