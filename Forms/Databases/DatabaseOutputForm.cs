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
using System.Data;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie.Forms.Databases
{
    public partial class DatabaseOutputForm : KryptonForm
    {
        private DataTable dataTable;

        public DatabaseOutputForm(DataTable dataTable)
        {
            InitializeComponent();

            this.dataTable = dataTable;

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonButton3.Palette = Globals.kryptonPalette;
            kryptonDataGridView1.Palette = Globals.kryptonPalette;
            kryptonDataGridView1.StateCommon.Background.ColorStyle = PaletteColorStyle.Solid;
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DatabaseOutputForm_Load(object sender, EventArgs e)
        {
            kryptonDataGridView1.DataSource = this.dataTable;
        }
    }
}
