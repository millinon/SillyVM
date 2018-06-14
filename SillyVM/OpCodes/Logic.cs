using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SillyVM.OpCodes
{
    class Logic
    {
        public static void Register(VirtualMachine Machine)
        {
            Machine.RegisterOperation("NOT", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.BOOL }, (VM, Args) =>
            {
                Args[0] = !Args[1].Bool;
            }));

            Machine.RegisterOperation("AND", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.BOOL, ArgumentType.BOOL }, (VM, Args) =>
            {
                Args[0] = Args[1].Bool && Args[2].Bool;
            }));

            Machine.RegisterOperation("OR", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.BOOL, ArgumentType.BOOL }, (VM, Args) =>
            {
                Args[0] = Args[1].Bool || Args[2].Bool;
            }));

            Machine.RegisterOperation("XOR", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.BOOL, ArgumentType.BOOL }, (VM, Args) =>
            {
                Args[0] = Args[1].Bool ^ Args[2].Bool;
            }));

            Machine.RegisterOperation("NAND", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.BOOL, ArgumentType.BOOL }, (VM, Args) =>
            {
                Args[0] = !(Args[1].Bool && Args[2].Bool);
            }));

            Machine.RegisterOperation("NOR", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.BOOL, ArgumentType.BOOL }, (VM, Args) =>
            {
                Args[0] = !(Args[1].Bool || Args[2].Bool);
            }));

            Machine.RegisterOperation("XNOR", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.BOOL, ArgumentType.BOOL }, (VM, Args) =>
            {
                Args[0] = !(Args[1].Bool ^ Args[2].Bool);
            }));
        }
    }
}
