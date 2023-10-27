using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AiportGapApiTests.Data
{
    public class InstructionClass
    {
        [JsonProperty("data")]
        public DataCollection? Data { get; set; }
    }
}
