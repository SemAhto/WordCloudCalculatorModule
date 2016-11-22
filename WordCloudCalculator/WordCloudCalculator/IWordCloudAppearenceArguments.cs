using System;
using WordCloudCalculator.Contract;
using WordCloudCalculator.Contract.Visualization;

namespace WordCloudCalculator.WordCloudCalculator
{
	/// <summary>
	/// Defines Appearenche Parameters
	/// </summary>
	public interface IWordCloudAppearenceArguments
	{
		/// <summary>
		/// Defines the available space for the word cloud
		/// </summary>
		Size PanelSize { get; set; }

		/// <summary>
		/// Defines the smalles and the biggest size of a word in the cloud [0..infinity]
		/// </summary>
		Range FontSizeRange { get; set; }

		/// <summary>
		/// Defines the lightest and the strongest apperence of a word in the cloud [0..1]
		/// </summary>
		Range OpacityRange { get; set; }

		/// <summary>
		/// Defines the minimum space around a single word
		/// </summary>
		Margin WordMargin { get; set; }

		/// <summary>
		/// Provides a method to calculate the size on screen by given content and desired fontsize
		/// </summary>
		Func<string, double, Size> WordSizeCalculator { get; set; }
	}
}