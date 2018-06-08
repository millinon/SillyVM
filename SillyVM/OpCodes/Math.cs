using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    namespace OpCodes
    {
        public class Math
        {
            public static void Register(VirtualMachine Machine)
            {
                Machine.RegisterFunction("INC", new Function(new ArgumentType[] { ArgumentType.REGISTER }, (VirtualMachine VM, Value[] Args) =>
                        {
                        Args[0].Register.Contents = Args[0].Register.Contents.Int + 1;
                        }));

                Machine.RegisterFunction("DEC", new Function(new ArgumentType[] { ArgumentType.REGISTER }, (VirtualMachine VM, Value[] Args) =>
                        {
                        Args[0].Register.Contents = Args[0].Register.Contents.Int - 1;
                        }));
            }
        }
    }
}
