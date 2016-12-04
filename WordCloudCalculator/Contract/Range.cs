using System;

namespace WordCloudCalculator.Contract
{
    /// <summary>
    /// Defines Boundaries and provides some related calculation methods
    /// </summary>
    public struct Range
    {
        private double _min;
        private double _max;
        //private double _step ;

        public Range(double min, double max /*, step = 1.0*/ )
        {
            //_step = 1.0;
            //_max = Convert.ToInt32(max / _step) * _step;
            //_min = Convert.ToInt32(min / _step) * _step;
            _max = max;
            _min = min;
            if (min > max) throw new Exception("Invalid Range!");
        }
        //private double InStep(double x, double s) => { return Convert.ToInt32(x / s) * s };
        /// <summary>
        /// Lower Bound
        /// </summary>
        public double Min
		{
			get { return _min; }
			set
			{
				if(value > Max) throw new Exception("Minimum cannot be greater than Maximum!");
				_min = value;
			}
		}

		/// <summary>
		/// Upper Bound
		/// </summary>
		public double Max
		{
			get { return _max; }
			set
			{
				if (!IsValid) throw new Exception("Maximum cannot be lesser than Minimum!");
				_max = value;
			}
		}

		public double Amplitude => Max - Min;
		
		/// <summary>
		/// Checks if an value is in range bounds
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Includes(double value) => Min <= value && value <= Max;

        /// <summary>
        /// Checks if bound relation is correct
        /// </summary>
        public bool IsValid => Max >= Min;

		/// <summary>
		/// Calculates the relative range value of a given value of an other Range
		/// </summary>
		/// <param name="valueRange">source range</param>
		/// <param name="currentValue">Value of source range which realtive value have to be calculated</param>
		/// <returns>Relative Range Value</returns>
		public double CalculateRelativeValue(Range valueRange, double currentValue)
		{
			return Min + (currentValue - valueRange.Min) * Amplitude / valueRange.Amplitude;
		}
	}
}