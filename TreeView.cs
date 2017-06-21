using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Velvet.Helpers
{
    public class TreeView
    {
        private List<TreeViewCommand> commands;

        public bool Selectable { get; set; }
        public string SelectionId { get; set; }

        public TreeView()
        {
            commands = new List<TreeViewCommand>();

            Selectable = false;
            SelectionId = "SelectedIds";
        }

        public void AddCommand(string name, Func<dynamic, object> format = null)
        {
            var command = new TreeViewCommand()
            {
                Name = name,
                Format = format
            };
            commands.Add(command);
        }

        public IHtmlString GetHtml(string name, dynamic model)
        {
            var htmlResult = string.Empty;

            var items = model as List<TreeViewItem>;
            if (items != null)
            {
                htmlResult += "<div class=\"treeview\" id=\"" + name + "\">";
                htmlResult += "<ul>";

                htmlResult += PopulateTreeViewItems(name, items, true);

                htmlResult += "</ul>";
                htmlResult += "</div>";
            }

            return new HtmlString(htmlResult);
        }

        private string PopulateTreeViewItems(string name, List<TreeViewItem> items, bool root = false)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            var htmlResult = string.Empty;

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                htmlResult += "<li class=\"treeview-item";

                if (!root)
                {
                    if (i != items.Count - 1)
                        htmlResult += " treeview-item-with-line";
                    else
                        htmlResult += " treeview-item-last-with-line";
                }

                htmlResult += "\" id=\"" + name + "_item" + item.Id.ToString() + "\"";
                if (item.Expanded)
                    htmlResult += "expanded=\"true\"";
                htmlResult += ">";

                    htmlResult += "<span>";
                    if (item.Items != null && item.Items.Count > 0)
                    {
                        if (item.Expanded)
                            htmlResult += "<span class=\"glyphicon glyphicon-folder-open\"></span>";
                        else
                            htmlResult += "<span class=\"glyphicon glyphicon-folder-close\"></span>";
                    }
                    else
                        htmlResult += "<span class=\"glyphicon glyphicon-file\"></span>";

                htmlResult += "<span class=\"treeview-item-title\">";

                if (Selectable)
                {
                    htmlResult += "<input type=\"checkbox\" name=\"" + SelectionId + "\" id=\"" + SelectionId + "\"";
                    if (item.Selected)
                        htmlResult += " checked=\"checked\"";
                    htmlResult += " value=\"" + item.Id.ToString() + "\"";
                    htmlResult += " />";
                }

                htmlResult += item.Title + " </span>";

                foreach (var command in commands)
                    htmlResult += command.Format(item) + "&nbsp;";

                htmlResult += "</span>";

                if (item.Items != null && item.Items.Count > 0)
                    htmlResult += "<ul>" + PopulateTreeViewItems(name, item.Items) + "</ul>";

                htmlResult += "</li>";
            }

            return htmlResult;
        }
    }
}