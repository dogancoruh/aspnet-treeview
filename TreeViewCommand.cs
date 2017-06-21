using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Velvet.Helpers
{
    public class TreeViewCommand
    {
        public string Name { get; set; }
        public Func<dynamic, object> Format { get; set; }
    }
}