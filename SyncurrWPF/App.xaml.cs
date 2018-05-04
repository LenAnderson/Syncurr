using Microsoft.Shell;
using SyncurrWPF.Helpers;
using SyncurrWPF.ViewModels;
using SyncurrWPF.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SyncurrWPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application, ISingleInstanceApp
	{
		private const string Unique = "Syncurr";

		private AppViewModel ViewModel;
		private AppView View;


		[STAThread]
		public static void Main(string[] args)
		{
			if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
			{
				var application = new App();
				application.InitializeComponent();
				application.Run();

				SingleInstance<App>.Cleanup();
			}
		}


		public bool SignalExternalCommandLineArgs(IList<string> args)
		{
			return true;
		}


		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			ViewModel = new AppViewModel();
			ImgurHelper.Context = ViewModel;
			View = new AppView(ViewModel);
			View.Show();

		}


		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
		}
	}
}
