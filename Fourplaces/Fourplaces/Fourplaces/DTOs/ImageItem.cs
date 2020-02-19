using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fourplaces.DTOs
{
    public class ImageItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
