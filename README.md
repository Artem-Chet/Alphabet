# .Net version of alphabet-rs 

Original : https://github.com/ivelrrat/alphabet-rs

Simple but slower startup
```
dotnet run
```

For faster startup, publish first
```
dotnet publish -c Release -o  ./bin/publish
cd ./bin/publish
```

Then run:

Windows:
```
./Alphabet.exe
```