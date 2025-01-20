@echo off

setlocal enabledelayedexpansion

for %%f in (*.cs) do (
    echo Compiling %%f
    set "FILENAME=%%~nf"
    csc.exe /target:library /out:../bin/Debug/formatters/!FILENAME!.dll %%f
    csc.exe /target:library /out:../bin/Release/formatters/!FILENAME!.dll %%f
)