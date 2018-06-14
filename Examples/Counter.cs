using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SillyVM;
using static SillyVM.VirtualMachine;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            VirtualMachine VM = new VirtualMachine(); 

            var r0 = VM.AddRegister("R0");
            var acc = VM.AddRegister("ACC");
            
            Instruction loop;
                
            var Program = new List<Instruction>(){
                new Instruction("LOAD", acc, 0),
        (loop = new Instruction("DUMP", r0, acc)),
                new Instruction("PRINT", r0),
                new Instruction("INC", acc),
                new Instruction("LT", r0, acc, 10),
                new Instruction("SLEEP", 500),
                new Instruction("JUMPIF", r0, loop),
                new Instruction("HALT"),
            };

            Assembler.Link(Program);

            VM.Load(Program);
            VM.Run();
        }
    }
}
