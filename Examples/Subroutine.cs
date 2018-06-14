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

            var subroutine = new List<Instruction>(){
                new Instruction("CONWRITELN", "(hello from subroutine)"),
                    new Instruction("CONWRITELN", r0),
                    new Instruction("CONWRITELN", "(goodbye from subroutine)"),
            };

            var func = new Function(Assembler.Link(subroutine));

            var Program = new List<Instruction>(){
                new Instruction("LOAD", r0, "Hello, world!"),
                    new Instruction("CONWRITELN", "(calling subroutine)"),
                    new Instruction("CALL", func),
                    new Instruction("CONWRITELN", "(subroutine done)"),
                    new Instruction("HALT"),
            };

            Assembler.Link(Program);

            VM.Load(Program);
            VM.Run();
        }
    }
}
