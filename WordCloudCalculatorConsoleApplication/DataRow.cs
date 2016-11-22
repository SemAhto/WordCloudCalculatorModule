using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCloudCalculatorConsoleApplication
{
	class DataRow : IDataRow
	{
		public double Weight { get; set; }
		public string Text { get; set; }
	}
}
