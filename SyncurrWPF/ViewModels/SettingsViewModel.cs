using SyncurrWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SyncurrWPF.ViewModels
{
	public class SettingsViewModel : ViewModel
	{
		public bool Startup
		{
			get { return Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false).GetValue(Assembly.GetExecutingAssembly().GetName().Name) != null; }
			set
			{
				if (value)
				{
					Assembly curAssembly = Assembly.GetExecutingAssembly();
					Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true).SetValue(curAssembly.GetName().Name, curAssembly.Location);
				}
				else
				{
					Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true).DeleteValue(Assembly.GetExecutingAssembly().GetName().Name);
				}
			}
		}

		public bool StartMinimized
		{
			get { return Properties.Settings.Default.StartMinimized; }
			set
			{
				if (Properties.Settings.Default.StartMinimized != value)
				{
					Properties.Settings.Default.StartMinimized = value;
					Properties.Settings.Default.Save();
					OnPropertyChanged(nameof(StartMinimized));
				}
			}
		}

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

		public bool DeleteLocalFolder
		{
			get { return Properties.Settings.Default.DeleteLocalFolder; }
			set
			{
				if (Properties.Settings.Default.DeleteLocalFolder != value)
				{
					Properties.Settings.Default.DeleteLocalFolder = value;
					Properties.Settings.Default.Save();
					OnPropertyChanged(nameof(DeleteLocalFolder));
				}
			}
		}

		public bool AskDeleteLocalFolder
		{
			get { return Properties.Settings.Default.AskDeleteLocalFolder; }
			set
			{
				if (Properties.Settings.Default.AskDeleteLocalFolder != value)
				{
					Properties.Settings.Default.AskDeleteLocalFolder = value;
					Properties.Settings.Default.Save();
					OnPropertyChanged(nameof(AskDeleteLocalFolder));
				}
			}
		}

		public bool DeleteLocalImage
		{
			get { return Properties.Settings.Default.DeleteLocalImage; }
			set
			{
				if (Properties.Settings.Default.DeleteLocalImage != value)
				{
					Properties.Settings.Default.DeleteLocalImage = value;
					Properties.Settings.Default.Save();
					OnPropertyChanged(nameof(DeleteLocalImage));
				}
			}
		}

		public bool AskDeleteLocalImage
		{
			get { return Properties.Settings.Default.AskDeleteLocalImage; }
			set
			{
				if (Properties.Settings.Default.AskDeleteLocalImage != value)
				{
					Properties.Settings.Default.AskDeleteLocalImage = value;
					Properties.Settings.Default.Save();
					OnPropertyChanged(nameof(AskDeleteLocalImage));
				}
			}
		}

		public bool DeleteRemoteFolder
		{
			get { return Properties.Settings.Default.DeleteRemoteFolder; }
			set
			{
				if (Properties.Settings.Default.DeleteRemoteFolder != value)
				{
					Properties.Settings.Default.DeleteRemoteFolder = value;
					Properties.Settings.Default.Save();
					OnPropertyChanged(nameof(DeleteRemoteFolder));
				}
			}
		}

		public bool AskDeleteRemoteFolder
		{
			get { return Properties.Settings.Default.AskDeleteRemoteFolder; }
			set
			{
				if (Properties.Settings.Default.AskDeleteRemoteFolder != value)
				{
					Properties.Settings.Default.AskDeleteRemoteFolder = value;
					Properties.Settings.Default.Save();
					OnPropertyChanged(nameof(AskDeleteRemoteFolder));
				}
			}
		}

		public bool DeleteRemoteImage
		{
			get { return Properties.Settings.Default.DeleteRemoteImage; }
			set
			{
				if (Properties.Settings.Default.DeleteRemoteImage != value)
				{
					Properties.Settings.Default.DeleteRemoteImage = value;
					Properties.Settings.Default.Save();
					OnPropertyChanged(nameof(DeleteRemoteImage));
				}
			}
		}

		public bool AskDeleteRemoteImage
		{
			get { return Properties.Settings.Default.AskDeleteRemoteImage; }
			set
			{
				if (Properties.Settings.Default.AskDeleteRemoteImage != value)
				{
					Properties.Settings.Default.AskDeleteRemoteImage = value;
					Properties.Settings.Default.Save();
					OnPropertyChanged(nameof(AskDeleteRemoteImage));
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
