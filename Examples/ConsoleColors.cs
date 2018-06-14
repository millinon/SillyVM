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

            var cols = VM.AddRegister("COLS");
            var numcols = VM.AddRegister("NUMCOLS");
            var x = VM.AddRegister("X");
            var y = VM.AddRegister("Y");
            var fg = VM.AddRegister("FG");
            var bg = VM.AddRegister("BG");
            var r0 = VM.AddRegister("R0");

            Instruction loop1, loop2;

            var colors = ((System.ConsoleColor[]) Enum.GetValues(typeof(System.ConsoleColor))).Select((System.ConsoleColor obj) => new Value((int) obj)).ToList();

            var Program = new List<Instruction>(){
                new Instruction("LOAD", cols, colors),
                    new Instruction("LEN", numcols, cols),
                    new Instruction("LOAD", x, 0),
                    new Instruction("LOAD", y, 0),
            (loop1 = new Instruction("LOOKUP", fg, cols, y)),
                    new Instruction("CONSETFG", fg),
            (loop2 = new Instruction("LOOKUP", bg, cols, x)),
                    new Instruction("CONSETBG", bg),
                    new Instruction("CONWRITE", "X"),
                    new Instruction("INC", x), 
                    new Instruction("LT", r0, x, numcols),
                    new Instruction("JUMPIF", r0, loop2),
                    new Instruction("LOAD", x, 0),
                    new Instruction("CONWRITELN", ""),
                    new Instruction("INC", y),
                    new Instruction("LT", r0, y, numcols),
                    new Instruction("JUMPIF", r0, loop1),
                    new Instruction("HALT"),
            };

            Assembler.Link(Program);

            VM.Load(Program);
            VM.Run();
        }
    }
}
