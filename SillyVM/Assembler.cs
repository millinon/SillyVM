using System;
using System.Collections.Generic;

namespace SillyVM
{
    public class Assembler
    {
        public static void Link(List<Instruction> Program)
        {
            for(int i = 0; i < Program.Count-1; i++)
            {
                var inst = Program[i];

                if(inst.Next == null) inst.Next = Program[i+1];
            }
        }
    }
}
