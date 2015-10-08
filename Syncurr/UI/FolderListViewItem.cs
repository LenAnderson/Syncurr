using Syncurr.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syncurr.UI
{
    class FolderListViewItem : ListViewItem
    {
        public Folder folder { get; set; }
        public FolderListViewItem(Folder folder) : base(new String[] { folder.albumTitle, folder.albumId, folder.path })
        {
            this.folder = folder;
            this.folder.PropertyChanged += Folder_PropertyChanged;
        }
        
        private void Folder_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.ListView.InvokeRequired)
            {
                this.ListView.Invoke(new MethodInvoker(delegate
                {
                    this.SubItems[0].Text = this.folder.albumTitle;
                    this.SubItems[1].Text = this.folder.albumId;
                    this.SubItems[2].Text = this.folder.path;
                }));
            }
            else
            {
                this.SubItems[0].Text = this.folder.albumTitle;
                this.SubItems[1].Text = this.folder.albumId;
                this.SubItems[2].Text = this.folder.path;
            }
        }
    }
}
