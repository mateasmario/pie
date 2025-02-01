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

            indexButton1.Click += kryptonCheckButton_Click;
            indexButton2.Click += kryptonCheckButton_Click;
            indexButton4.Click += kryptonCheckButton_Click;
            indexButton3.Click += kryptonCheckButton_Click;
            indexButton8.Click += kryptonCheckButton_Click;
            indexButton7.Click += kryptonCheckButton_Click;
            indexButton6.Click += kryptonCheckButton_Click;
            indexButton5.Click += kryptonCheckButton_Click;
            indexButton0.Click += kryptonCheckButton_Click;
            indexButton9.Click += kryptonCheckButton_Click;

            Output = new CodeTemplatesFormOutput();
        }

        private void CodeTemplatesForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            ColorizeButton(indexButton1);
            ColorizeButton(indexButton2);
            ColorizeButton(indexButton4);
            ColorizeButton(indexButton3);
            ColorizeButton(indexButton8);
            ColorizeButton(indexButton7);
            ColorizeButton(indexButton6);
            ColorizeButton(indexButton5);
            ColorizeButton(indexButton0);
            ColorizeButton(indexButton9);

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
            indexButton1.Checked = false;
            indexButton2.Checked = false;
            indexButton4.Checked = false;
            indexButton3.Checked = false;
            indexButton8.Checked = false;
            indexButton7.Checked = false;
            indexButton6.Checked = false;
            indexButton5.Checked = false;
            indexButton0.Checked = false;
            indexButton9.Checked = false;

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

            selectedIndex = Convert.ToInt32(kryptonCheckButton.Name.Remove(0, 11));
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
    }
}
