using WordCloudCalculator.Contract.Visualization;

namespace WordCloudCalculator.Contract.Word
{
	/// <summary>
	/// Defines an element of the result
	/// </summary>
	public class VisualizedWord: IWord
	{
		public VisualizedWord() { }

		public VisualizedWord(IWord word)
		{
			Text = word.Text;
		}

		/// <summary>
		/// The Word itself
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Defines how big it should be displayed (in pt)
		/// </summary>
		public double FontSize { get; set; }

		/// <summary>
		/// Defines where it has to be displayed on screen
		/// </summary>
		public Position Position { get; set; }

		/// <summary>
		/// Defines how strong the word should be displayed [0..1]
		/// </summary>
		public double Opacity { get; set; }
        public Size Size { get; set; }
		public override string ToString() => $"{Text}, Position({Position}), Size({Size}), FontSize({FontSize}) Opacity = {Opacity:N1}";

    }
}