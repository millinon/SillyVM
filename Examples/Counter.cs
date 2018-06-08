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
            VM.AddRegister("ACC");
            
            var loop = new Instruction("PRINT", VM.Registers["ACC"]);
                
            var Program = new List<Instruction>(){
                new Instruction("LOAD", VM.Registers["ACC"], 0),
                loop,
                new Instruction("INC", VM.Registers["ACC"]),
                new Instruction("LT", VM.Registers["R0"], VM.Registers["ACC"], 10),
                new Instruction("JUMPIF", VM.Registers["R0"], loop),
                new Instruction("HALT"),
            };

            Assembler.Link(Program);

            VM.Load(Program);
            VM.Run();
        }
    }
}
