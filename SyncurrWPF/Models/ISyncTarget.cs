using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncurrWPF.Models
{
	public interface ISyncTarget
	{
		Task Sync();
	}
}
