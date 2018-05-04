using MahApps.Metro.Controls.Dialogs;
using SyncurrWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncurrWPF.ViewModels
{
	public class SyncViewModel : ViewModel, ISyncViewModel
	{
		public async Task Sync(ProgressDialogController pdc)
		{
			IsLoading = true;
			await DoSync(pdc);
			IsLoading = false;
		}

		protected virtual async Task DoSync(ProgressDialogController pdc) { }
	}
}
