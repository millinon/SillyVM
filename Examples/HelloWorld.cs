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

            var Program = new List<Instruction>(){
                new Instruction("PRINT", "Hello, world!"),
                new Instruction("HALT"),
            };

            Assembler.Link(Program);

            VM.Load(Program);
            VM.Run();
        }
    }
}
