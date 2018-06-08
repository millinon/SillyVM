.PHONY: clean

all: Counter.exe HelloWorld.exe
	cp ../SillyVM/SillyVM.dll .

Counter.exe: Counter.cs ../SillyVM/SillyVM.dll
	mcs -out:Counter.exe Counter.cs -r:../SillyVM/SillyVM.dll

HelloWorld.exe: HelloWorld.cs ../SillyVM/SillyVM.dll
	mcs -out:HelloWorld.exe HelloWorld.cs -r:../SillyVM/SillyVM.dll

clean:
	rm -f *.exe
