using Syncurr.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Syncurr
{
    class Syncurr
    {
        protected Thread syncThread;
        protected List<Folder> folders;
        protected System.Timers.Timer timer;

        public Syncurr()
        {
            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000 * 60 * 10;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Sync();
        }

        public event EventHandler SyncStarted;
        protected virtual void OnSyncStarted(EventArgs e)
        {
            EventHandler handler = SyncStarted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler SyncStopped;
        protected virtual void OnSyncStopped(EventArgs e)
        {
            EventHandler handler = SyncStopped;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler Error;
        protected virtual void OnError(ErrorEventArgs e)
        {
            EventHandler handler = Error;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public class ErrorEventArgs : EventArgs
        {
            public String message { get; set; }
            public ErrorEventArgs(String message)
            {
                this.message = message;
            }
        }


        public void Sync()
        {
            if (this.syncThread == null || this.syncThread.ThreadState == ThreadState.Stopped)
            {
                this.folders = Folder.folders;
                this.syncThread = new Thread(new ThreadStart(DoSync));
                this.syncThread.Start();
            }

        }

        public void Sync(Folder folder)
        {
            if (this.syncThread == null || this.syncThread.ThreadState == ThreadState.Stopped)
            {
                this.folders = new List<Folder> { folder };
                this.syncThread = new Thread(new ThreadStart(DoSync));
                this.syncThread.Start();
            }
        }

        protected void DoSync()
        {
            OnSyncStarted(new EventArgs());
            foreach(Folder folder in this.folders)
            {
                try
                {
                    folder.Sync();
                }
                catch (WebException we)
                {
                    if (we.Response != null && ((HttpWebResponse)we.Response).StatusCode == HttpStatusCode.Forbidden)
                    {
                        OnError(new ErrorEventArgs(we.Message));
                    }
                    else
                    {
                        OnError(new ErrorEventArgs(we.Message));
                    }
                }
                catch (Exception e)
                {
                    OnError(new ErrorEventArgs(e.Message));
                }
            }
            OnSyncStopped(new EventArgs());
        }
    }
}
