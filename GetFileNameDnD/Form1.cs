using System;
using System.Windows.Forms;
using System.IO;

namespace GetFileNameDnD
{
    public partial class Form1 : Form
    {
        private string ext = "";

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームが実行された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length >= 2)
            {
                this.ext = ("." + args[1].ToLower()).ToLower();
                this.Text += " (*" + this.ext + ")";
            }

            lblPath.Text = "";
            btnOK.Enabled = false;
        }

        /// <summary>
        /// btnOK が押された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblPath.Text);
            this.Close();
        }

        /// <summary>
        /// D&D がされた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDnD_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                if (this.ext != "")
                {
                    // 拡張子指定あり
                    if (Path.GetExtension(files[0]).ToLower() == this.ext)
                    {
                        lblPath.Text = files[0];
                        btnOK.Enabled = true;
                    }
                    else
                    {
                        lblPath.Text = "";
                        btnOK.Enabled = false;
                    }

                }
                else
                {
                    // 拡張子指定なし
                    lblPath.Text = files[0];
                    btnOK.Enabled = true;
                }   
            }
            else
            {
                lblPath.Text = "";
                btnOK.Enabled = false;
            }
        }

        /// <summary>
        /// D&D がされた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDnD_DragEnter(object sender, DragEventArgs e)
        {
            // 判定
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // ファイルでないと許さない
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    e.Effect = DragDropEffects.All;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
