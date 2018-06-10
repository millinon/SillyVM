using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    namespace OpCodes
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
                switch(A.ValueType)
                {
                    case ValueType.VOID: throw new InvalidOperationException();
                    case ValueType.BOOL:
                                         if(B.ValueType != ValueType.BOOL) throw new InvalidOperationException();
                                         else return new cmp_result {
                                             gt = A.Bool && !B.Bool,
                                                lt = B.Bool && !A.Bool,
                                                eq = A.Bool == B.Bool,
                                         };
                    case ValueType.INT:
                                         switch(B.ValueType){
                                             case ValueType.INT:
                                                 return new cmp_result {
                                                     gt = A.Int > B.Int,
                                                        lt = A.Int < B.Int,
                                                        eq = A.Int == B.Int,
                                                 };
                                             case ValueType.DOUBLE:
                                                 return new cmp_result {
                                                     gt = A.Int > B.Double,
                                                        lt = A.Int < B.Double,
                                                        eq = A.Int == B.Double,
                                                 };
                                             default: throw new InvalidOperationException();
                                         }
                    case ValueType.DOUBLE:
                                         switch(B.ValueType){
                                             case ValueType.INT:
                                                 return new cmp_result {
                                                     gt = A.Double > B.Int,
                                                        lt = A.Double < B.Int,
                                                        eq = A.Double == B.Int,
                                                 };
                                             case ValueType.DOUBLE:
                                                 return new cmp_result {
                                                     gt = A.Double > B.Double,
                                                        lt = A.Double < B.Double,
                                                        eq = A.Double == B.Double,
                                                 };
                                             default: throw new InvalidOperationException();
                                         }
                    case ValueType.STRING:
                                         if(B.ValueType != ValueType.STRING) throw new InvalidOperationException();
                                         int c = string.Compare(A.String, B.String);
                                         return new cmp_result {
                                             gt = c > 0,
                                                lt = c < 0,
                                                eq = c == 0,
                                         };
                    default: throw new InvalidOperationException();
                }
            }

            public static void Register(VirtualMachine Machine)
            {
                Machine.RegisterOperation("GT",  new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.COMPARABLE, ArgumentType.COMPARABLE}, (VirtualMachine VM, Value[] Args) => {
                            var c = cmp(Args[1], Args[2]);

                            Args[0].Register.Contents = c.gt;
                            }));

                Machine.RegisterOperation("GTE",  new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.COMPARABLE, ArgumentType.COMPARABLE}, (VirtualMachine VM, Value[] Args) => {
                            var c = cmp(Args[1], Args[2]);

                            Args[0].Register.Contents = c.gt || c.eq;
                            }));

                Machine.RegisterOperation("LT",  new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.COMPARABLE, ArgumentType.COMPARABLE}, (VirtualMachine VM, Value[] Args) => {
                            var c = cmp(Args[1], Args[2]);

                            Args[0].Register.Contents = c.lt;
                            }));

                Machine.RegisterOperation("LTE",  new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.COMPARABLE, ArgumentType.COMPARABLE}, (VirtualMachine VM, Value[] Args) => {
                            var c = cmp(Args[1], Args[2]);

                            Args[0].Register.Contents = c.lt || c.eq;
                            }));

                Machine.RegisterOperation("EQ", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.EQUATABLE, ArgumentType.EQUATABLE}, (VirtualMachine VM, Value[] Args) => {
                            var c = cmp(Args[1], Args[2]);

                            Args[0].Register.Contents = c.eq;
                            }));

                Machine.RegisterOperation("NEQ", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.EQUATABLE, ArgumentType.EQUATABLE}, (VirtualMachine VM, Value[] Args) => {
                            var c = cmp(Args[1], Args[2]);

                            Args[0].Register.Contents = ! c.eq;
                            }));
            }
        }
    }
}
