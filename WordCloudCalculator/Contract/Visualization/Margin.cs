namespace WordCloudCalculator.Contract.Visualization
{
	/// <summary>
	/// Defines space metrics around an object
	/// </summary>
	public struct Margin
	{
		/// <summary>
		/// No space around
		/// </summary>
		public static Margin None => new Margin(0,0,0,0);

		public Margin(double left, double top, double right, double bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		/// <summary>
		/// Space on left side
		/// </summary>
		public double Left { get; set; }

		/// <summary>
		/// Space on top side
		/// </summary>
		public double Top { get; set; }

		/// <summary>
		/// Space on right side
		/// </summary>
		public double Right { get; set; }

		/// <summary>
		/// Space on bottom side
		/// </summary>
		public double Bottom { get; set; }

		/// <summary>
		/// Calculates how much bigger the objects width is
		/// </summary>
		public double TotalWidthEnlargement => Left + Right;

		/// <summary>
		/// Calculates how much bigger the objects height is
		/// </summary>
		public double TotalHeightEnlargement => Top + Bottom;
	}
}