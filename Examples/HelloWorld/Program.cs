using System.Collections.Generic;

using SillyVM;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            VirtualMachine VM = new VirtualMachine();

            var Program = new List<Instruction>(){
                new Instruction("CONWRITELN", "Hello, world!"),
                new Instruction("HALT"),
            };

            Assembler.Link(Program);

            VM.Load(Program);
            VM.Run();
        }
    }
}
