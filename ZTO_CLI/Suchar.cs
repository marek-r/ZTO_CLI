using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ZTO_CLI
{
    /// <summary>
    /// Klasa Suchar odpowiadająca tabeli oraz właściwości automatyczne.
    /// </summary>
    /// <example>
    /// Przykład odpowiedzi z API.
    /// {
    //   "categories":[      
    //   ],
    //   "created_at":"2020-01-05 13:42:29.569033",
    //   "icon_url":"https://assets.chucknorris.host/img/avatar/chuck-norris.png",
    //   "id":"qJ9lipf0RFeWLTNGNbwCBg",
    //   "updated_at":"2020-01-05 13:42:29.569033",
    //   "url":"https://api.chucknorris.io/jokes/qJ9lipf0RFeWLTNGNbwCBg",
    //   "value":"The jokes are slacking, pick up the paste guys, or Chuck Norris will Virtually Roundhouse your asses!!"
    //}
    /// </example>
    internal class Suchar
    {
        public int SucharId { get; set; }
        public int PersonId { get; set; }

        public List<object> categories { get; set; }
        public string created_at { get; set; }
        public string icon_url { get; set; }
        public string id { get; set; }
        public string updated_at { get; set; }
        public string url { get; set; }
        public string value { get; set; }

    }
}
