using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja1
{
    public class Person
    {
        [JsonProperty(PropertyName = "imię")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "nazwisko")]
        public string Surname { get; set; }

        [JsonProperty(PropertyName = "zawód")]
        public string Profession { get; set; }

        [JsonProperty(PropertyName = "wiek")]
        public int Age { get; set; }

        [JsonProperty(PropertyName = "miejsce-urodzenia")]
        public string PlaceOfBirth { get; set; }
    }
}
