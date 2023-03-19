using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace pie
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            App myApp = new App();
            myApp.Run(Environment.GetCommandLineArgs());
        }
    }

    class App : WindowsFormsApplicationBase
    {
        public App()
        {
            // Make this a single-instance application
            this.IsSingleInstance = true;
            this.EnableVisualStyles = true;

            // There are some other things available in 
            // the VB application model, for
            // instance the shutdown style:
            this.ShutdownStyle =
              Microsoft.VisualBasic.ApplicationServices.ShutdownMode.AfterMainFormCloses;

            // Add StartupNextInstance handler
            this.StartupNextInstance +=
              new StartupNextInstanceEventHandler(this.SIApp_StartupNextInstance);
        }

        protected override void OnCreateMainForm()
        {
            // Create an instance of the main form 
            // and set it in the application; 
            // but don't try to run() it.
            this.MainForm = new MainForm();

            // We want to pass along the command-line arguments to 
            // this first instance

            // Allocate room in our string array
            ((MainForm)this.MainForm).Args =
                  new string[this.CommandLineArgs.Count];

            // And copy the arguments over to our form
            this.CommandLineArgs.CopyTo(((MainForm)this.MainForm).Args, 0);
        }

        protected void SIApp_StartupNextInstance(object sender, StartupNextInstanceEventArgs eventArgs)
        {
            // Copy the arguments to a string array
            string[] args = new string[eventArgs.CommandLine.Count];
            eventArgs.CommandLine.CopyTo(args, 0);

            // Need to use invoke to b/c this is being called 
            // from another thread.
            this.MainForm.Invoke(new MainForm.ProcessParametersDelegate(
                ((MainForm)this.MainForm).ProcessParameters), new object[] { args });
        }
    }
}
