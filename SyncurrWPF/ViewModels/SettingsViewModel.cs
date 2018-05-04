using SyncurrWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SyncurrWPF.ViewModels
{
	public class SettingsViewModel : ViewModel
	{
		public bool SyncMe
		{
			get { return Properties.Settings.Default.SyncMe; }
			set
			{
				if (Properties.Settings.Default.SyncMe != value)
				{
					Properties.Settings.Default.SyncMe = value;
					Properties.Settings.Default.Save();
					OnPropertyChanged(nameof(SyncMe));
				}
			}
		}

		public string MeRoot
		{
			get { return Properties.Settings.Default.MeRoot; }
			set
			{
				if (Properties.Settings.Default.MeRoot != value)
				{
					Properties.Settings.Default.MeRoot = value;
					Properties.Settings.Default.Save();
					OnPropertyChanged(nameof(MeRoot));
				}
			}
		}

		public int SyncInterval
		{
			get { return Properties.Settings.Default.SyncInterval; }
			set
			{
				if (Properties.Settings.Default.SyncInterval != value)
				{
					Properties.Settings.Default.SyncInterval = value;
					Properties.Settings.Default.Save();
					OnPropertyChanged(nameof(SyncInterval));
				}
			}
		}




		public SettingsViewModel()
		{
			Title = "Settings";
		}




		#region Commands
		private ICommand _disconnectCommand;
		public ICommand DisconnectCommand
		{
			get
			{
				if (_disconnectCommand == null)
				{
					_disconnectCommand = new RelayCommand(p => {
						Properties.Settings.Default.Token = null;
						Properties.Settings.Default.Save();
					});
				}
				return _disconnectCommand;
			}
		}

		private ICommand _syncNowCommand;
		public ICommand SyncNowCommand
		{
			get
			{
				if (_syncNowCommand == null)
				{
					_syncNowCommand = new RelayCommand(p => {

					});
				}
				return _syncNowCommand;
			}
		}
		#endregion
	}
}
