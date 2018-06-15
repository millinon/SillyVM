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
                Machine.RegisterOperation("NOOP", new Operation(new ArgumentType[] { }, (VM, Args) => 
                            {

                            }));

                Machine.RegisterOperation("JUMP", new Operation(new ArgumentType[] { ArgumentType.INSTRUCTION }, (VM, Args) =>
                            {
                            VM.PC = Args[0].Instruction;
                            }));

                Machine.RegisterOperation("JUMPIF", new Operation(new ArgumentType[] { ArgumentType.BOOL, ArgumentType.INSTRUCTION }, (VM, Args) =>
                            {
                            if (Args[0].Bool) VM.PC = Args[1].Instruction;
                            }));

                Machine.RegisterOperation("DUMP", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.ANY }, (VM, Args) =>
                            {
                            Args[0].Register.Contents = Args[1].ToString();
                            }));

                Machine.RegisterOperation("SLEEP", new Operation(new ArgumentType[] { ArgumentType.INT }, (VM, Args) =>
                            {
                            Thread.Sleep(Args[0].Int);
                            }));

                Machine.RegisterOperation("HALT", new Operation(new ArgumentType[] { }, (VM, Args) =>
                            {
                            VM.Halt();
                            }));

                Machine.RegisterOperation("CALL", new Operation(new ArgumentType[] { ArgumentType.FUNCTION }, (VM, Args) =>
                            {
                            VM.CallStack.Push(VM.PC);
                            VM.PC = Args[0].Function.Procedure[0];
                            }));

                Machine.RegisterOperation("MKINST", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.STRING, ArgumentType.VECTOR }, (VM, Args) => 
                            {
                            Args[0].Register.Contents = new Instruction(Args[1].String, Args[2].Vector.ToArray());
                            }));

                Machine.RegisterOperation("LINK", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.VECTOR }, (VM, Args) =>
                            {
                            Args[0].Register.Contents = Assembler.Link(Args[1].Vector.Select(val => val.Instruction).ToList()).Select(inst => new Value(inst)).ToList();
                            }));
            }
        }
    }
}
