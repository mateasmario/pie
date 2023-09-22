using System;

namespace pie.Classes
{
    public class BuildCommand
    {
        private string name;
        private string command;

        public BuildCommand()
        {

        }

        public BuildCommand(String name, String command)
        {
            this.name = name;
            this.command = command;
        }

        public string getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public string getCommand()
        {
            return command;
        }

        public void setCommand(String command)
        {
            this.command = command;
        }
    }
}
