using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    public class Instruction
    {
        public readonly string OpCode;
        public readonly Value[] Arguments;

        public readonly string Label;
        public Instruction Next;

        public Instruction(string OpCode, Value[] Arguments, string Label = null)
        {
            this.OpCode = OpCode;
            this.Arguments = Arguments;
            this.Label = Label;
            this.Next = null;
        }

        public Instruction(string OpCode, params Value[] Arguments)
        {
            this.OpCode = OpCode;
            this.Arguments = Arguments;
            this.Label = null;
            this.Next = null;
        }
    }
}
