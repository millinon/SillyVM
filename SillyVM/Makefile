OUTPUT:=SillyVM.dll

SillyVM.dll: Assembler.cs Function.cs Instruction.cs Register.cs Value.cs VirtualMachine.cs
	mcs -t:library -out:SillyVM.dll Assembler.cs Function.cs Instruction.cs Register.cs Value.cs VirtualMachine.cs
