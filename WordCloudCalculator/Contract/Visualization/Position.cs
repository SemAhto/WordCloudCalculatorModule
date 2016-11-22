namespace WordCloudCalculator.Contract.Visualization
{
	/// <summary>
	/// Defines an absolute place on screen
	/// </summary>
	public struct Position
	{
		/// <summary>
		/// Contructor
		/// </summary>
		/// <param name="top">Distance to top bound</param>
		/// <param name="left">Distance to left bound</param>
		public Position(double top, double left)
		{
			Top = top;
			Left = left;
		}

		/// <summary>
		/// Distance to top bound
		/// </summary>
		public double Top { get; set; }

		/// <summary>
		/// Distance to left bound
		/// </summary>
		public double Left { get; set; }

		public override string ToString()
		{
			return $" Left = {Left:N1}, Top = {Top:N1}";
		}
	}
}
