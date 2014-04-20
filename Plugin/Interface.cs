using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CycleProcessControl.Pattern.Model;
namespace Plugin
{
	public interface IDBManager
    {
		void Save(StaticPatternModel SaveData);

    }
}
