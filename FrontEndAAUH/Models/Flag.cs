using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndAAUH.Model {
    public class Flag {
        public string id { get; set; }
        public string flagName { get; set; }
        public string flagDescription { get; set; }
        public bool flagRaised { get; set; }
        public string alertLevel { get; set; }

        public Flag() { }
        public Flag(string id, string flagName, string flagDescription,  bool flagRaised) {
            this.id = id;
            this.flagName = flagName;
            this.flagDescription = flagDescription;
            this.flagRaised = flagRaised;
            this.alertLevel = "-1";
        }
    }
}
