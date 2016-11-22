using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCloudCalculator.Contract.Word
{
	/// <summary>
	/// Default implementation of the source element
	/// </summary>
	public class WeightedWord : IWeightedWord
	{
		public string Text { get; set; }
		public double Weight { get; set; }

		public override string ToString()
		{
			return $"{Text} Weight: {Weight:N1}";
		}
	}
}
