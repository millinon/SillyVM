.PHONY: clean

OUTPUT:=SillyVM.dll
OPCODE_MODULES:=Comparison Console Conversion Logic Math Registers Stack Types Util Vector

SillyVM.dll: Assembler.cs Function.cs Instruction.cs Operation.cs Register.cs Value.cs VirtualMachine.cs $(foreach mod,$(OPCODE_MODULES),OpCodes/$(mod).cs)
	mcs -debug -t:library -out:SillyVM.dll Assembler.cs Function.cs Instruction.cs Operation.cs Register.cs Value.cs VirtualMachine.cs $(foreach mod,$(OPCODE_MODULES),OpCodes/$(mod).cs)

clean:
	rm -f $(OUTPUT)
