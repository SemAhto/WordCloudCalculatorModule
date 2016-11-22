namespace WordCloudCalculator.Contract.Word
{
	/// <summary>
	/// Defines an element of the source of claculation
	/// </summary>
	public interface IWeightedWord : IWord
	{
		/// <summary>
		/// Defines the importance in the cloud
		/// </summary>
		double Weight { get; set; }
	}
}
