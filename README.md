# ![pie](https://i.imgur.com/mvR0VQv.png) Pie: A General Purpose Code Editor Focused on Simplicity

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0) [![REUSE status](https://api.reuse.software/badge/github.com/mateasmario/pie)](https://api.reuse.software/info/github.com/mateasmario/pie)
 [![GitHub Release](https://img.shields.io/github/v/release/mateasmario/pie)](https://github.com/mateasmario/pie/releases/latest) [![GitHub Issues or Pull Requests by label](https://img.shields.io/github/issues/mateasmario/pie)](https://github.com/mateasmario/pie/issues)

Pie is a tool intended to replace existing code editing software in daily tasks such as scripting and text formatting. It has been designed to aid both beginner and experienced developers, providing a simple user interface that doesn't interfere with the main work area of the individual. 

The tool comes packed with the most commonly accessed development tools, while supporting various types of plugins, for those who want to extend its functionality for their specific needs.

## Motivation

The motivation was pretty personal in the beginning. Pie was intended to be a full replacement for editors I was then using on a common basis. Available text editors such as Notepad and Notepad++ didn't offer all the tools I needed for my tasks, while integrated development environments such as [Visual Studio](https://visualstudio.microsoft.com/) (including its [Code variant](https://code.visualstudio.com/)) and the [JetBrains bundle](https://www.jetbrains.com/ides/) were just too much for that. Plus, the user interface of state-of-the-art tools became more and more complex, as newer features got integrated, leading to reduced productivity, at least from my point of view. I also provided direct links for the editors mentioned previously, in case you've already decided not to use Pie and stick with something else.

In the meantime, I chose to present Pie as my [diploma project](https://github.com/mateasmario/diploma), and have also published [an article](https://ieeexplore.ieee.org/document/10619920/) at an IEEE conference on how it might increase the productivity of its users.

## Technologies

Pie has been built on the [Windows Forms](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/?view=netdesktop-9.0) framework (commonly known as "WinForms") relying on .NET 4.7.2. The reason for not using newer user interface frameworks (such as WPF, WinUI or frameworks based on other languages, such as Electron) is the mature community support that WinForms still has to offer to the wide audience. During the years, developers came with open-source implementations for various features that can be easily integrated within a Windows Forms solution, no longer requiring others to implement them from scratch. As the framework's initial release was in 2002, we can conclude that there are more dependencies available for WinForms than for any other framework on the market.

The text editor relies on [ScintillaNET](https://github.com/jacobslusser/ScintillaNET), a C# wrapper of [Scintilla](https://sourceforge.net/projects/scintilla), a popular text editing engine used by products such as Notepad++. Autocomplete features have been provided by [AutoCompleteMenu-ScintillaNET](https://github.com/Ahmad45123/AutoCompleteMenu-ScintillaNET).

The overall design of the application is done through the [Krypton](https://github.com/ComponentFactory/Krypton) library, which offers a set controls that are more customizable than the ones offered by the framework itself. Another custom control offering more than its vanilla counterpart is [ObjectListView](https://objectlistview.sourceforge.net/cs/index.html).

Integrated terminals can also be accessed in Pie, and these are nothing more than instances of [ConEmu](https://github.com/Maximus5/ConEmu), adapted to C# through the [ConEmu-Inside](https://github.com/Maximus5/conemu-inside) library.

Integrated web browser instances and the Markdown to HTML converter have been provided by [CefSharp](https://github.com/cefsharp/CefSharp) and [Markdig](https://github.com/xoofx/markdig).

The Git interface, that allows a simpler view over the Git repository (compared to the classic command line tool), has been implemented using the [Libgit2sharp](https://github.com/libgit2/libgit2) pacakge, which is nothing more than a wrapper over [Libgit2](https://github.com/libgit2/libgit2).

Database connection management for MySQL and PostgreSQL is done through [MySQL Connector/NET](https://github.com/mysql/mysql-connector-net) and [Npgsql](https://github.com/npgsql/npgsql). Microsoft SQL (MSSQL) connections are managed by .NET's vanilla methods and do not require an external package to be installed.

JSON configuration is handled by the [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) library, which is so popular that it doesn't require an introduction on this page.

## Extension Points

Pie features certain extension points, which refer to configuration files and user-defined methods that extend its available functionality. Extension points start from simple theme definitions stored in JSON files that can be configured through an integrated user interface, and can get to defining methods that manipulate text and are stored as Dynamic-Link Libraries (DLLs) inside Pie's configuration folder.

Extension points are a great way to offer experienced developers a greater amount of customizability in Pie. These include: defining build commands, associating file extensions with certain lexers, defining keywords for custom languages and creating custom analysis tools.

## Installing and Updating Pie

An installer for Pie is yet to be implemented, but at the moment, one can navigate to the [Releases](https://github.com/mateasmario/pie/releases) section and download the latest release in an archive whose contents should be extracted in a preferred directory. Running the executable should then work smoothly. 

Pie also features an automatic updater, called `PieSync.exe`, which is a console app that extracts the contents of the latest release from GitHub inside the launch directory of the executable. This is why it is **not** recommended to not change the directory structure of the binaries. PieSync can also be run manually, but the editor sends a notification (on launch) whenever a newer release is published, and the update process can be started directly from the application's user interface. It is, however, recommended to explicitly run PieSync when there are errors coming from manually configured JSON files, and Pie cannot resolve them by itself.

## Contribute: Making Pie Better

Interested individuals may also contribute to the continuous improvement of Pie, either by sending feedback (via the [Feedback Form](https://forms.gle/L3mjuyTrYwBSVdYJ9)), by opening issues related to certain bugs or new improvement ideas, or by creating pull requests related to a certain issue. Anything is well appreciated!
