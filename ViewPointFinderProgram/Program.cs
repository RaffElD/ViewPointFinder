using System;
using ViewPointFinderLib;

namespace ViewPointFinderProgram
{
	class Program
	{
		static void Main(string[] args)
		{
			string inputPath = ParseArguments(args, out int viewPointCountToFind);

			if (JSONHandler.DeserializeMeshFromJSONFile(inputPath, ref ViewPointFinder.mesh))
			{
				ViewPointFinder.Initialize();

				Value[] viewPoints = ViewPointFinder.GetViewPointsSorted(viewPointCountToFind);

				var viewPointsJSON = JSONHandler.ViewPointsToJSON(viewPoints, indent: true);
				Console.WriteLine(viewPointsJSON);
			}
			else
			{
				Console.WriteLine($"Couldn't deserialize mesh from file {inputPath}");
			}
			
			Console.ReadKey();
		}

		private static string ParseArguments(string[] args, out int viewPointCountToFind)
		{
			string inputPath;
			if (args.Length > 0) inputPath = args[0];
			else
			{
				Console.WriteLine("Input mesh JSON filepath:");
				inputPath = Console.ReadLine();
			}

			viewPointCountToFind = 0;
			if (args.Length > 1)
			{
				if (int.TryParse(args[1], out int val)) { viewPointCountToFind = val; }
			}

			if (viewPointCountToFind == 0)
			{
				Console.WriteLine("Input number of viewpoints to find:");
				viewPointCountToFind = int.TryParse(Console.ReadLine(), out int val) ? val : 3;
			}

			return inputPath;
		}
	}
}