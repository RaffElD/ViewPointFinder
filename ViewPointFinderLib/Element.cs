namespace ViewPointFinderLib
{
	public class Element
	{
		public int   id    { get; set; }
		public int[] nodes { get; set; }

		public Value     valueRef;
		public Element[] higherNeighbours;
	}
}