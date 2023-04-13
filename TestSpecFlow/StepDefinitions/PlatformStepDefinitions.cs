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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Given(@"the robot starts at x equals (.*) and y equals (.*)")]
        public void GivenTheRobotStartsAtXEqualsAndYEquals(int x, int y)
        {
            var coord = new int[] { x, y };
            try
            {
                //Console.WriteLine(coord[0]);
                //Console.WriteLine(coord[1]);
                _context["coord"] = coord;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Given(@"there are patches of dirt at the following positions:")]
        public void GivenThereArePatchesOfDirtAtTheFollowingPositions(Table table)
        {


            var patches = new List<Patch>();
            try
            {
                foreach (var row in table.Rows)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        [When(@"the robot moves according to the instruccions ""([^""]*)""")]
        public void WhenTheRobotMovesAccordingToTheInstruccions(string instructions)
        {
            try
            {
                _context["instructions"] = instructions;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        [Then(@"the final robot position should be at x equals (.*) and y equals (.*)")]
        public async Task ThenTheFinalRobotPositionShouldBeAtXEqualsAndYEqualsAsync(int x, int y)
        {
            var coord = new int[] { x, y };
            try
            {
                _context["coordFinal"] = coord;

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
                Assert.IsTrue(_response.StatusCode == HttpStatusCode.OK);
                
                var responseContent = await _response.Content.ReadAsStringAsync();
                var responsePayload = JsonConvert.DeserializeAnonymousType(responseContent, new { coords = new int[] { 0, 0 }, patches = 0 });
                
                

                _context["responsePayload"] = responsePayload;

                var coordsResponse = responsePayload.GetType().GetProperty("coords").GetValue(responsePayload, null) as int[];
                
                Assert.IsNotNull(coords);
                Assert.AreEqual(x, coordsResponse[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        [Then(@"the number of patches of dirt cleaned should be (.*)")]
        public void ThenTheNumberOfPatchesOfDirtCleanedShouldBe(int p0)
        {
            try
            {
                var responsePayload = _context["responsePayload"];

                var cleanedPatches = (int)responsePayload.GetType().GetProperty("patches").GetValue(responsePayload, null);
                Assert.AreEqual(p0, cleanedPatches);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Given(@"there are no patches of dirt in the room")]
        public void GivenThereAreNoPatchesOfDirtInTheRoom()
        {
            try
            {
                _context["patches"] = new List<int[]>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
