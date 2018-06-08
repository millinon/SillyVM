using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    public class VirtualMachine
    {
        public VirtualMachine()
        {
            Stack = new Stack<Value>();
            Functions = new Dictionary<string, Function>();
            Program = new List<Instruction>();
            Registers = new Dictionary<string, Register>();

            Reset();
        }

        private void Invoke(Function Func, params Value[] Parameters)
        {
            Func.ValidateArguments(Parameters);

            Func.Invoke(this, Parameters);
        }

        private void Execute(Instruction Instruction)
        {
            PC = null;

            Invoke(Functions[Instruction.OpCode], Instruction.Arguments);

            if (PC == null) PC = Instruction.Next;
        }

        public void AddRegister(string Name)
        {
            if(Registers.Keys.Contains(Name)) throw new InvalidOperationException();

            Registers[Name] = new Register(Name);
        }

        public void Run()
        {
            while(!IsHalted)
            {
                if (PC == null) throw new InvalidOperationException();
                Execute(PC);
            }
        }

        public void Reset()
        {
            Functions.Clear();
            RegisterNatives();

            Registers.Clear();

            PC = null;

            IsHalted = false;
        }

        private void Halt()
        {
            IsHalted = true;
        }

        public void Load(List<Instruction> Program)
        {
            this.Program.Clear();

            foreach(var inst in Program)
            {
                this.Program.Add(inst);
            }

            this.PC = this.Program[0];
        }

        private static ArgumentType vt2at(ValueType VT)
        {
            ArgumentType AT;
            if(! Enum.TryParse(VT.ToString(), out AT)) throw new ArgumentException();
            else return AT;
        }

        private readonly List<Instruction> Program;

        public readonly Dictionary<string, Register> Registers;

        private Instruction PC;

        public bool IsHalted;

        private readonly Stack<Value> Stack;

        protected Dictionary<string, Function> Functions;

        private struct cmp_result
        {
            public bool gt;
            public bool lt;
            public bool eq;
        }

        private cmp_result cmp(Value A, Value B)
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

        private void RegisterNatives()
        {
            Functions["NOOP"] = new Function(new ArgumentType[] { }, (VirtualMachine VM, Value[] Args) => 
                    {

                    });

            Functions["PUSH"] = new Function(new ArgumentType[] { ArgumentType.ANY }, (VirtualMachine VM, Value[] Args) =>
                    {
                    Stack.Push(Args[0]);
                    });

            Functions["POP"] = new Function(new ArgumentType[] { ArgumentType.REGISTER }, (VirtualMachine VM, Value[] Args) =>
                    {
                    Args[0].Register.Contents = Stack.Pop();
                    });

            Functions["STACKSIZE"] = new Function(new ArgumentType[] { ArgumentType.REGISTER }, (VirtualMachine VM, Value[] Args) =>
                    {
                    Args[0].Register.Contents = new Value(Stack.Count);
                    });

            Functions["LOAD"] = new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.ANY }, (VirtualMachine VM, Value[] Args) =>
                    {
                    Args[0].Register.Contents = Args[1];
                    });

            Functions["MOVE"] = new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.REGISTER }, (VirtualMachine VM, Value[] Args) =>
                    {
                    Args[0].Register.Contents = Args[1].Register.Contents;
                    });

            Functions["JUMP"] = new Function(new ArgumentType[] { ArgumentType.INSTRUCTION }, (VirtualMachine VM, Value[] Args) =>
                    {
                    VM.PC = Args[0].Instruction;
                    });

            Functions["JUMPIF"] = new Function(new ArgumentType[] { ArgumentType.BOOL, ArgumentType.INSTRUCTION }, (VirtualMachine VM, Value[] Args) =>
                    {
                    if (Args[0].Bool) PC = Args[1].Instruction;
                    });

            Functions["PRINT"] = new Function(new ArgumentType[] { ArgumentType.ANY }, (VirtualMachine VM, Value[] Args) =>
                    {
                    Console.WriteLine(Args[0].ToString());
                    });

            Functions["ADDREG"] = new Function(new ArgumentType[] { ArgumentType.STRING }, (VirtualMachine VM, Value[] Args) => 
                    {
                    var regname = Args[0].String;
                    if(Registers.Keys.Contains(regname)) throw new InvalidOperationException();
                    Registers[regname] = new Register(regname);
                    });

            Functions["ADDCONST"] = new Function(new ArgumentType[] { ArgumentType.STRING, ArgumentType.ANY }, (VirtualMachine VM, Value[] Args) => 
                    {
                    var regname = Args[0].String;
                    if(Registers.Keys.Contains(regname)) throw new InvalidOperationException();
                    Registers[regname] = new Register(regname, Args[1]);
                    });

            Functions["INC"] = new Function(new ArgumentType[] { ArgumentType.REGISTER }, (VirtualMachine VM, Value[] Args) =>
                    {
                    Args[0].Register.Contents = Args[0].Register.Contents.Int + 1;
                    });

            Functions["DEC"] = new Function(new ArgumentType[] { ArgumentType.REGISTER }, (VirtualMachine VM, Value[] Args) =>
                    {
                    Args[0].Register.Contents = Args[0].Register.Contents.Int - 1;
                    });

            Functions["GT"] = new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.INT | ArgumentType.DOUBLE | ArgumentType.STRING, ArgumentType.INT | ArgumentType.DOUBLE | ArgumentType.STRING}, (VirtualMachine VM, Value[] Args) => {
                    var c = cmp(Args[1], Args[2]);

                    Args[0].Register.Contents = c.gt;
                    });

            Functions["GTE"] = new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.INT | ArgumentType.DOUBLE | ArgumentType.STRING, ArgumentType.INT | ArgumentType.DOUBLE | ArgumentType.STRING }, (VirtualMachine VM, Value[] Args) => {
                    var c = cmp(Args[1], Args[2]);

                    Args[0].Register.Contents = c.gt || c.eq;
                    });

            Functions["LT"] = new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.INT | ArgumentType.DOUBLE | ArgumentType.STRING, ArgumentType.INT | ArgumentType.DOUBLE | ArgumentType.STRING }, (VirtualMachine VM, Value[] Args) => {
                    var c = cmp(Args[1], Args[2]);

                    Args[0].Register.Contents = c.lt;
                    });

            Functions["LTE"] = new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.INT | ArgumentType.DOUBLE | ArgumentType.STRING, ArgumentType.INT | ArgumentType.DOUBLE | ArgumentType.STRING}, (VirtualMachine VM, Value[] Args) => {
                    var c = cmp(Args[1], Args[2]);

                    Args[0].Register.Contents = c.lt || c.eq;
                    });

            Functions["EQ"] = new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.BOOL | ArgumentType.INT | ArgumentType.DOUBLE | ArgumentType.STRING, ArgumentType.BOOL | ArgumentType.INT | ArgumentType.DOUBLE | ArgumentType.STRING}, (VirtualMachine VM, Value[] Args) => {
                    var c = cmp(Args[1], Args[2]);

                    Args[0].Register.Contents = c.eq;
                    });

            Functions["NEQ"] = new Function(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.BOOL | ArgumentType.INT | ArgumentType.DOUBLE | ArgumentType.STRING, ArgumentType.BOOL | ArgumentType.INT | ArgumentType.DOUBLE | ArgumentType.STRING}, (VirtualMachine VM, Value[] Args) => {
                    var c = cmp(Args[1], Args[2]);

                    Args[0].Register.Contents = ! c.eq;
                    });

            Functions["HALT"] = new Function(new ArgumentType[] { }, (VirtualMachine VM, Value[] Args) =>
                    {
                    VM.Halt();
                    });
        }
    }
}
