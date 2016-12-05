using WordCloudCalculator.Contract;
using WordCloudCalculator.Contract.Visualization;
using WordCloudCalculator.Contract.Word;
using WordCloudCalculator.WordCloudCalculator;

namespace WordCloudCalculator.ExtractingWordCloudCalculator
{
	public class SimpleAppearenceCalculationMethod : IWordAppearenceCalculationMethod
	{
		public IWordCloudAppearenceArguments Arguments { get; set; }
		public double MaxWeight { get; set; }

		public double Top { get; set; }

		public VisualizedWord CalculateWordAppearence(IWeightedWord word, int itemIndex)
		{
			var fontSize = CalculateRelativeValue(Arguments.FontSizeRange, word.Weight);

			var size = Arguments.WordSizeCalculator(word.Text, fontSize);

			var visualizedTag = new VisualizedWord(word)
			{
				Position = new Position(Top, itemIndex * 10),
				Opacity = CalculateRelativeValue(Arguments.OpacityRange, word.Weight),
				FontSize = fontSize,
				Size = size
			};

			Top += size.Height;

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