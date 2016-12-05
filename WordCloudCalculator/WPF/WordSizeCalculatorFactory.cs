using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WordCloudCalculator.WPF
{
	public static class WordSizeCalculatorFactory
	{
		public static Func<string, double, Size> FormattedTextWordSizeCalculator(string fontFamily = "Segoe UI", int border = 1, int margin = 0, int padding = 5)
		{
			var sizeExtension = 2 *(border + margin + padding);
			return (s, d) =>
			{
				var formattedText = new FormattedText(s, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
					new Typeface(fontFamily), d, Brushes.Black);
				return new Size(formattedText.Width + sizeExtension, formattedText.Height + sizeExtension);
			};
		}
	}
}
