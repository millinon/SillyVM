.PHONY: clean

OUTPUT:=SillyVM.dll

OPCODE_MODULES:=Comparison Conversion Math Registers Stack Types Util
SillyVM.dll: Assembler.cs Function.cs Instruction.cs Operation.cs Register.cs Value.cs VirtualMachine.cs $(foreach mod,$(OPCODE_MODULES),OpCodes/$(mod).cs)
	mcs -t:library -out:SillyVM.dll Assembler.cs Function.cs Instruction.cs Operation.cs Register.cs Value.cs VirtualMachine.cs $(foreach mod,$(OPCODE_MODULES),OpCodes/$(mod).cs)

clean:
	rm -f $(OUTPUT)
