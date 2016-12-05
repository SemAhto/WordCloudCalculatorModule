using System.Windows;
using WordCloudCalculator.Contract;
using WordCloudCalculator.Contract.Word;
using WordCloudCalculator.WordCloudCalculator;

namespace WordCloudCalculator.ExtractingWordCloudCalculator
{
	public class SimpleAppearenceCalculationMethod : IWordAppearenceCalculationMethod
	{
		public IWordCloudAppearenceArguments Arguments { get; set; }
		public double MaxWeight { get; set; }

		public VisualizedWord CalculateWordAppearence(IWeightedWord word, int itemIndex)
		{

			var size = Arguments.WordSizeCalculator(word.Text, CalculateRelativeValue(Arguments.FontSizeRange, word.Weight));

			var visualizedTag = new VisualizedWord(word)
			{
				Position = new Point(itemIndex, itemIndex),
				Opacity = CalculateRelativeValue(Arguments.OpacityRange, word.Weight)
			};

			if (itemIndex == 4)
			{
				CanAddWords = false;
			}

			return visualizedTag;
		}

		public bool CanAddWords { get; private set; } = true;
	
		private double CalculateRelativeValue(Range range, double current)
		{
			return range.CalculateRelativeValue(new Range(0, MaxWeight), current);
		}
	}
}