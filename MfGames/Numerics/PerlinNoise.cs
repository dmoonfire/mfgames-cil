using System;
using System.Xml;
using MfGames.Utility;

namespace MfGames.Numerics
{
	/// <summary>
	/// Implements a basic controllable Perlin noise generator.
	/// 
	/// Stolen from http://www.sepcot.com/blog/2006/08/PDN-PerlinNoise2d
	/// </summary>
	public class PerlinNoiseGenerator
	{
		#region Constructors
		/// <summary>
		/// Creates a Perlin generator with a random seed.
		/// </summary>
		public PerlinNoiseGenerator()
			: this(Entropy.Next())
		{
		}

		/// <summary>
		/// Creates a Perlin generator with a given seed value.
		/// </summary>
		/// <param name="seed"></param>
		public PerlinNoiseGenerator(int seed)
		{
			Seed = seed;
			Frequency = 0.015;
			Persistence = 0.65;
			Octaves = 8;
			Amplitude = 1;
			Density = 1;
			Coverage = 0;
		}
		#endregion

		#region Properties
		private int seed;

		public double Amplitude { get; set; }

		public double Coverage { get; set; }

		public double Density { get; set; }

		public double Frequency { get; set; }

		public int Octaves { get; set; }

		public double Persistence { get; set; }

		public int Rank1 { get; set; }
		public int Rank2 { get; set; }
		public int Rank3 { get; set; }

		/// <summary>
		/// Gets or sets the seed value for this generator. This seed is
		/// a starting point for a MersenneRandom and setting it will
		/// set the Rank1, Rank2, Rank3 variables with a random value.
		/// </summary>
		public int Seed
		{
			get { return seed; }
			set
			{
				// Set the seed value
				seed = value;

				// Create random rank values
				MersenneRandom random = new MersenneRandom(seed);
				Rank1 = random.Next(1000, 10000);
				Rank2 = random.Next(100000, 1000000);
				Rank3 = random.Next(1000000000, 2000000000);
			}
		}
		#endregion

		#region Noise Generation
		private double GenerateNoise(int x, int y)
		{
			int n = x + y * 57;
			n = (n << 13) ^ n;

			return (1.0 - ((n * (n * n * Rank1 + Rank2) + Rank3) & 0x7fffffff) / 1073741824.0);
		}

		/// <summary>
		/// Creates a noise value between 0 and 1 for a given X and Y coordinate.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public double GetNoise(int x, int y)
		{
			// Build up a running total and keep the frequency and amplitude
			// since those will be changing with octavtes.
			double total = 0;
			double dx = x;
			double dy = y;
			double frequency = Frequency;
			double amplitude = Amplitude;

			// Process each octave
			for (int octave = 0; octave < Octaves; octave++)
			{
				// Calculate the total for this octave
				total += Smooth(dx * frequency, dy * frequency) * amplitude;
				frequency *= 2;
				amplitude *= Persistence;
			}

			// Adjust for coverage and density
			total = (total + Coverage) * Density;

			// Clamp the values from -1 to 1
			total = System.Math.Max(-1, System.Math.Min(1, total));

			// Return the results
			return total;
		}

		/// <summary>
		/// Uses cosine interplotion between two values with an angle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="a"></param>
		/// <returns></returns>
		private double Interpolate(double x, double y, double a)
		{
			double val = (1 - System.Math.Cos(a * System.Math.PI)) * 0.5;
			return x * (1 - val) + y * val;
		}

		/// <summary>
		/// Smooths out the points around a given X and Y.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private double Smooth(double x, double y)
		{
			// Cast to make things easier
			int ix = (int) x;
			int iy = (int) y;

			// Get the four square points
			double n1 = GenerateNoise(ix, iy);
			double n2 = GenerateNoise(ix + 1, iy);
			double n3 = GenerateNoise(ix, iy + 1);
			double n4 = GenerateNoise(ix + 1, iy + 1);

			// Interpolate the values
			double i1 = Interpolate(n1, n2, x - ix);
			double i2 = Interpolate(n3, n4, x - ix);

			// Interpolate the final numbers
			return Interpolate(i1, i2, y - iy);
		}
		#endregion

		#region XML I/O
		/// <summary>
		/// Reads in a perlin noise generator's settings from the given stream, using tagName
		/// as the enclosing tag.
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="tagName"></param>
		public void Read(XmlReader xml, string tagName)
		{
			// Make sure we have the right tag
			if (xml.LocalName != tagName)
				throw new Exception("Expected tag " + tagName + " but got " + xml.LocalName);

			// Read in the properties
			Rank1 = XmlConvert.ToInt32(xml["r1"]);
			Rank2 = XmlConvert.ToInt32(xml["r2"]);
			Rank3 = XmlConvert.ToInt32(xml["r3"]);
			Frequency = XmlConvert.ToDouble(xml["f"]);
			Persistence = XmlConvert.ToDouble(xml["p"]);
			Octaves = XmlConvert.ToInt32(xml["o"]);
			Amplitude = XmlConvert.ToDouble(xml["a"]);
			Density = XmlConvert.ToDouble(xml["d"]);
			Coverage = XmlConvert.ToDouble(xml["c"]);
		}

		/// <summary>
		/// Writes out the settings of the perlin noise generator to the given XML stream.
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="tagName"></param>
		public void Write(XmlWriter xml, string tagName)
		{
			if (tagName != null)
				xml.WriteStartElement(tagName);

			xml.WriteAttributeString("r1", XmlConvert.ToString(Rank1));
			xml.WriteAttributeString("r2", XmlConvert.ToString(Rank2));
			xml.WriteAttributeString("r3", XmlConvert.ToString(Rank3));
			xml.WriteAttributeString("f", XmlConvert.ToString(Frequency));
			xml.WriteAttributeString("p", XmlConvert.ToString(Persistence));
			xml.WriteAttributeString("o", XmlConvert.ToString(Octaves));
			xml.WriteAttributeString("a", XmlConvert.ToString(Amplitude));
			xml.WriteAttributeString("d", XmlConvert.ToString(Density));
			xml.WriteAttributeString("c", XmlConvert.ToString(Coverage));

			if (tagName != null)
				xml.WriteEndElement();
		}
		#endregion
	}
}
