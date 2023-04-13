

using Newtonsoft.Json;

namespace TestSpecFlow.JsonStructure.Post
{
    public class PostRobot
    {
        [JsonProperty("roomSize")]
        public List<int> roomSize { get; set; }
        [JsonProperty("coords")]
        public List<int> coords { get; set; }
        [JsonProperty("patches")]
        public List<List<int>> patches { get; set; }
        [JsonProperty("instructions")]
        public string instructions { get; set; }
    }
    public class Patch
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Patch(int x, int y)
        {
            X = x;
            Y = y;
        }

    }

    public class PatchList
    {
        public List<List<int>> Patches { get; set; }

        public PatchList()
        {
            Patches = new List<List<int>>();
        }
    }


    public class RobotInput
    {
        
    public int[] Position { get; set; }
    public string Instructions { get; set; }
    public PatchList Patches { get; set; }
    }
    public class RobotOutput
    {
        public int[] FinalPosition { get; set; }
        public int CleanedPatches { get; set; }
    }
}
