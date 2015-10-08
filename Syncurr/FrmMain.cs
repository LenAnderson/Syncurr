using Syncurr.Imgur.API;
using Syncurr.Imgur.Auth;
using Syncurr.Local;
using Syncurr.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syncurr
{
    public partial class FrmMain : Form
    {
        Syncurr syncurr;

        public FrmMain()
        {
            InitializeComponent();

            chkProxy.Checked = Properties.Settings.Default.proxy;
            rdoProxyTypeHttp.Checked = Properties.Settings.Default.proxyType == "HTTP";
            rdoProxyTypeSocks4.Checked = Properties.Settings.Default.proxyType == "SOCKS4";
            rdoProxyTypeSocks5.Checked = Properties.Settings.Default.proxyType == "SOCKS5";
            txtProxyUrl.Text = Properties.Settings.Default.proxyUrl;
            numProxyPort.Value = Properties.Settings.Default.proxyPort;
            txtProxyInternalUrl.Text = Properties.Settings.Default.proxyInternalUrl;
            numProxyInternalPort.Value = Properties.Settings.Default.proxyInternalPort;
            txtProxyUsername.Text = Properties.Settings.Default.proxyUsername;
            txtProxyPassword.Text = Properties.Settings.Default.proxyPassword;

            chkProxy_CheckedChanged(null, null);
            rdoProxyType_CheckedChanged(null, null);


            foreach (String path in Properties.Settings.Default.folders)
            {
                Folder folder = Folder.Get(path);
                lstAlbums.Items.Add(new FolderListViewItem(folder));
            }

            syncurr = new Syncurr();
            syncurr.SyncStarted += Syncurr_SyncStarted;
            syncurr.SyncStopped += Syncurr_SyncStopped;
            prgSync.MarqueeAnimationSpeed = 30;
            prgSync.Hide();

            syncurr.Error += Syncurr_Error;
        }

        private void Syncurr_Error(object sender, EventArgs e)
        {
            Syncurr.ErrorEventArgs args = (Syncurr.ErrorEventArgs)e;
            MessageBox.Show(args.message, "Something went wrong!");
        }

        private void Syncurr_SyncStarted(object sender, EventArgs e)
        {
            prgSync.Invoke(new MethodInvoker(delegate { prgSync.Show(); }));
        }
        private void Syncurr_SyncStopped(object sender, EventArgs e)
        {
            prgSync.Invoke(new MethodInvoker(delegate { prgSync.Hide(); }));
        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(OAuth.pinUrl);
        }

        private void btnTokens_Click(object sender, EventArgs e)
        {
            if (txtPin.Text.Trim().Length > 0)
            {
                try {
                    OAuth.RetrieveTokensWithPin(txtPin.Text);
                }
                catch (WebException we)
                {
                    if (we.Response != null && ((HttpWebResponse)we.Response).StatusCode == HttpStatusCode.BadRequest)
                    {
                        MessageBox.Show("Please request a new pin from imgur.", "Bad Pin!", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show(we.Message, "Something went wrong!", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void chkProxy_CheckedChanged(object sender, EventArgs e)
        {
            grpProxyType.Enabled = chkProxy.Checked;
            grpProxySettings.Enabled = chkProxy.Checked;

            Properties.Settings.Default.proxy = chkProxy.Checked;
            settingsChanged(sender, e);
        }

        private void rdoProxyType_CheckedChanged(object sender, EventArgs e)
        {
            txtProxyInternalUrl.Enabled = rdoProxyTypeSocks4.Checked || rdoProxyTypeSocks5.Checked;
            numProxyInternalPort.Enabled = rdoProxyTypeSocks4.Checked || rdoProxyTypeSocks5.Checked;
            if (rdoProxyTypeHttp.Checked)
                Properties.Settings.Default.proxyType = "HTTP";
            else if (rdoProxyTypeSocks4.Checked)
                Properties.Settings.Default.proxyType = "SOCKS4";
            else if (rdoProxyTypeSocks5.Checked)
                Properties.Settings.Default.proxyType = "SOCKS5";
            else
                Properties.Settings.Default.proxyType = "";

            settingsChanged(sender, e);
        }

        private void settingsChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void numProxyPort_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.proxyPort = numProxyPort.Value;
            settingsChanged(sender, e);
        }

        private void numProxyInternalPort_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.proxyInternalPort = numProxyInternalPort.Value;
            settingsChanged(sender, e);
        }

        private void txtProxyUrl_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.proxyUrl = txtProxyUrl.Text;
            settingsChanged(sender, e);
        }

        private void txtProxyInternalUrl_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.proxyInternalUrl = txtProxyInternalUrl.Text;
            settingsChanged(sender, e);
        }

        private void txtProxyUsername_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.proxyUsername = txtProxyUsername.Text;
            settingsChanged(sender, e);
        }

        private void txtProxyPassword_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.proxyPassword = txtProxyPassword.Text;
            settingsChanged(sender, e);
        }

        private void lstAlbums_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lstAlbums.FocusedItem.Bounds.Contains(e.Location))
                {
                    ctxAlbum.Show(Cursor.Position);
                }
            }
        }

        private void FrmMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
                foreach (String path in files)
                {
                    if (Directory.Exists(path))
                    {
                        Folder folder = Folder.Get(path);
                        folder.Save();
                        lstAlbums.Items.Add(new FolderListViewItem(folder));
                    }
                }
            }
        }

        private void FrmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
        }

        private void ctxAlbumRemove_Click(object sender, EventArgs e)
        {
            ((FolderListViewItem)lstAlbums.FocusedItem).folder.Remove();
            lstAlbums.FocusedItem.Remove();
        }

        private void ctxAlbumProperties_Click(object sender, EventArgs e)
        {
            String path = lstAlbums.FocusedItem.SubItems[2].Text;
            FrmAlbumProperties form = new FrmAlbumProperties(path);
            form.ShowDialog(this);
        }

        private void ctxAlbumSync_Click(object sender, EventArgs e)
        {
            syncurr.Sync(((FolderListViewItem)lstAlbums.FocusedItem).folder);
        }

        private void lstAlbums_DoubleClick(object sender, EventArgs e)
        {
            String path = lstAlbums.FocusedItem.SubItems[2].Text;
            FrmAlbumProperties form = new FrmAlbumProperties(path);
            form.ShowDialog(this);
        }

        private void ntfTray_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void ctxAlbumOpenFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(((FolderListViewItem)lstAlbums.FocusedItem).folder.path);
        }

        private void ctxAlbumOpenAlbum_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Album.GetLink(((FolderListViewItem)lstAlbums.FocusedItem).folder.albumId));
        }
    }
}
