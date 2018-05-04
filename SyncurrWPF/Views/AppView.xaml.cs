using MahApps.Metro.Controls;
using SyncurrWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SyncurrWPF.Views
{
	/// <summary>
	/// Interaction logic for AppView.xaml
	/// </summary>
	public partial class AppView : MetroWindow
	{
		public AppView(AppViewModel vm)
		{
			InitializeComponent();

			DataContext = vm;
		}

		private void AppView_Closed(object sender, EventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void AppView_DragOver(object sender, DragEventArgs e)
		{

		}

		private void AppView_Drop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				((AppViewModel)DataContext).Drop((string[])e.Data.GetData(DataFormats.FileDrop));
			}
			else if (e.Data.GetDataPresent(DataFormats.StringFormat))
			{
				((AppViewModel)DataContext).Drop(new string[] { (string)e.Data.GetData(DataFormats.StringFormat) });
			}
		}
	}
}
