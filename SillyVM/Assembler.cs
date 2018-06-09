using System;
using System.Collections.Generic;

namespace SillyVM
{
    public class Assembler
    {
        public static List<Instruction> Link(List<Instruction> Routine)
        {
            for(int i = 0; i < Routine.Count-1; i++)
            {
                var inst = Routine[i];

                if(inst.Next == null) inst.Next = Routine[i+1];
            }

            return Routine;
        }
    }
}
