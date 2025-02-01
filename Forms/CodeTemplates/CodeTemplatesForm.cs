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

        private int selectedIndex = -1;

        public List<CodeTemplate> Input { get; set; }
        private List<CodeTemplate> Working { get; set; }
        public List<CodeTemplate> Output { get; private set; }

        public CodeTemplatesForm()
        {
            InitializeComponent();

            themeService.SetPaletteToObjects(this, Globals.kryptonPalette);

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
                TextArea.Styles[ScintillaNET.Style.Default].Font = "Consolas";
                TextArea.Styles[ScintillaNET.Style.Default].Size = 15;
                TextArea.Styles[ScintillaNET.Style.Default].ForeColor = Globals.theme.Fore;
                TextArea.CaretForeColor = Globals.theme.Fore;
                TextArea.Styles[ScintillaNET.Style.Default].BackColor = Globals.theme.Primary;
                TextArea.SetSelectionBackColor(true, Globals.theme.Selection);
                TextArea.CaretLineBackColor = Globals.theme.CaretLineBack;
                TextArea.StyleClearAll();
                TextArea.Styles[ScintillaNET.Style.LineNumber].BackColor = Globals.theme.NumberMargin;
                TextArea.Styles[ScintillaNET.Style.LineNumber].ForeColor = Globals.theme.Fore;
                TextArea.Styles[ScintillaNET.Style.IndentGuide].ForeColor = Globals.theme.Fore;
                TextArea.Styles[ScintillaNET.Style.IndentGuide].BackColor = Globals.theme.NumberMargin;

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

        private void CodeTemplatesForm_Load(object sender, EventArgs e)
        {
            Working = Input.ConvertAll(codeTemplate => codeTemplate.Clone());
            Output = Input;

            int x = (textAreaPanel.Width - placeholderLabel.Width) / 2;
            int y = (textAreaPanel.Height - placeholderLabel.Height) / 2;
            placeholderLabel.Location = new Point(x,y);
        }

        private void ColorizeButton(KryptonCheckButton kryptonCheckButton)
        {
            kryptonCheckButton.StateCommon.Back.Color1 = Globals.theme.Primary;
            kryptonCheckButton.StateCommon.Back.Color2 = Globals.theme.Primary;
            kryptonCheckButton.StateCheckedNormal.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton.StateCheckedNormal.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton.StateCheckedTracking.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton.StateCheckedTracking.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton.StateCheckedPressed.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton.StateCheckedPressed.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton.StateCheckedNormal.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton.StateCheckedTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton.StateCheckedPressed.Back.ColorStyle = PaletteColorStyle.Solid;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            SaveCurrentTemplate();
            Output = Working;
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
