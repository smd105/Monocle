﻿using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MonocleUI
{
    public partial class MonocleUI : Form
    {
        Files InputFiles = new Files();

        public MonocleUI()
        {
            InitializeComponent();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if(input_file_dialog.ShowDialog() == DialogResult.OK)
            {
                foreach(string file in input_file_dialog.FileNames)
                {
                    if (InputFiles.Add(file))
                    {
                        input_files_dgv.Rows.Add(file);
                    }
                }
            }
        }
        private void Input_files_dgv_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Input_files_dgv_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileArray;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                fileArray = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string filePath in fileArray)
                {
                    if (InputFiles.Add(filePath))
                    {
                        input_files_dgv.Rows.Add(filePath);
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (export_folder_dialog.ShowDialog() == DialogResult.OK)
            {
                export_folder_maskedTB.Text = export_folder_dialog.SelectedPath;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if(input_files_dgv.SelectedRows.Count > 0)
            {
                foreach(DataGridViewRow row in input_files_dgv.Rows)
                {
                    if (row.Selected)
                    {
                        input_files_dgv.Rows.Remove(row);
                    }
                }
                
            }
        }
    }
}
