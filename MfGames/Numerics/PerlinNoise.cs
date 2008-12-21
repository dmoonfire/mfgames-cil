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
	public class PerlinNoise
		: INoise2
	{
		#region Constructors
		/// <summary>
		/// Creates a Perlin generator with a random seed.
		/// </summary>
		public PerlinNoise()
			: this(Entropy.Next())
		{
		}

		/// <summary>
		/// Creates a Perlin generator with a given seed value.
		/// </summary>
		/// <param name="seed"></param>
		public PerlinNoise(int seed)
		{
			MersenneRandom random = new MersenneRandom(seed);
			Rank1 = random.Next(1000, 10000);
			Rank2 = random.Next(100000, 1000000);
			Rank3 = random.Next(1000000000, 2000000000);
		}
		#endregion

		#region Properties
		public int Rank1 {
			get;
			set;
		}
		public int Rank2 {
			get;
			set;
		}
		public int Rank3 {
			get;
			set;
		}
		#endregion

		#region Noise Generation
		/// <summary>
		/// Gets the noise for the given X and Y coordinate.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns></returns>
		public double GetNoise(int x, int y)
		{
			int n = x + y * 57;
			n = (n << 13) ^ n;

			return (1.0 - ((n * (n * n * Rank1 + Rank2) + Rank3) & 0x7fffffff) / 1073741824.0);
		}
		#endregion

#if TODO_REMOVED
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
#endif
	}
}
