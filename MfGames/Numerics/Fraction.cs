namespace MfGames.Numerics
{
	/// <summary>
	/// Represents a fractional number, keeping the individual components.
	/// </summary>
	public class Fraction
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Fraction"/> class.
		/// </summary>
		public Fraction()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Fraction"/> class.
		/// </summary>
		/// <param name="numerator">The numerator.</param>
		/// <param name="denominator">The denominator.</param>
		public Fraction(int numerator, int denominator)
		{
			Numerator = numerator;
			Denominator = denominator;
		}
		#endregion

		#region Properties
		private int numerator, denominator;

		/// <summary>
		/// Gets or sets the denominator of the fraction.
		/// </summary>
		/// <value>The denominator.</value>
		public int Denominator
		{
			get
			{
				return denominator;
			}
			set
			{
				denominator = value;
			}
		}

		/// <summary>
		/// Gets the mixed fraction denominator.
		/// </summary>
		/// <value>The mixed denominator.</value>
		public int MixedDenominator
		{
			get
			{
				return denominator;
			}
		}

		/// <summary>
		/// Gets the mixed fraction numerator.
		/// </summary>
		/// <value>The mixed numerator.</value>
		public int MixedNumerator
		{
			get
			{
				return numerator % denominator;
			}
		}

		/// <summary>
		/// Gets the mixed fraction whole.
		/// </summary>
		/// <value>The mixed whole.</value>
		public int MixedWhole
		{
			get
			{
				return numerator / denominator;
			}
		}

		/// <summary>
		/// Gets or sets the numerator of the fraction.
		/// </summary>
		/// <value>The numerator.</value>
		public int Numerator
		{
			get
			{
				return numerator;
			}
			set
			{
				numerator = value;
			}
		}

		/// <summary>
		/// Gets the double value of this fraction.
		/// </summary>
		/// <value>The value.</value>
		public double Value
		{
			get
			{
				return ((double) numerator) / ((double) denominator);
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Simplifies this fraction instance and returns a new fraction.
		/// </summary>
		public Fraction Simplify()
		{
			int gcf = Math.GreatestCommonFactor(numerator, denominator);

			return new Fraction(numerator / gcf, denominator / gcf);
		}
		#endregion
	}
}
