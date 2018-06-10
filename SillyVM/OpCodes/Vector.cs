using System;

namespace SillyVM
{
    namespace OpCodes
    {
        public class Vector
        {
            public static void Register(VirtualMachine Machine)
            {
                Machine.RegisterFunction("VEC", new Function(new ArgumentType[]{ ArgumentType.REGISTER }, (VM, Args) => {
                   Args[0].Register.Contents = new List<Value>(); 
                        }));
            }
        }
    }
}
