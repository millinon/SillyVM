.PHONY: clean all

TARGETS=Counter HelloWorld Subroutine ConsoleColors Conversion Vector

all: $(foreach target,$(TARGETS),$(target).exe)
	cp ../SillyVM/SillyVM.dll .

%.exe: %/Program.cs ../SillyVM/SillyVM.dll
	mcs -debug -out:$@ $< -r:../SillyVM/SillyVM.dll

clean:
	rm -f *.exe *.dll *.mdb