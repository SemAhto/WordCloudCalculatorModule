using WordCloudCalculator.Contract.Word;
using WordCloudCalculator.WordCloudCalculator;

namespace WordCloudCalculator.ExtractingWordCloudCalculator
{
	/// <summary>
	/// Defines a method which calculates the appearence of an given IWeightedWord 
	/// </summary>
	public interface IWordAppearenceCalculationMethod
	{
		IWordCloudAppearenceArguments Arguments { get; set; }
		double MaxWeight { get; set; }
		VisualizedWord CalculateWordAppearence(IWeightedWord word, int itemIndex);
		bool CanAddWords { get; }
	}
}