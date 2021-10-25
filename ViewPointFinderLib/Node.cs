using System.Collections.Generic;

namespace ViewPointFinderLib
{
	public class Node
	{
		public int   id { get; set; }
		public float x  { get; set; }
		public float y  { get; set; }

		public readonly List<Element> connectedElements = new List<Element>();
	}
}