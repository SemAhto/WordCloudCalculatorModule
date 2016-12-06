using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WordCloudCalculator.Contract;
using WordCloudCalculator.WordCloudCalculator;
using WordCloudCalculator.WPF;

namespace GuiTest
{
	class TestAppeareanceArgs : IWordCloudAppearenceArguments
	{
		public TestAppeareanceArgs()
		{
			WordSizeCalculator = WordSizeCalculatorFactory.FormattedTextWordSizeCalculator();
		}
		public Size PanelSize { get; set; }
		public Range FontSizeRange { get; set; } = new Range(9, 40);
		public Range OpacityRange { get; set; } = new Range(0.4, 1);
		public Func<string, double, Size> WordSizeCalculator { get; set; }
	}
}
