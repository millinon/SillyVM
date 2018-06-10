using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    public enum ValueType
    {
        VOID,
        BOOL,
        CHAR,
        INT,
        DOUBLE,
        STRING,
        VECTOR,
        DICTIONARY,
        REFERENCE,
        TYPE,
        REGISTER,
        FUNCTION,
        INSTRUCTION,
    }

    public class Value
    {
        public readonly ValueType ValueType;

        private readonly bool _boolval;
        public bool Bool
        {
            get
            {
                if (ValueType == ValueType.BOOL) return _boolval;
                else throw new InvalidOperationException();
            }
        }

        private readonly int _intval;
        public int Int
        {
            get
            {
                if (ValueType == ValueType.INT) return _intval;
                else throw new InvalidOperationException();
            }
        }

        private readonly char _charval;
        public char Char
        {
            get
            {
                if (ValueType == ValueType.CHAR) return _charval;
                else throw new InvalidOperationException();
            }
        }

        private readonly double _doubleval;
        public double Double
        {
            get
            {
                if (ValueType == ValueType.DOUBLE) return _doubleval;
                else throw new InvalidOperationException();
            }
        }

        private readonly string _stringval;
        public string String
        {
            get
            {
                if (ValueType == ValueType.STRING) return _stringval;
                else throw new InvalidOperationException();
            }
        }

        private readonly List<Value> _vectorval;
        public List<Value> Vector
        {
            get
            {
                if (ValueType == ValueType.VECTOR) return _vectorval;
                else throw new InvalidOperationException();
            }
        }

        private readonly Dictionary<string, Value> _dictionaryval;
        public Dictionary<string, Value> Dictionary
        {
            get
            {
                if (ValueType == ValueType.DICTIONARY) return _dictionaryval;
                else throw new InvalidOperationException();
            }
        }

        private readonly Value _referenceval;
        public Value Reference
        {
            get
            {
                if (ValueType == ValueType.REFERENCE) return _referenceval;
                else throw new InvalidOperationException();
            }
        }

        private readonly ValueType _typeval;
        public ValueType Type
        {
            get
            {
                if (ValueType == ValueType.TYPE) return _typeval;
                else throw new InvalidOperationException();
            }
        }

        private readonly Register _registerval;
        public Register Register
        {
            get
            {
                if (ValueType == ValueType.REGISTER) return _registerval;
                else throw new InvalidOperationException();
            }
        }

        private readonly Function _functionval;
        public Function Function
        {
            get
            {
                if (ValueType == ValueType.FUNCTION) return _functionval;
                else throw new InvalidOperationException();
            }
        }

        private readonly Instruction _instructionval;
        public Instruction Instruction
        {
            get
            {
                if (ValueType == ValueType.INSTRUCTION) return _instructionval;
                else throw new InvalidOperationException();
            }
        }

        public Value()
        {
            ValueType = ValueType.VOID;
        }

        public Value(bool BoolValue)
        {
            ValueType = ValueType.BOOL;
            _boolval = BoolValue;
        }

        public static implicit operator Value(bool BoolValue)
        {
            return new Value(BoolValue);
        }

        public Value(char CharValue)
        {
            ValueType = ValueType.CHAR;
            _charval = CharValue;
        }

        public static implicit operator Value(char CharValue)
        {
            return new Value(CharValue);
        }

        public Value(int IntValue)
        {
            ValueType = ValueType.INT;
            _intval = IntValue;
        }

        public static implicit operator Value(int IntValue)
        {
            return new Value(IntValue);
        }

        public Value(double DoubleValue)
        {
            ValueType = ValueType.DOUBLE;
            _doubleval = DoubleValue;
        }

        public static implicit operator Value(double DoubleValue)
        {
            return new Value(DoubleValue);
        }

        public Value(string StringValue)
        {
            ValueType = ValueType.STRING;
            _stringval = StringValue;
        }

        public static implicit operator Value(string StringValue)
        {
            return new Value(StringValue);
        }

        public Value(List<Value> VectorValue)
        {
            ValueType = ValueType.VECTOR;
            _vectorval = VectorValue;
        }

        public static implicit operator Value(List<Value> VectorValue)
        {
            return new Value(VectorValue);
        }

        public Value(Dictionary<string, Value> DictionaryValue)
        {
            ValueType = ValueType.DICTIONARY;
            _dictionaryval = DictionaryValue;
        }

        public static implicit operator Value(Dictionary<string, Value> DictionaryValue)
        {
            return new Value(DictionaryValue);
        }

        /*public Value(Value ReferenceValue)
          {
          ValueType = ValueType.REFERENCE;
          _referenceval = ReferenceValue;
          }

          public static Value Reference(Value ReferenceValue)
          {
          return new Value(ReferenceValue);
          }*/

        public Value(ValueType TypeValue)
        {
            ValueType = ValueType.TYPE;
            _typeval = TypeValue;
        }

        public static implicit operator Value(ValueType TypeValue)
        {
            return new Value(TypeValue);
        }

        public Value(Register RegisterValue)
        {
            ValueType = ValueType.REGISTER;
            _registerval = RegisterValue;
        }

        public static implicit operator Value(Register RegisterValue)
        {
            return new Value(RegisterValue);
        }

        public Value(Instruction InstructionValue)
        {
            ValueType = ValueType.INSTRUCTION;
            _instructionval = InstructionValue;
        }

        public static implicit operator Value(Instruction InstructionValue)
        {
            return new Value(InstructionValue);
        }

        public Value(Function FunctionValue)
        {
            ValueType = ValueType.FUNCTION;
            _functionval = FunctionValue;
        }

        public static implicit operator Value(Function FunctionValue)
        {
            return new Value(FunctionValue);
        }

        public override int GetHashCode()
        {
            switch (ValueType)
            {
                case ValueType.VOID:
                    return 0;

                case ValueType.BOOL:
                    return _boolval.GetHashCode();

                case ValueType.CHAR:
                    return _charval.GetHashCode();

                case ValueType.INT:
                    return _intval.GetHashCode();

                case ValueType.DOUBLE:
                    return _doubleval.GetHashCode();

                case ValueType.STRING:
                    return _stringval.GetHashCode();

                case ValueType.VECTOR:
                    return _vectorval.GetHashCode();

                case ValueType.DICTIONARY:
                    return _dictionaryval.GetHashCode();

                case ValueType.REFERENCE:
                    return _referenceval.GetHashCode();

                case ValueType.TYPE:
                    return _typeval.GetHashCode();

                case ValueType.FUNCTION:
                    return _functionval.GetHashCode();

                case ValueType.INSTRUCTION:
                    return _instructionval.GetHashCode();

                case ValueType.REGISTER:
                    return _registerval.GetHashCode();

                default:
                    throw new InvalidOperationException();
            }
        }

        public override string ToString()
        {
            var typeval = $"[{ValueType}]";

            string str;
            int i;

            switch (ValueType)
            {
                case ValueType.VOID:
                    return $"{typeval}";

                case ValueType.BOOL:
                    return $"{typeval} {_boolval}";
        
                case ValueType.CHAR:
                    return $"{typeval} {_charval}";

                case ValueType.INT:
                    return $"{typeval} {_intval}";

                case ValueType.DOUBLE:
                    return $"{typeval} {_doubleval}";

                case ValueType.STRING:
                    return $"{typeval} \"{_stringval}\"";

                case ValueType.VECTOR:
                    str = $"{typeval} [";
                    for(i = 0; i < _vectorval.Count; i++)
                    {
                        str += _vectorval[i].ToString();
                        if (i < _vectorval.Count - 1) str += ",";
                    }
                    str += "]";

                    return str;

                case ValueType.DICTIONARY:
                    str = typeval + " {";
                    var keys = _dictionaryval.Keys;

                    i = 0;
                    foreach(var key in keys)
                    {
                        str += '"' + key + '"' + ":" + _dictionaryval[key].ToString();
                        if (i++ < keys.Count) str += ",";
                    }
                    str += "}";

                    return str;

                case ValueType.REFERENCE:
                    return $"{typeval} {_referenceval}";

                case ValueType.TYPE:
                    return $"{typeval} {_typeval}";

                case ValueType.FUNCTION:
                    return $"{typeval} {_functionval}";

                case ValueType.INSTRUCTION:
                    return $"{typeval} {_instructionval}";

                case ValueType.REGISTER:
                    return $"{typeval} {_registerval}";

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
