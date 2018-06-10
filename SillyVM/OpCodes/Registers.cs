using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    namespace OpCodes
    {
        public class Registers
        {
            public static void Register(VirtualMachine Machine)
            {
                Machine.RegisterOperation("LOAD", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.ANY }, (VM, Args) =>
                        {
                        Args[0].Register.Contents = Args[1];
                        }));

                Machine.RegisterOperation("MOVE", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.REGISTER }, (VM, Args) =>
                        {
                        Args[0].Register.Contents = Args[1].Register.Contents;
                        }));

                Machine.RegisterOperation("ADDREG", new Operation(new ArgumentType[] { ArgumentType.STRING }, (VM, Args) => 
                        {
                        var regname = Args[0].String;
                        if(VM.Registers.Keys.Contains(regname)) throw new InvalidOperationException();
                        VM._registers[regname] = new Register(regname, VM);
                        }));

                Machine.RegisterOperation("ADDCONST",  new Operation(new ArgumentType[] { ArgumentType.STRING, ArgumentType.ANY }, (VM, Args) => 
                        {
                        var regname = Args[0].String;
                        if(VM.Registers.Keys.Contains(regname)) throw new InvalidOperationException();
                        VM._registers[regname] = new Register(regname, Args[1], VM);
                        }));
            }
        }
    }
}
