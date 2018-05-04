using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SyncurrWPF.Helpers.Controls
{
	public class AutoGrid : Grid
	{
		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			RowDefinitions.Clear();

			int cols = Math.Max(1, ColumnDefinitions.Count);

			int index = 0;
			for (int i = 0; i < Children.Count; i++)
			{
				if (index % cols == 0)
				{
					RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
				}
				UIElement child = Children[i];
				SetRow(child, RowDefinitions.Count - 1);
				SetColumn(child, index - ((RowDefinitions.Count - 1) * cols));
				index += GetColumnSpan(child);
			}
		}
	}
}
