.PHONY: clean all

all: Counter.exe HelloWorld.exe Subroutine.exe
	cp ../SillyVM/SillyVM.dll .

%.exe: %.cs ../SillyVM/SillyVM.dll
	mcs -out:$@ $< -r:../SillyVM/SillyVM.dll

clean:
	rm -f *.exe SillyVM.dll
