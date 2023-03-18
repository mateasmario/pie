# pie
Lightweight text editor with some third-party features, capable of replacing Notepad.

![image](https://imgur.com/7PmVmWT.png)

### Short Introduction
pie is a combination between a text-editor (like Notepad) and an over-simplified IDE, that allows super-fast modification of code files (or other types of files), whenever you don't feel the need to wait for a big IDE to open.

pie has integrated tabs, in order to give the user the ability to manipulate multiple files at once, without keeping more than one instance of the app open in the same time.

pie also has several Build, Run & Git shortcuts integrated, making your life easier whenever you feel the urge to run a command through the terminal. In case you still need a terminal, pie also has this option!

### Beatiful design
pie is built in C# using Microsoft's .NET Winforms technology. The main controls use the **Krypton Suite**, available at: https://github.com/ComponentFactory/Krypton. Without Krypton, pie wouldn't be as awesome as it is now.

### Simple, but powerful code editor
As for the boxes you use in order to write content to a file, these are instances of **FastColoredTextBox**, available at: https://github.com/PavelTorgashov/FastColoredTextBox. FastColoredTextbox isn't as advanced as professional editors such as Visual Studio Code or IntelliJ, as it doesn't provide autocomplete features or advanced code highlighting, but it is still better than having none of these features at all.

### Terminal
A Terminal object is actually an instance of **ConsoleControl**, available at: https://github.com/dwmkerr/consolecontrol.

The terminal tab control can be accessed via `Interface` (or right click) -> `Show Terminal Tab`, or `Ctrl` + `B`.

![image](https://imgur.com/Ig1lm2o.png)

Right click the tab control (either a tab or the empty slots next to the tab) to open the context menu.

![image](https://imgur.com/rhu7qVB.png)

### Build, Run and Git functionalities
You can use both the terminal or the integrated commands (from `Build`, `Run` and `Git` tabs in the upper menu), in order to run some common commands.

![image](https://imgur.com/zYc0plN.png)

Through the `Build` tab, you are able to compile Java or C source files. 

The `Run` tab allows you to choose what type of file to run (either interpret a Python/Perl script, run a Java class file or interpret an HTML page). 

Finally, the `Git` tab allows you to run common git commands such as `git add .`, `git commit -m ""` (with a special dialogue that allows you to write a custom commit message), and so on.

![image](https://imgur.com/X8G3Eld.png)

pie **doesn't** provide any compilers or interpreters. It tries to run the default commands (`javac`, `java`, `gcc`, `py`, ...), meaning that you already need to have these tools installed on your system. Thus meaning, you won't need to worry that the IDE's build tools is going to wrongly mix up with your already-installed tools.

### Code highlights
In order to enable code highlight features, right click in your text editor and select the preferred language from the `Code Formatting` category.

![image](https://imgur.com/DcM92xJ.png)
