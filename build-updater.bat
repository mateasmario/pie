csc.exe /reference:System.IO.Compression.dll /reference:System.IO.Compression.FileSystem.dll PieUpdater.cs
copy "PieUpdater.exe" "bin/Release/PieUpdater.exe"
copy "PieUpdater.exe" "bin/Debug/PieUpdater.exe"
del "PieUpdater.exe"