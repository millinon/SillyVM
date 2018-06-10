using System;
using System.Linq;
using System.Collections.Generic;

namespace SillyVM
{
    namespace OpCodes
    {
        public class Conversion
        {
            private static Value Convert(Value From, ValueType To)
            {
                if(From.ValueType == To) return From;

                switch(From.ValueType)
                {
                    case ValueType.BOOL: {
                                             var val = From.Bool;
                                             switch(To)
                                             {
                                                 case ValueType.INT:
                                                     return val ? 1 : 0;
                                                 case ValueType.DOUBLE:
                                                     return val ? 1.0 : 0.0;
                                                 case ValueType.STRING:
                                                     return val.ToString().ToLower();
                                                 default: throw new InvalidOperationException();
                                             }
                                         }
                    case ValueType.CHAR: {
                                             var val = From.Char;
                                             switch(To)
                                             {
                                                 case ValueType.INT:
                                                     return (int) val;
                                                 case ValueType.DOUBLE:
                                                     return (double) val;
                                                 case ValueType.STRING:
                                                     return val.ToString();
                                                 default: throw new InvalidOperationException();
                                             }
                                         }
                    case ValueType.INT: {
                                            var val = From.Int;
                                            switch(To)
                                            {
                                                case ValueType.CHAR:
                                                    return (char) val;
                                                case ValueType.DOUBLE:
                                                    return (double) val;
                                                case ValueType.STRING:
                                                    return val.ToString();
                                                default: throw new InvalidOperationException();
                                            }
                                        }
                    case ValueType.DOUBLE: {
                                               var val = From.Double;
                                               switch(To)
                                               {
                                                   case ValueType.INT:
                                                       return (int) val;
                                                   case ValueType.STRING:
                                                       return val.ToString();
                                                   default: throw new InvalidOperationException();
                                               }
                                           }
                    case ValueType.STRING: {
                                               var val = From.String;
                                               switch(To)
                                               {
                                                   case ValueType.INT:
                                                       int intval;
                                                       if(! int.TryParse(val, out intval)) throw new InvalidOperationException();
                                                       return intval;
                                                   case ValueType.DOUBLE:
                                                       double doubleval;
                                                       if(! double.TryParse(val, out doubleval)) throw new InvalidOperationException();
                                                       return doubleval;
                                                   default: throw new InvalidOperationException();
                                               }
                                           }
                    case ValueType.VECTOR: {
                                               var val = From.Vector;
                                               switch(To)
                                               {
                                                   case ValueType.STRING:
                                                       var str = "[";
                                                       for(int i = 0; i < val.Count; i++)
                                                       {
                                                           var strrep = Convert(val[i], ValueType.STRING).String;
                                                           if(val[i].ValueType == ValueType.STRING || val[i].ValueType == ValueType.CHAR)
                                                           {
                                                               strrep = '"' + strrep + '"';
                                                           }
                                                           str += strrep;
                                                           if(i < val.Count-1) str += ',';
                                                       }
                                                       str += ']';

                                                       return str;
                                                   default: throw new InvalidOperationException();
                                               }
                                           }
                    case ValueType.DICTIONARY: {
                                                   var val = From.Dictionary;
                                                   switch(To)
                                                   {
                                                       case ValueType.STRING:
                                                           var keys = val.Keys;

                                                           var str = "{";
                                                           int i = 0;
                                                           foreach(var key in keys)
                                                           {
                                                               var strrep = Convert(val[key], ValueType.STRING).String;
                                                               str += '"' + key + "\":";
                                                               if(val[key].ValueType == ValueType.STRING || val[key].ValueType == ValueType.CHAR){
                                                                   strrep = '"' + strrep + '"';
                                                               }
                                                               str += strrep;
                                                               if(i++ < keys.Count-1) str += ",";
                                                           }
                                                           str += "}";

                                                           return str;
                                                       default: throw new InvalidOperationException();
                                                   }
                                               }
                    default:
                                               throw new InvalidOperationException();

                }
            }

            public static void Register(VirtualMachine Machine)
            {
                Machine.RegisterOperation("CONV", new Operation(new ArgumentType[] { ArgumentType.REGISTER, ArgumentType.CONVERTIBLE, ArgumentType.TYPE }, (VM, Args) => {
                            Args[0].Register.Contents = Convert(Args[1], Args[2].Type);
                            }));
            }
        }
    }
}
