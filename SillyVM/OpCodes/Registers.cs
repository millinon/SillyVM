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
                Machine.RegisterFunction("LOAD", new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.ANY }, (VirtualMachine VM, Value[] Args) =>
                        {
                        Args[0].Register.Contents = Args[1];
                        }));

                Machine.RegisterFunction("MOVE", new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.REGISTER }, (VirtualMachine VM, Value[] Args) =>
                        {
                        Args[0].Register.Contents = Args[1].Register.Contents;
                        }));

                Machine.RegisterFunction("ADDREG", new Function(new ArgumentType[] { ArgumentType.STRING }, (VirtualMachine VM, Value[] Args) => 
                        {
                        var regname = Args[0].String;
                        if(VM.Registers.Keys.Contains(regname)) throw new InvalidOperationException();
                        VM._registers[regname] = new Register(regname, VM);
                        }));

                Machine.RegisterFunction("ADDCONST",  new Function(new ArgumentType[] { ArgumentType.STRING, ArgumentType.ANY }, (VirtualMachine VM, Value[] Args) => 
                        {
                        var regname = Args[0].String;
                        if(VM.Registers.Keys.Contains(regname)) throw new InvalidOperationException();
                        VM._registers[regname] = new Register(regname, Args[1], VM);
                        }));
            }
        }
    }
}
