using System.IO;
using System.Text.Json;

namespace ViewPointFinderLib
{
	public static class JSONHandler
	{
		public static bool DeserializeMeshFromJSONFile(string filePath, ref Mesh mesh)
		{
			if (File.Exists(filePath))
			{
				string jsonInput = File.ReadAllText(filePath);
				mesh = JsonSerializer.Deserialize<Mesh>(jsonInput);
				if (mesh != null)
				{
					return true;
				}
			}

			return false;
		}

		public static string ViewPointsToJSON(Value[] viewPoints, bool indent = true)
		{
			JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = indent };
			return JsonSerializer.Serialize(viewPoints, options);
		}

		public static void WriteViewPointsToJSONFile(string fileName, Value[] viewPoints)
		{
			File.WriteAllText($"{fileName}.json", ViewPointsToJSON(viewPoints));
		}
	}
}