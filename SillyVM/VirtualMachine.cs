using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    public class VirtualMachine
    {
        internal List<Instruction> Program;
        internal readonly Stack<Instruction> CallStack;

        internal readonly Dictionary<string, Register> _registers;
        public IReadOnlyDictionary<string, Register> Registers
        {
            get {
             return _registers;   
            }
        }
        
        private Instruction _pc;
        public Instruction PC
        {
            get {
                return _pc;
            }

            internal set {
                _pc = value;
            }
        }

        public bool IsHalted;

        internal readonly Stack<Value> Stack;

        private Dictionary<string, Operation> Operations;

        public VirtualMachine()
        {
            Stack = new Stack<Value>();
            Operations = new Dictionary<string, Operation>();
            Program = new List<Instruction>();
            _registers = new Dictionary<string, Register>();
            CallStack = new Stack<Instruction>();

            Reset();
        }

        private void Execute(Instruction Instruction)
        {
            var opcode = Instruction.OpCode.ToLower();

            if(! Operations.ContainsKey(opcode)) throw new InvalidOperationException("OpCode " + Instruction.OpCode + " not found");

            var op = Operations[opcode];
            
            PC = Instruction.Next;

            op.Invoke(this, Instruction.Arguments);

            if (!IsHalted && PC == null) {
                if(CallStack.Count > 0) PC = CallStack.Pop();
                else throw new InvalidOperationException();
            }
        }

        public Register AddRegister(string Name)
        {
            if(Registers.Keys.Contains(Name)) throw new InvalidOperationException();

            _registers[Name] = new Register(Name, this);

            return _registers[Name];
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
            Stack.Clear();

            Operations.Clear();
            RegisterNatives();

            _registers.Clear();
            CallStack.Clear();

            Program = null;

            PC = null;

            IsHalted = false;
        }

        internal void Halt()
        {
            IsHalted = true;
        }

        public void Load(List<Instruction> Program)
        {
            this.Program = new List<Instruction>(Program);

            this.PC = this.Program[0];
        }

        internal void RegisterOperation(string OpCode, Operation Op)
        {
            var opcode = OpCode.ToLower();

            if(Operations.Keys.Contains(opcode)) throw new InvalidOperationException();

            Operations[opcode] = Op;
        }

        private void RegisterNatives()
        {
            OpCodes.Comparison.Register(this);
            OpCodes.Console.Register(this);
            OpCodes.Conversion.Register(this);
            OpCodes.Logic.Register(this);
            OpCodes.Math.Register(this);
            OpCodes.Registers.Register(this);
            OpCodes.Stack.Register(this);
            OpCodes.Types.Register(this);
            OpCodes.Util.Register(this);
            OpCodes.Vector.Register(this);
        }
    }
}
