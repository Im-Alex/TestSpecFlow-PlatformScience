using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSpecFlow.JsonStructure.Response
{
    public class PostResponse
    {
        public List<int> coords { get; set; }
        public int patches { get; set; }
    }
}
