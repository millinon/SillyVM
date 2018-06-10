using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    namespace OpCodes
    {
        public class Types
        {
            public static void Register(VirtualMachine Machine)
            {
                Machine.RegisterOperation("TYPEOF", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.ANY }, (VM, Args) => {
                    Args[0].Register.Contents = Args[1].ValueType;
                            }));
            }
        }
    }
}
