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

            var Program = new List<Instruction>(){
                new Instruction("LOAD", r0, new Dictionary<string, Value>(){
                        {"key1", true},
                        {"pi", 3.14159265359},
                        {"arr", new List<Value>(){
                        1, "2", false
                        }},
                        {"dict", new Dictionary<string, Value>(){
                        {"key2", "look ma, I'm in a dictionary!"},
                        {"key3", "this is very silly"}
                        }},
                        }),
                    new Instruction("CONV", r0, r0, SillyVM.ValueType.STRING),
                    new Instruction("HALT"),
            };

            Assembler.Link(Program);

            VM.Load(Program);
            VM.Run();

            Console.WriteLine(r0.Contents.String);
        }
    }
}
