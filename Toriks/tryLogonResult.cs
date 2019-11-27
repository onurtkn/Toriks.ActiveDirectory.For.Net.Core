using System;
using System.Collections.Generic;
using System.Text;

namespace Toriks
{
    public class tryLogonResult
    {
        public string Exception { get; set; }
        public bool isSuccess  { get; set; }

        public Dictionary<string,string> propertyList { get; set; }

    }
}
