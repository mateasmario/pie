/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using pie.Services;
using pie.Classes;

/**
 * LibGit2Sharp is used for integrating several advanced Git functionalities into pie.
 * 
 * Copyright (c) LibGit2Sharp contributors
 */
using LibGit2Sharp;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie
{
    public partial class GitCloneForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();

        public GitCloneFormInput Input { get; set; }
        public GitCloneFormOutput Output { get; set; }

        public GitCloneForm()
        {
            InitializeComponent();

            Output = new GitCloneFormOutput();
        }

        private void GitCloneForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            Output.ClonePath = null;
        }

        public void ShowNotification(string text)
        {
            NotificationOKForm notificationOkForm = new NotificationOKForm();

            NotificationFormInput notificationFormInput = new NotificationFormInput();
            notificationFormInput.EditorProperties = new EditorProperties();
            notificationFormInput.Palette = Input.Palette;
            notificationFormInput.NotificationText = text;

            notificationOkForm.Input = notificationFormInput;

            notificationOkForm.ShowDialog();
        }


        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (repositoryURLTextBox.Text == "" || cloneIntoTextBox.Text == "")
            {
                ShowNotification("Repository URL and clone location should not be empty.");
            }
            else if (!Directory.Exists(cloneIntoTextBox.Text))
            {
                ShowNotification("Directory does not exist. Create it before cloning a repository.");
            }
            else if (Directory.GetFiles(cloneIntoTextBox.Text).Length > 0 || Directory.GetDirectories(cloneIntoTextBox.Text).Length > 0)
            {
                ShowNotification("Specified directory needs to be empty.");
            }
            else
            {
                GitClone();
            }
        }

        private void GitClone()
        {
            if (Input.GitCredentials.Username == null || Input.GitCredentials.Password == null)
            {
                GitPushCredentialsForm gitPushCredentialsForm = new GitPushCredentialsForm();

                GitPushCredentialsFormInput gitPushCredentialsFormInput = new GitPushCredentialsFormInput();
                gitPushCredentialsFormInput.EditorProperties = Input.EditorProperties;
                gitPushCredentialsFormInput.GitCredentials = Input.GitCredentials;
                gitPushCredentialsFormInput.Palette = Input.Palette;

                gitPushCredentialsForm.Input = gitPushCredentialsFormInput;

                gitPushCredentialsForm.ShowDialog();

                if (gitPushCredentialsForm.Output.Saved)
                {
                    File.WriteAllText("git.config", Input.GitCredentials.Name + "\n" + Input.GitCredentials.Email + "\n" + Input.GitCredentials.Username + "\n" + Input.GitCredentials.Password);
                    GitClone();
                }
            }
            else
            {
                var options = new CloneOptions
                {
                    CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                    {
                        Username = Input.GitCredentials.Username,
                        Password = Input.GitCredentials.Password
                    }
                };

                try
                {
                    Task.Run(() =>
                    {
                        Repository.Clone(repositoryURLTextBox.Text, cloneIntoTextBox.Text, options);
                    }).Wait();

                    Output.ClonePath = cloneIntoTextBox.Text;
                    this.Close();
                }
                catch (NameConflictException e)
                {
                    ShowNotification(e.Message);
                }
                catch (LibGit2SharpException e)
                {
                    ShowNotification(e.Message);
                }
            }
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                cloneIntoTextBox.Text = dialog.SelectedPath;
            }
        }
    }
}
