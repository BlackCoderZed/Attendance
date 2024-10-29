using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRAttendance.Models.Common
{
    public class GridviewJsonResponse : JsonResponse
    {
        [JsonProperty("draw")]
        public int draw { get; set; }

        [JsonProperty("recordsTotal")]
        public int recordsTotal { get; set; }

        [JsonProperty("recordsFiltered")]
        public int recordsFiltered { get; set; }

        [JsonProperty("data")]
        public IEnumerable<Object> data { get; set; }
    }
}