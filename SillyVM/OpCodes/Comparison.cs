using System;

namespace SillyVM.OpCodes
{
    public class Comparison
    {
        private struct cmp_result
        {
            public bool gt;
            public bool lt;
            public bool eq;
        }

        private static cmp_result cmp(Value A, Value B)
        {
            switch (A.ValueType)
            {
                case ValueType.VOID: throw new InvalidOperationException();
                case ValueType.BOOL:
                    if (B.ValueType != ValueType.BOOL) throw new InvalidOperationException();
                    else return new cmp_result
                    {
                        gt = A.Bool && !B.Bool,
                        lt = !A.Bool && B.Bool,
                        eq = A.Bool == B.Bool,
                    };
                case ValueType.CHAR:
                    switch (B.ValueType)
                    {
                        case ValueType.CHAR:
                            return new cmp_result
                            {
                                gt = A.Char > B.Char,
                                lt = A.Char < B.Char,
                                eq = A.Char == B.Char
                            };
                        case ValueType.INT:
                            return new cmp_result
                            {
                                gt = A.Char > B.Int,
                                lt = A.Char < B.Int,
                                eq = A.Char == B.Int,
                            };
                        case ValueType.DOUBLE:
                            return new cmp_result
                            {
                                gt = A.Char > B.Double,
                                lt = A.Char < B.Double,
                                eq = A.Char == B.Double
                            };
                        default: throw new InvalidOperationException();
                    }
                case ValueType.INT:
                    switch (B.ValueType)
                    {
                        case ValueType.CHAR:
                            return new cmp_result
                            {
                                gt = A.Int > B.Char,
                                lt = A.Int < B.Char,
                                eq = A.Int == B.Char,
                            };
                        case ValueType.INT:
                            return new cmp_result
                            {
                                gt = A.Int > B.Int,
                                lt = A.Int < B.Int,
                                eq = A.Int == B.Int,
                            };
                        case ValueType.DOUBLE:
                            return new cmp_result
                            {
                                gt = A.Int > B.Double,
                                lt = A.Int < B.Double,
                                eq = A.Int == B.Double,
                            };
                        default: throw new InvalidOperationException();
                    }
                case ValueType.DOUBLE:
                    switch (B.ValueType)
                    {
                        case ValueType.CHAR:
                            return new cmp_result
                            {
                                gt = A.Double > B.Char,
                                lt = A.Double < B.Char,
                                eq = A.Double == B.Char,
                            };
                        case ValueType.INT:
                            return new cmp_result
                            {
                                gt = A.Double > B.Int,
                                lt = A.Double < B.Int,
                                eq = A.Double == B.Int,
                            };
                        case ValueType.DOUBLE:
                            return new cmp_result
                            {
                                gt = A.Double > B.Double,
                                lt = A.Double < B.Double,
                                eq = A.Double == B.Double,
                            };
                        default: throw new InvalidOperationException();
                    }
                case ValueType.STRING:
                    if (B.ValueType != ValueType.STRING) throw new InvalidOperationException();
                    int c = string.Compare(A.String, B.String);
                    return new cmp_result
                    {
                        gt = c > 0,
                        lt = c < 0,
                        eq = c == 0,
                    };

                case ValueType.TYPE:
                    if (B.ValueType != ValueType.TYPE) throw new InvalidOperationException();
                    return new cmp_result
                    {
                        gt = false,
                        lt = false,
                        eq = A.Type == B.Type,
                    };

                default: throw new InvalidOperationException();
            }
        }

        public static void Register(VirtualMachine Machine)
        {
            Machine.RegisterOperation("GT", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.COMPARABLE, ArgumentType.COMPARABLE }, (VM, Args) =>
            {
                var c = cmp(Args[1], Args[2]);

                Args[0].Register.Contents = c.gt;
            }));

            Machine.RegisterOperation("GTE", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.COMPARABLE, ArgumentType.COMPARABLE }, (VM, Args) =>
            {
                var c = cmp(Args[1], Args[2]);

                Args[0].Register.Contents = c.gt || c.eq;
            }));

            Machine.RegisterOperation("LT", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.COMPARABLE, ArgumentType.COMPARABLE }, (VM, Args) =>
            {
                var c = cmp(Args[1], Args[2]);

                Args[0].Register.Contents = c.lt;
            }));

            Machine.RegisterOperation("LTE", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.COMPARABLE, ArgumentType.COMPARABLE }, (VM, Args) =>
            {
                var c = cmp(Args[1], Args[2]);

                Args[0].Register.Contents = c.lt || c.eq;
            }));

            Machine.RegisterOperation("EQ", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.EQUATABLE, ArgumentType.EQUATABLE }, (VM, Args) =>
            {
                var c = cmp(Args[1], Args[2]);

                Args[0].Register.Contents = c.eq;
            }));

            Machine.RegisterOperation("NEQ", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.EQUATABLE, ArgumentType.EQUATABLE }, (VM, Args) =>
            {
                var c = cmp(Args[1], Args[2]);

                Args[0].Register.Contents = !c.eq;
            }));

            Machine.RegisterOperation("JGT", new Operation(new ArgumentType[] { ArgumentType.COMPARABLE, ArgumentType.COMPARABLE, ArgumentType.INSTRUCTION }, (VM, Args) =>
            {
                var c = cmp(Args[0], Args[1]);

                if (c.gt) VM.PC = Args[2].Instruction;
            }));

            Machine.RegisterOperation("JGTE", new Operation(new ArgumentType[] { ArgumentType.COMPARABLE, ArgumentType.COMPARABLE, ArgumentType.INSTRUCTION }, (VM, Args) =>
            {
                var c = cmp(Args[0], Args[1]);

                if (c.gt || c.eq) VM.PC = Args[2].Instruction;
            }));

            Machine.RegisterOperation("JLT", new Operation(new ArgumentType[] { ArgumentType.COMPARABLE, ArgumentType.COMPARABLE, ArgumentType.INSTRUCTION }, (VM, Args) =>
            {
                var c = cmp(Args[0], Args[1]);

                if (c.lt) VM.PC = Args[2].Instruction;
            }));

            Machine.RegisterOperation("JLTE", new Operation(new ArgumentType[] { ArgumentType.COMPARABLE, ArgumentType.COMPARABLE, ArgumentType.INSTRUCTION }, (VM, Args) =>
            {
                var c = cmp(Args[0], Args[1]);

                if (c.lt || c.eq) VM.PC = Args[2].Instruction;
            }));

            Machine.RegisterOperation("JE", new Operation(new ArgumentType[] { ArgumentType.COMPARABLE, ArgumentType.COMPARABLE, ArgumentType.INSTRUCTION }, (VM, Args) =>
            {
                var c = cmp(Args[0], Args[1]);

                if (c.eq) VM.PC = Args[2].Instruction;
            }));

            Machine.RegisterOperation("JNE", new Operation(new ArgumentType[] { ArgumentType.COMPARABLE, ArgumentType.COMPARABLE, ArgumentType.INSTRUCTION }, (VM, Args) =>
            {
                var c = cmp(Args[0], Args[1]);

                if (!c.eq) VM.PC = Args[2].Instruction;
            }));
        }
    }
}
