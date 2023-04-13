using System;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using TestSpecFlow.JsonStructure.Post;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net;

namespace TestSpecFlow.StepDefinitions
{
    [Binding]
    public class PlatformStepDefinitions
    {
       

        private readonly ScenarioContext _context;
        private HttpClient _client;
        private HttpResponseMessage _response;
       
        

        public PlatformStepDefinitions(ScenarioContext context) 
        { 
            _context = context;
            _client = new HttpClient();
        }
        [Given(@"The room size is (.*) by (.*)")]
        public void GivenTheRoomSizeIsBy(int x, int y)
        {
            var roomSize = new int[] { x, y };
            try
            {
               
                _context["roomSize"] = roomSize;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        [Given(@"the robot starts at x equals (.*) and y equals (.*)")]
        public void GivenTheRobotStartsAtXEqualsAndYEquals(int x, int y)
        {
            var coord = new int[] { x, y };
            _context["coord"] = coord;
        }

        [Given(@"there are patches of dirt at the following positions:")]
        public void GivenThereArePatchesOfDirtAtTheFollowingPositions(Table table)
        {


            var patches = new List<Patch>();

            foreach(var row in table.Rows)
            {
                var x = int.Parse(row["X"]);
                var y = int.Parse(row["Y"]);
                patches.Add(new Patch(x, y));
            }

            var patchList = new PatchList
            {
                Patches = patches.GroupBy(patch => patch.Y)
        .Select(group => group.Select(patch => new List<int> { patch.X, patch.Y }).ToList())
        .SelectMany(x => x)
        .ToList()
            };
            var patchCoordinates = patchList.Patches.Select(p => new int[] { p[0], p[1] }).ToList();

            _context["patches"] = patchCoordinates;
            
        }

        [When(@"the robot moves according to the instruccions ""([^""]*)""")]
        public void WhenTheRobotMovesAccordingToTheInstruccions(string instructions)
        {
            _context["instructions"] = instructions;
        }



        [Then(@"the final robot position should be at x equals (.*) and y equals (.*)")]
        public async Task ThenTheFinalRobotPositionShouldBeAtXEqualsAndYEqualsAsync(int x, int y)
        {
            
            var coord = new int[] { x, y };
            _context["coord"] = coord;

            var roomSize = _context["roomSize"] as int[];
            var coords = _context["coord"] as int[];
            var patches = _context["patches"] as List<int[]>;
            var instructions = _context["instructions"] as string;

            var payLoad = new
            {
                roomSize = roomSize,
                coords = coords,
                patches = patches,
                instructions = instructions
            };
            var json = JsonConvert.SerializeObject(payLoad);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _response = await _client.PostAsync("http://localhost:8080/v1/cleaning-sessions", content);
            // STATUS 200
            Console.WriteLine("\n" + _response.StatusCode);
            Assert.IsTrue(_response.StatusCode == HttpStatusCode.OK);
            //Console.WriteLine("\n" + content);
            var responseContent = await _response.Content.ReadAsStringAsync();
            var responsePayload = JsonConvert.DeserializeAnonymousType(responseContent, new { coords = new int[] { 0, 0 }, patches = 0 });
            //Console.WriteLine("\n   RESPONDE   " );
            //Console.WriteLine("\n" + responseContent);

            _context["responsePayload"] = responsePayload;

            var coordsResponse = responsePayload.GetType().GetProperty("coords").GetValue(responsePayload, null) as int[];
            //Console.WriteLine("\n" + coordsResponse);
            Console.WriteLine("\n" + x);
            Console.WriteLine("\n" + x);
            Assert.IsNotNull(coords);
            Assert.AreEqual(x, coordsResponse[0]);
            Assert.AreEqual(y, coordsResponse[1]);
        }
        [Then(@"the number of patches of dirt cleaned should be (.*)")]
        public void ThenTheNumberOfPatchesOfDirtCleanedShouldBe(int p0)
        {
            var responsePayload = _context["responsePayload"];

            var cleanedPatches = (int)responsePayload.GetType().GetProperty("patches").GetValue(responsePayload, null);
            Console.WriteLine("\n" + cleanedPatches);
            Assert.AreEqual(p0, cleanedPatches);
        }

        [Given(@"there are no patches of dirt in the room")]
        public void GivenThereAreNoPatchesOfDirtInTheRoom()
        {
            _context["patches"] = new List<int[]> { new int[] { } };
        }


    }
}
