using System;
using System.Collections;
using System.Collections.Generic;
using WordCloudCalculator.Contract.Word;

namespace WordCloudCalculator.WordCloudCalculator
{
	/// <summary>
	/// Defines an Implementation of a calculator
	/// </summary>
	public interface IWordCloudCalculator
	{
		/// <summary>
		/// Main calculation method
		/// </summary>
		/// <param name="args"></param>
		/// <param name="wordSource"></param>
		/// <returns></returns>
		VisualizedWord[] Calculate(IWordCloudAppearenceArguments args, IEnumerable<IWeightedWord> wordSource);
	}
}