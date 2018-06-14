using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    namespace OpCodes
    {
        public class Console
        {
            private static bool validColor(int Color)
            {
                var color = (System.ConsoleColor) Color;

                var colors = (System.ConsoleColor[]) Enum.GetValues(typeof(System.ConsoleColor));

                return colors.Contains(color);
            }

            public static void Register(VirtualMachine Machine)
            {
                Machine.RegisterOperation("CONWRITELN", new Operation(new ArgumentType[] { ArgumentType.STRING }, (VM, Args) =>
                            {
                            System.Console.WriteLine(Args[0].String);
                            }));
                Machine.RegisterOperation("CONWRITE", new Operation(new ArgumentType[] { ArgumentType.STRING }, (VM, Args) => 
                            {
                            System.Console.Write(Args[0].String);
                            }));
                Machine.RegisterOperation("CONREADLN", new Operation(new ArgumentType[] { ArgumentType.REGISTER }, (VM, Args) =>
                            {
                            Args[0].Register.Contents = System.Console.ReadLine();
                            }));
                Machine.RegisterOperation("CONBEEP", new Operation(new ArgumentType[] { }, (VM, Args) =>
                            {
                            System.Console.Beep();
                            }));
                Machine.RegisterOperation("CONGETFG", new Operation(new ArgumentType[] { ArgumentType.REGISTER }, (VM, Args) =>
                            {
                            Args[0].Register.Contents = (int) System.Console.ForegroundColor;
                            }));
                Machine.RegisterOperation("CONSETFG", new Operation(new ArgumentType[] { ArgumentType.INT }, (VM, Args) =>
                            {
                            if(!validColor(Args[0].Int)) throw new InvalidOperationException();

                            System.Console.ForegroundColor = (System.ConsoleColor) Args[0].Int;
                            }));
                Machine.RegisterOperation("CONGETBG", new Operation(new ArgumentType[] { ArgumentType.REGISTER }, (VM, Args) =>
                            {
                            Args[0].Register.Contents = (int) System.Console.BackgroundColor;
                            }));
                Machine.RegisterOperation("CONSETBG", new Operation(new ArgumentType[] { ArgumentType.INT }, (VM, Args) =>
                            {
                            if(!validColor(Args[0].Int)) throw new InvalidOperationException();

                            System.Console.BackgroundColor = (System.ConsoleColor) Args[0].Int;
                            }));

            }
        }
    }
}
