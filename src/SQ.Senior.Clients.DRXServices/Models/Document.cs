using System.Collections.Generic;

namespace SQ.Senior.Clients.DrxServices.Models {
    public class Document {
        public List<Type> Types { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string LinkName { get; set; }
    }
}
