# aspnet_treeview
TreeView helper

**Example usage to populate data for treeview**

```csharp
private List<TreeViewItem> GetAssetCategoryHierarchy(VFabrikaEntities dbContext, int? categoryId = null)
{
    var items = new List<TreeViewItem>();

    var assetCategories = (from table in dbContext.AssetCategories
                           where (categoryId == null && (table.CategoryId == null || table.CategoryId == 0)) ||
                                 (categoryId != null && table.CategoryId == categoryId)
                           select table).ToList();

    foreach (var assetCategory in assetCategories)
    {
        var item = new TreeViewItem()
        {
            Id = assetCategory.Id,
            Title = assetCategory.Name,
            Items = GetAssetCategoryHierarchy(dbContext, assetCategory.Id)
        };
        items.Add(item);
    }

    return items;
}
```
**Razor syntax**

```csharp
@{ 
    var treeView = new TreeView()
    {
        Selectable = true
    };
}

@using (Html.BeginForm("CategoriesOfAsset", "ControlPanel", FormMethod.Post))
{
    @treeView.GetHtml("treeView1", Model.AssetCategoryHierarchy)

    <script>
            function treeViewCollapseExpand(nodeId) {
                var $node = $("#" + nodeId);
                var $nodeItems = $($node.find(".treeview-node-items")[0]);
                if ($node.attr("expanded") != "true") {
                    $node.attr("expanded", "true");
                    $nodeItems.css("display", "flex");
                } else {
                    $node.attr("expanded", "false");
                    $nodeItems.css("display", "none");
                }
            }
    </script>

    <div class="row-seperated">
        <input type="submit" class="btn btn-default " value="Save" />
        <button class="btn btn-default" onclick="history.back();">Cancel</button>
    </div>
}
```