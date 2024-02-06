# ![pie (2)](https://i.imgur.com/2nE7mDE.png) pie
Lightweight text editor with some third-party features, capable of replacing Notepad.

![image](https://i.imgur.com/B533oZ0.png)

### ![arrow-right](https://i.imgur.com/4Eg9yg9.png) Short Introduction
pie is a combination between a text-editor (like Notepad) and an over-simplified IDE, that allows super-fast modification of code files (or other types of files), whenever you don't feel the need to wait for a big IDE to open.

pie has integrated tabs, in order to give the user the ability to manipulate multiple files at once, without keeping more than one instance of the app open in the same time.

pie also has several Build, Run & Git shortcuts integrated, making your life easier whenever you feel the urge to run a command through the terminal. In case you still need a terminal, pie also has this option!

### ![arrow-right](https://i.imgur.com/4Eg9yg9.png) Beautiful design
pie is built in C# using Microsoft's .NET Winforms technology. The main controls use the **Krypton Suite**, available at: https://github.com/ComponentFactory/Krypton. Without Krypton, pie wouldn't be as awesome as it is now.

### ![arrow-right](https://i.imgur.com/4Eg9yg9.png) Simple, but powerful code editor
As for the boxes you use in order to write content to a file, these were at the beginning instances of **FastColoredTextBox**, available at: https://github.com/PavelTorgashov/FastColoredTextBox. FastColoredTextbox isn't as advanced as professional editors such as Visual Studio Code or IntelliJ, as it doesn't provide autocomplete features or advanced code highlighting, but it is still better than having none of these features at all. Later, I have upgraded to **ScintilaNET**, a .NET implementation of the well-known Scintilla, available at: https://github.com/jacobslusser/ScintillaNET.

### ![arrow-right](https://i.imgur.com/4Eg9yg9.png) Code highlights and autocomplete
pie automatically highlights the code in your files. At this moment, it supports some basic ScintillaNET user-submitted highlights, available on https://github.com/jacobslusser/ScintillaNET/wiki/User-Submitted-Recipes and https://github.com/VPKSoft/ScintillaLexers/.

The autocomplete feature is also provided through an implementation of AutocompleteMenu-ScintillaNet (see https://github.com/Ahmad45123/AutoCompleteMenu-ScintillaNET).

![image](https://i.imgur.com/PrC4Gz1.png)

### ![arrow-right](https://i.imgur.com/4Eg9yg9.png) Switch between themes
pie allows you to change all of its colors. From syntax highlighting to buttons. It provides you several pre-installed themes, but if you feel creative, you can easily add new themes (or modify the existing ones).

![image](https://i.imgur.com/7gE6UzG.png)

Each `.json` file added to the `config/themes` directory will be made available in the list of pie themes. `Name.json` will be automatically converted to a new theme called `Name`.

### ![arrow-right](https://i.imgur.com/4Eg9yg9.png) Terminal
A Terminal object is actually an instance of **ConEmu Inside** (GitHub Page: https://github.com/Maximus5/conemu-inside), available at: https://www.nuget.org/packages/ConEmu.Control.WinForms.

The terminal tab control can be accessed via `Interface` (or right click somewhere in your editor) -> `Show Terminal Tab`, or `Ctrl` + `B`.
Right click the tab control (either a tab or the empty slots next to the tab) to open the context menu.

![image](https://i.imgur.com/5dVZlQZ.png)

### ![arrow-right](https://i.imgur.com/4Eg9yg9.png) Build, Run, and Git functionalities
You can use both the terminal or the integrated commands (from `Build`, `Run` and `Git` tabs in the upper menu), in order to run some common commands.

![image](https://i.imgur.com/f7KGSY8.png)

Through the `Build` tab, you are able to compile Java or C source files. You are also able to add your own custom Build commands and specify the currently opened file using the `$FILE` placeholder.

![image](https://i.imgur.com/pPooUzU.png)

The `Run` tab allows you to choose what type of file to run (either interpret a Python/Perl script, run a Java class file or interpret an HTML page). 

pie **doesn't** provide any compilers or interpreters. It tries to run the default commands (`javac`, `java`, `gcc`, `py`, ...), meaning that you already need to have these tools installed on your system. Thus meaning, you won't need to worry that the IDE's build tools is going to wrongly mix up with your already-installed tools.

Finally, the `Show Git Tab` button from the `Interface` tab allows you to manage your Git repository in a professional way, allowing you to keep track of all your changes and easily switch between branches.

![image](https://i.imgur.com/n8hB6Zm.png)

### ![arrow-right](https://i.imgur.com/4Eg9yg9.png) Execute SQL queries against a database

pie provides you the ability to run simple SQL queries against *MySQL*, *MSSQL* or *PostgreSQL* databases. By navigating to `Preferences` -> `Databases`, you will be provided an option to store all of your database connections.

![image](https://i.imgur.com/SBsnXuE.png)

Such databases can be selected through the context menu in a tab where a `.sql` file has been opened.

![image](https://i.imgur.com/hlGNvtb.png)
