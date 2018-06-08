using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    public class VirtualMachine
    {
        private readonly List<Instruction> Program;

        internal readonly Dictionary<string, Register> _registers;
        public IReadOnlyDictionary<string, Register> Registers
        {
            get {
             return (IReadOnlyDictionary<string, Register>) _registers;   
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

        private Dictionary<string, Function> Functions;

        public VirtualMachine()
        {
            Stack = new Stack<Value>();
            Functions = new Dictionary<string, Function>();
            Program = new List<Instruction>();
            _registers = new Dictionary<string, Register>();

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

            _registers[Name] = new Register(Name);
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

            _registers.Clear();

            PC = null;

            IsHalted = false;
        }

        internal void Halt()
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

        internal void RegisterFunction(string OpCode, Function Function)
        {
            if(Functions.Keys.Contains(OpCode)) throw new InvalidOperationException();

            Functions[OpCode] = Function;
        }

        private void RegisterNatives()
        {
            OpCodes.Comparison.Register(this);
            OpCodes.Math.Register(this);
            OpCodes.Registers.Register(this);
            OpCodes.Stack.Register(this);
            OpCodes.Util.Register(this);
        }
    }
}
