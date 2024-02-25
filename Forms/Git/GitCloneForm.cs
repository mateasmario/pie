/** Copyright (C) 2023  Mario-Mihai Mateas
 * 
 * This file is part of pie.
 * 
 * pie is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * 
 * along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/

using System;
using System.IO;
using System.Windows.Forms;

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
using System.Threading.Tasks;

namespace pie
{
    public partial class GitCloneForm : KryptonForm
    {
        public GitCloneForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonTextBox1.Palette = Globals.kryptonPalette;
            kryptonTextBox2.Palette = Globals.kryptonPalette;
            kryptonLabel1.Palette = Globals.kryptonPalette;
            kryptonLabel2.Palette = Globals.kryptonPalette;
            kryptonButton1.Palette = Globals.kryptonPalette;
            kryptonButton2.Palette = Globals.kryptonPalette;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text == "" || kryptonTextBox2.Text == "")
            {
                MainForm.ShowNotification("Repository URL and clone location should not be empty.");
            }
            else if (!Directory.Exists(kryptonTextBox2.Text))
            {
                MainForm.ShowNotification("Directory does not exist. Create it before cloning a repository.");
            }
            else if (Directory.GetFiles(kryptonTextBox2.Text).Length > 0 || Directory.GetDirectories(kryptonTextBox2.Text).Length > 0)
            {
                MainForm.ShowNotification("Specified directory needs to be empty.");
            }
            else
            {
                GitClone();
            }
        }

        private void GitClone()
        {
            if (Globals.gitCredentials.Username == null || Globals.gitCredentials.Password == null)
            {
                GitPushCredentialsForm gitCredentialsForm = new GitPushCredentialsForm();
                Globals.gitFormClosedWithOk = false;
                gitCredentialsForm.ShowDialog();

                if (Globals.gitFormClosedWithOk)
                {
                    File.WriteAllText("git.config", Globals.gitCredentials.Name + "\n" + Globals.gitCredentials.Email + "\n" + Globals.gitCredentials.Username + "\n" + Globals.gitCredentials.Password);
                    GitClone();
                }
            }
            else
            {
                var options = new CloneOptions
                {
                    CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                    {
                        Username = Globals.gitCredentials.Username,
                        Password = Globals.gitCredentials.Password
                    }
                };

                try
                {
                    Task.Run(() =>
                    {
                        Repository.Clone(kryptonTextBox1.Text, kryptonTextBox2.Text, options);
                    }).Wait();

                    Globals.clonePath = kryptonTextBox2.Text;
                    this.Close();
                } catch(NameConflictException e)
                {
                    MainForm.ShowNotification(e.Message);
                } catch(LibGit2SharpException e)
                {
                    MainForm.ShowNotification(e.Message);
                }
            }
        }

        private void GitCloneForm_Load(object sender, EventArgs e)
        {
            if (Globals.glass)
            {
                this.Opacity = 0.875;
            }

            Globals.clonePath = null;
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                kryptonTextBox2.Text = dialog.SelectedPath;
            }
        }
    }
}
