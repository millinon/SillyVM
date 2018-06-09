using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    public class Register
    {
        public readonly bool IsConstant;

        public readonly VirtualMachine VM;

        private Value _contents;
        public Value Contents
        {
            get {
                return _contents;
            }

            set {
                if(IsConstant) throw new InvalidOperationException();
                _contents = value;
            }
        }

        public readonly string Name;

        public Register(string Name, VirtualMachine VM)
        {
            this.Name = Name;
            this.VM = VM;
            this.IsConstant = false;
        }

        public Register(string Name, Value Constant, VirtualMachine VM)
        {
            this.Name = Name;
            this._contents = Constant;
            this.VM = VM;
            this.IsConstant = true;
        }
    }
}
