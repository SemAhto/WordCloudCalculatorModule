using System;
using WordCloudCalculator.Contract;
using WordCloudCalculator.Contract.Visualization;

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
		public Margin WordMargin { get; set; }
		public Func<string, double, Size> WordSizeCalculator { get; set; }
	}
}
