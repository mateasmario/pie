# ![pie](https://i.imgur.com/mvR0VQv.png) Pie: A General Purpose Code Editor Focused on Simplicity

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0) ![GitHub Issues or Pull Requests by label](https://img.shields.io/github/issues/mateasmario/pie/bug) ![GitHub Issues or Pull Requests by label](https://img.shields.io/github/issues/mateasmario/pie/enhancement) ![GitHub Issues or Pull Requests by label](https://img.shields.io/github/issues/mateasmario/pie/documentation) 

Pie is a tool intended to replace existing code editing software in daily tasks such as scripting and text formatting. It has been designed to aid both beginner and experienced developers, providing a simple user interface that doesn't interfere with the main work area of the individual. 

The tool comes packed with the most commonly accessed development tools, while supporting various types of plugins, for those who want to extend its functionality for their specific needs.

## Technologies

Pie has been built on the [Windows Forms](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/?view=netdesktop-9.0) framework (commonly known as "WinForms") relying on .NET 4.7.2. The reason for not using newer user interface frameworks (such as WPF, WinUI or frameworks based on other languages, such as Electron) is the mature community support that WinForms still has to offer to the wide audience. During the years, developers came with open-source implementations for various features that can be easily integrated within a Windows Forms solution, no longer requiring others to implement them from scratch. As the framework's initial release was in 2002, we can conclude that there are more dependencies available for WinForms than for any other framework on the market.

The text editor relies on [ScintillaNET](https://github.com/jacobslusser/ScintillaNET), a C# wrapper of [Scintilla](https://sourceforge.net/projects/scintilla), a popular text editing engine used by products such as Notepad++. Autocomplete features have been provided by [AutoCompleteMenu-ScintillaNET](https://github.com/Ahmad45123/AutoCompleteMenu-ScintillaNET).

The overall design of the application is done through the [Krypton](https://github.com/ComponentFactory/Krypton) library, which offers a set controls that are more customizable than the ones offered by the framework itself. Another custom control offering more than its vanilla counterpart is [ObjectListView](https://objectlistview.sourceforge.net/cs/index.html).

Integrated terminals can also be accessed in Pie, and these are nothing more than instances of [ConEmu](https://github.com/Maximus5/ConEmu), adapted to C# through the [ConEmu-Inside](https://github.com/Maximus5/conemu-inside) library.

Integrated web browser instances and the Markdown to HTML converter have been provided by [CefSharp](https://github.com/cefsharp/CefSharp) and [Markdig](https://github.com/xoofx/markdig).

The Git interface, that allows a simpler view over the Git repository (compared to the classic command line tool), has been implemented using the [Libgit2sharp](https://github.com/libgit2/libgit2) pacakge, which is nothing more than a wrapper over [Libgit2](https://github.com/libgit2/libgit2).

Database connection management for MySQL and PostgreSQL is done through [MySQL Connector/NET](https://github.com/mysql/mysql-connector-net) and [Npgsql](https://github.com/npgsql/npgsql). Microsoft SQL (MSSQL) connections are managed by .NET's vanilla methods and do not require an external package to be installed.

JSON configuration is handled by the [NewtonSoft.Json](https://github.com/JamesNK/Newtonsoft.Json) library, which is so popular that it doesn't require an introduction on this page.

## Features

## Extensions

## Other useful links
