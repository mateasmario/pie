/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using pie.Classes;

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
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            myApp.Run(Environment.GetCommandLineArgs());
        }

        private static void CurrentDomain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            ShowFatalNotification(e.ExceptionObject.ToString());
        }

        private static void ShowFatalNotification(string text)
        {
            NotificationFatalForm notificationFatalForm = new NotificationFatalForm();

            NotificationFatalFormInput notificationFatalFormInput = new NotificationFatalFormInput();
            notificationFatalFormInput.NotificationText = text;

            notificationFatalForm.Input = notificationFatalFormInput;

            notificationFatalForm.ShowDialog();
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
