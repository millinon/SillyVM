OUTPUT:=SillyVM.dll

OPCODE_MODULES:=Comparison Math Registers Stack Util
SillyVM.dll: Assembler.cs Function.cs Instruction.cs Register.cs Value.cs VirtualMachine.cs $(foreach mod,$(OPCODE_MODULES),OpCodes/$(mod).cs)
	mcs -t:library -out:SillyVM.dll Assembler.cs Function.cs Instruction.cs Register.cs Value.cs VirtualMachine.cs $(foreach mod,$(OPCODE_MODULES),OpCodes/$(mod).cs)
