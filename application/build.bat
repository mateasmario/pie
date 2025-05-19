:: SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com>
:: SPDX-License-Identifier: GPL-3.0-or-later

@echo off

:: Build PieSync
csc.exe /reference:System.IO.Compression.dll /reference:System.IO.Compression.FileSystem.dll -out:piesync.exe PieSync.cs
copy "piesync.exe" "bin/Release/net8.0-windows7.0/piesync.exe"
copy "piesync.exe" "bin/Debug/net8.0-windows7.0/piesync.exe"
del "piesync.exe"

:: 

:: Build formatters
setlocal enabledelayedexpansion

cd Formatters

for %%f in (*.cs) do (
    echo Compiling %%f
    set "FILENAME=%%~nf"
    csc.exe /target:library /out:../bin/Debug/net8.0-windows7.0/formatters/!FILENAME!.dll %%f
    csc.exe /target:library /out:../bin/Release/net8.0-windows7.0/formatters/!FILENAME!.dll %%f
)