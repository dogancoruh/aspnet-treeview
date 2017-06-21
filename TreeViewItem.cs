using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Velvet.Controllers
{
    public class TreeViewItem
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public List<TreeViewItem> Items { get; set; }

        public bool Expanded { get; set; }
        public bool Selected { get; set; }
    }
}