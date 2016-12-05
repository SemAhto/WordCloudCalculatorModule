using System;
using System.Windows;
using WordCloudCalculator.Contract;

namespace WordCloudCalculator.WordCloudCalculator
{
	/// <summary>
	/// Default Implementation of Appearence Arguments
	/// </summary>
	public class WordCloudAppearenceArguments : IWordCloudAppearenceArguments
	{
		public Size PanelSize { get; set; }
		public Range FontSizeRange { get; set; }
		public Range OpacityRange { get; set; }
		public Func<string, double, Size> WordSizeCalculator { get; set; }
	}
}
