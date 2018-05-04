using MahApps.Metro.Controls.Dialogs;
using SyncurrWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncurrWPF.ViewModels
{
	public interface ISyncViewModel
	{
		Task Sync(ProgressDialogController pdc);
	}
}
