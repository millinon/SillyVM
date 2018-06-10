using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SillyVM
{
    public class Function
    {
        public readonly string Name;

        public readonly  ReadOnlyCollection<Instruction> Procedure;

        public Function(List<Instruction> Procedure)
        {
            this.Procedure = Procedure.AsReadOnly();
        }
    }
}
