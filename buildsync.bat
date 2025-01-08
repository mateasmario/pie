csc.exe /reference:System.IO.Compression.dll /reference:System.IO.Compression.FileSystem.dll -out:piesync.exe PieSync.cs
copy "piesync.exe" "bin/Release/piesync.exe"
copy "piesync.exe" "bin/Debug/piesync.exe"
del "piesync.exe"