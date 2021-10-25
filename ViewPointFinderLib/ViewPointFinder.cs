using System.Collections.Generic;
using System.Linq;

namespace ViewPointFinderLib
{
	public static class ViewPointFinder
	{
		public static Mesh mesh;

		public static void Initialize()
		{
			AddValueRefsToTheirElements();

			AddConnectedElementsRefsToNodes();

			AddHigherValueNeighbourRefsToElements();
		}

		private static void AddHigherValueNeighbourRefsToElements()
		{
			foreach (Element meshElement in mesh.elements)
			{
				var elementNeighbours = GetNeighboursOfElement(meshElement);
				meshElement.higherNeighbours = elementNeighbours
					.Where(neighbour => neighbour.valueRef.value > meshElement.valueRef.value).ToArray();
			}
		}

		private static void AddConnectedElementsRefsToNodes()
		{
			foreach (Element meshElement in mesh.elements)
			{
				foreach (int nodeID in meshElement.nodes)
				{
					var currentNodeConnectedElementsList = mesh.nodes[nodeID].connectedElements;
					if (!currentNodeConnectedElementsList.Contains(meshElement))
					{
						currentNodeConnectedElementsList.Add(meshElement);
					}
				}
			}
		}

		private static void AddValueRefsToTheirElements()
		{
			foreach (Value meshValue in mesh.values) { mesh.elements[meshValue.element_id].valueRef = meshValue; }
		}

		public static Value[] GetViewPointsSorted(int numberOfViewPointsToFind = 0)
		{
			var allViewPoints = GetAllViewPoints().ToArray();
			if (numberOfViewPointsToFind == 0 || numberOfViewPointsToFind > allViewPoints.Length)
			{
				numberOfViewPointsToFind = allViewPoints.Length;
			}

			return allViewPoints.Select(viewPoint => viewPoint.valueRef)
				.OrderByDescending(value => value.value).Take(numberOfViewPointsToFind).ToArray();
		}

		private static IEnumerable<Element> GetAllViewPoints()
		{
			return mesh.elements.Where(e => e.higherNeighbours.Length == 0);
		}

		private static IEnumerable<Element> GetNeighboursOfElement(Element element)
		{
			IEnumerable<Element> neighbourElements = System.Array.Empty<Element>();
			return element.nodes.Select(nodeID => mesh.nodes[nodeID])
				.Aggregate(neighbourElements, (current, node) => current.Concat(node.connectedElements)).Distinct();
		}
	}
}