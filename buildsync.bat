:: SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com>
:: SPDX-License-Identifier: GPL-3.0-or-later

csc.exe /reference:System.IO.Compression.dll /reference:System.IO.Compression.FileSystem.dll -out:piesync.exe PieSync.cs
copy "piesync.exe" "bin/Release/piesync.exe"
copy "piesync.exe" "bin/Debug/piesync.exe"
del "piesync.exe"