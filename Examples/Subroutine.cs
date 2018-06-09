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

            VM.AddRegister("R0");

            var subroutine = new List<Instruction>(){
                new Instruction("PRINT", "(hello from subroutine)"),
                    new Instruction("PRINT", VM.Registers["R0"]),
                    new Instruction("PRINT", "(goodbye from subroutine)"),
            };

            Assembler.Link(subroutine);

            VM.RegisterFunction("MYFUNC", new Function(subroutine));

            var Program = new List<Instruction>(){
                new Instruction("LOAD", VM.Registers["R0"], "Hello, world!"),
                    new Instruction("PRINT", "(calling subroutine)"),
                    new Instruction("MYFUNC"),
                    new Instruction("PRINT","(subroutine done)"),
                    new Instruction("HALT"),
            };

            Assembler.Link(Program);

            VM.Load(Program);
            VM.Run();
        }
    }
}
