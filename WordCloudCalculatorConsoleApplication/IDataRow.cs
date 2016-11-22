using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCloudCalculator.Contract.Word;

namespace WordCloudCalculatorConsoleApplication
{
	public interface IDataRow
	{
		double Weight { get; set; }
		string Text { get; set; }
	}
}
