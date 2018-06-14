using System.Collections.Generic;

using SillyVM;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            VirtualMachine VM = new VirtualMachine();

            var r0 = VM.AddRegister("R0");
            var r1 = VM.AddRegister("R1");
            var acc = VM.AddRegister("ACC");

            Instruction loop;

            var Program = new List<Instruction>(){
                new Instruction("LOAD", r0, new List<Value>()),
                    new Instruction("APPEND", r0, r0, "Hello"),
                    new Instruction("APPEND", r0, r0, "world!"),
                    new Instruction("APPEND", r0, r0, "I'm"),
                    new Instruction("APPEND", r0, r0, "in"),
                    new Instruction("APPEND", r0, r0, "a"),
                    new Instruction("APPEND", r0, r0, "loop!"),
                    new Instruction("LOAD", acc, 0),
            (loop = new Instruction("LOOKUP", r1, r0, acc)),
                    new Instruction("CONWRITELN", r1),
                    new Instruction("INC", acc),
                    new Instruction("LEN", r1, r0),
                    new Instruction("LT", r1, acc, r1),
                    new Instruction("JUMPIF", r1, loop),
                    new Instruction("HALT"),
            };

            Assembler.Link(Program);

            VM.Load(Program);
            VM.Run();
        }
    }
}
