using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    namespace OpCodes
    {
        public class Math
        {
            private enum op
            {
                ADD, SUB, MUL, DIV, MOD
            }

            private static Value Op(Value A, Value B, op OP)
            {
                switch(A.ValueType)
                {
                    case ValueType.INT:
                        switch(B.ValueType)
                        {
                            case ValueType.INT:
                                switch(OP)
                                {
                                    case op.ADD: return A.Int + B.Int;
                                    case op.SUB: return A.Int - B.Int;
                                    case op.MUL: return A.Int * B.Int;
                                    case op.DIV: return A.Int / B.Int;
                                    case op.MOD: return A.Int % B.Int;
                                    default: throw new InvalidOperationException();
                                }
                            case ValueType.DOUBLE:
                                switch(OP)
                                {
                                    case op.ADD: return A.Int + B.Double;
                                    case op.SUB: return A.Int - B.Double;
                                    case op.MUL: return A.Int * B.Double;
                                    case op.DIV: return A.Int / B.Double;
                                    case op.MOD: return A.Int % B.Double;
                                    default: throw new InvalidOperationException();
                                }
                            default: throw new InvalidOperationException();
                        }

                    case ValueType.DOUBLE:
                        switch(B.ValueType)
                        {
                            case ValueType.INT:
                                switch(OP)
                                {
                                    case op.ADD: return A.Double + B.Int;
                                    case op.SUB: return A.Double - B.Int;
                                    case op.MUL: return A.Double * B.Int;
                                    case op.DIV: return A.Double / B.Int;
                                    case op.MOD: return A.Double % B.Int;
                                    default: throw new InvalidOperationException();
                                }
                            case ValueType.DOUBLE:
                                switch(OP)
                                {
                                    case op.ADD: return A.Double + B.Double;
                                    case op.SUB: return A.Double - B.Double;
                                    case op.MUL: return A.Double * B.Double;
                                    case op.DIV: return A.Double / B.Double;
                                    case op.MOD: return A.Double % B.Double;
                                    default: throw new InvalidOperationException();
                                }
                            default: throw new InvalidOperationException();
                        }
                    default: throw new InvalidOperationException();
                }
            }




            public static void Register(VirtualMachine Machine)
            {
                Machine.RegisterFunction("INC", new Function(new ArgumentType[] { ArgumentType.REGISTER }, (VirtualMachine VM, Value[] Args) =>
                        {
                        var reg = Args[0].Register;

                        switch(reg.Contents.ValueType)
                        {
                        case ValueType.INT:
                        reg.Contents = reg.Contents.Int + 1;
                        break;

                        case ValueType.DOUBLE:
                        reg.Contents = reg.Contents.Double + 1.0;
                        break;

                        default:
                        throw new InvalidOperationException();
                        }
                        }));

                Machine.RegisterFunction("DEC", new Function(new ArgumentType[] { ArgumentType.REGISTER }, (VirtualMachine VM, Value[] Args) =>
                            {
                        var reg = Args[0].Register;

                        switch(reg.Contents.ValueType)
                        {
                        case ValueType.INT:
                        reg.Contents = reg.Contents.Int - 1;
                        break;

                        case ValueType.DOUBLE:
                        reg.Contents = reg.Contents.Double - 1.0;
                        break;

                        default:
                        throw new InvalidOperationException();
                        }
                            }));

                Machine.RegisterFunction("ADD", new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.NUMERIC, ArgumentType.NUMERIC }, (VirtualMachine VM, Value[] Args) =>
                            {
                            Args[0].Register.Contents = Op(Args[1], Args[2], op.ADD);
                            }));

                Machine.RegisterFunction("SUB", new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.NUMERIC, ArgumentType.NUMERIC }, (VirtualMachine VM, Value[] Args) =>
                            {
                            Args[0].Register.Contents = Op(Args[1], Args[2], op.SUB);
                            }));

                Machine.RegisterFunction("MUL", new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.NUMERIC, ArgumentType.NUMERIC }, (VirtualMachine VM, Value[] Args) =>
                            {
                            Args[0].Register.Contents = Op(Args[1], Args[2], op.MUL);
                            }));

                Machine.RegisterFunction("DIV", new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.NUMERIC, ArgumentType.NUMERIC }, (VirtualMachine VM, Value[] Args) =>
                            {
                            Args[0].Register.Contents = Op(Args[1], Args[2], op.DIV);
                            }));


            }
        }
    }
}
