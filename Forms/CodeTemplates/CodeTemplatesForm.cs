/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using pie.Services;
using pie.Classes;

/**
 * ScintillaNET provides the text editors used in pie.
 * 
 * Copyright (c) 2017, Jacob Slusser, https://github.com/jacobslusser
*/
using ScintillaNET;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie.Forms.CodeTemplates
{
    public partial class CodeTemplatesForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();
        private ConfigurationService configurationService = new ConfigurationService();

        public CodeTemplatesFormInput Input { get; set; }
        public CodeTemplatesFormOutput Output { get; private set; }

        private List<CodeTemplate> Working { get; set; }

        private int selectedIndex = -1;

        public CodeTemplatesForm()
        {
            InitializeComponent();

            template1CheckButton.Click += kryptonCheckButton_Click;
            template2CheckButton.Click += kryptonCheckButton_Click;
            template4CheckButton.Click += kryptonCheckButton_Click;
            template3CheckButton.Click += kryptonCheckButton_Click;
            template8CheckButton.Click += kryptonCheckButton_Click;
            template7CheckButton.Click += kryptonCheckButton_Click;
            template6CheckButton.Click += kryptonCheckButton_Click;
            template5CheckButton.Click += kryptonCheckButton_Click;
            template0CheckButton.Click += kryptonCheckButton_Click;
            template9CheckButton.Click += kryptonCheckButton_Click;

            Output = new CodeTemplatesFormOutput();
        }

        private void CodeTemplatesForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            ColorizeButton(template1CheckButton);
            ColorizeButton(template2CheckButton);
            ColorizeButton(template4CheckButton);
            ColorizeButton(template3CheckButton);
            ColorizeButton(template8CheckButton);
            ColorizeButton(template7CheckButton);
            ColorizeButton(template6CheckButton);
            ColorizeButton(template5CheckButton);
            ColorizeButton(template0CheckButton);
            ColorizeButton(template9CheckButton);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            Working = Input.CodeTemplates.ConvertAll(codeTemplate => codeTemplate.Clone());

            int x = (textAreaPanel.Width - placeholderLabel.Width) / 2;
            int y = (textAreaPanel.Height - placeholderLabel.Height) / 2;
            placeholderLabel.Location = new Point(x, y);
        }

        private void SaveCurrentTemplate()
        {
            if (selectedIndex >= 0)
            {
                CodeTemplate currentTemplate = Working.Find(codeTemplate => codeTemplate.Index == selectedIndex);
                string text = ((Scintilla)textAreaPanel.Controls[1]).Text;

                if (text != "")
                {
                    if (currentTemplate != null)
                    {
                        currentTemplate.Content = text;
                    }
                    else
                    {
                        Working.Add(new CodeTemplate(selectedIndex, text));
                    }
                }
                else
                {
                    if (currentTemplate != null)
                    {
                        Working.Remove(currentTemplate);
                    }
                }
            }
        }

        private void kryptonCheckButton_Click(object sender, EventArgs e)
        {
            template1CheckButton.Checked = false;
            template2CheckButton.Checked = false;
            template4CheckButton.Checked = false;
            template3CheckButton.Checked = false;
            template8CheckButton.Checked = false;
            template7CheckButton.Checked = false;
            template6CheckButton.Checked = false;
            template5CheckButton.Checked = false;
            template0CheckButton.Checked = false;
            template9CheckButton.Checked = false;

            KryptonCheckButton kryptonCheckButton = (KryptonCheckButton)sender;
            kryptonCheckButton.Checked = true;

            if (selectedIndex == -1)
            {
                placeholderLabel.Visible = false;

                Scintilla TextArea = new Scintilla();

                TextArea.BorderStyle = ScintillaNET.BorderStyle.None;
                TextArea.StyleResetDefault();
                TextArea.Styles[Style.Default].Font = "Consolas";
                TextArea.Styles[Style.Default].Size = 15;
                TextArea.Styles[Style.Default].ForeColor = Input.ActiveTheme.Fore;
                TextArea.CaretForeColor = Input.ActiveTheme.Fore;
                TextArea.Styles[Style.Default].BackColor = Input.ActiveTheme.Primary;
                TextArea.SetSelectionBackColor(true, Input.ActiveTheme.Selection);
                TextArea.CaretLineBackColor = Input.ActiveTheme.CaretLineBack;
                TextArea.StyleClearAll();
                TextArea.Styles[Style.LineNumber].BackColor = Input.ActiveTheme.NumberMargin;
                TextArea.Styles[Style.LineNumber].ForeColor = Input.ActiveTheme.Fore;
                TextArea.Styles[Style.IndentGuide].ForeColor = Input.ActiveTheme.Fore;
                TextArea.Styles[Style.IndentGuide].BackColor = Input.ActiveTheme.NumberMargin;

                TextArea.Margins[0].Width = 24;
                TextArea.Parent = textAreaPanel;
                TextArea.Dock = DockStyle.Fill;
            }
            else
            {
                SaveCurrentTemplate();
            }

            selectedIndex = Convert.ToInt32(kryptonCheckButton.Text);
            SyncScintillaWithSelectedTemplate((Scintilla)textAreaPanel.Controls[1]);

            saveButton.Enabled = true;
        }

        private void ColorizeButton(KryptonCheckButton kryptonCheckButton)
        {
            kryptonCheckButton.StateCommon.Back.Color1 = Input.ActiveTheme.Primary;
            kryptonCheckButton.StateCommon.Back.Color2 = Input.ActiveTheme.Primary;
            kryptonCheckButton.StateCheckedNormal.Back.Color1 = Input.ActiveTheme.Secondary;
            kryptonCheckButton.StateCheckedNormal.Back.Color2 = Input.ActiveTheme.Secondary;
            kryptonCheckButton.StateCheckedTracking.Back.Color1 = Input.ActiveTheme.Secondary;
            kryptonCheckButton.StateCheckedTracking.Back.Color2 = Input.ActiveTheme.Secondary;
            kryptonCheckButton.StateCheckedPressed.Back.Color1 = Input.ActiveTheme.Secondary;
            kryptonCheckButton.StateCheckedPressed.Back.Color2 = Input.ActiveTheme.Secondary;
            kryptonCheckButton.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton.StateCheckedNormal.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton.StateCheckedTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton.StateCheckedPressed.Back.ColorStyle = PaletteColorStyle.Solid;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            SaveCurrentTemplate();

            configurationService.WriteToFile("config\\templates.json", Working);

            Output.Saved = true;

            this.Close();
        }

        private void SyncScintillaWithSelectedTemplate(Scintilla scintilla)
        {
            CodeTemplate codeTemplate = Working.Find(t => t.Index == selectedIndex);

            if (codeTemplate != null)
            {
                scintilla.Text = codeTemplate.Content;
            }
            else
            {
                scintilla.Text = "";
            }
        }

        private void CodeTemplatesForm_Resize(object sender, EventArgs e)
        {
            buttonPanel.Left = (this.Width - buttonPanel.Width) / 2;
            placeholderLabel.Left = (this.Width - placeholderLabel.Width) / 2;
        }
    }
}
