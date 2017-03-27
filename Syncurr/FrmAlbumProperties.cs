using Syncurr.Local;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syncurr
{
    public partial class FrmAlbumProperties : Form
    {
        Folder folder;

        public FrmAlbumProperties(String path)
        {
            InitializeComponent();

            folder = Folder.Get(path);
            txtAlbumId.Text = folder.albumId;
            txtPath.Text = folder.path;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            folder.albumId = txtAlbumId.Text;
            folder.path = txtPath.Text;
            folder.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
