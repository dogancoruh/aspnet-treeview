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

.treeview {
    display: block;
}

.treeview-node {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: flex-start;
    width: 900px;
}

.treeview-item {
    list-style-type: none;
    margin: 0;
    padding: 10px 0px 0px 10px;
    position: relative;
    box-sizing: border-box;
}

.treeview-item ul {
    padding-left: 20px;
}

.treeview-item-with-line::before {
    position: absolute;
    border-left: 1px solid #333;
    bottom: 50px;
    height: 100%;
    top: 0;
    left: -13px;
    right: auto;
    width: 1px;
    content: '';
    box-sizing: border-box;
}

.treeview-item-last-with-line::before {
    position: absolute;
    border-left: 1px solid #333;
    bottom: 50px;
    height: 20px;
    top: 0;
    left: -13px;
    right: auto;
    width: 1px;
    content: '';
    box-sizing: border-box;
}

.treeview-item-with-line::after,
.treeview-item-last-with-line::after {
    position: absolute;
    border-top: 1px solid #333;
    top: 20px;
    left: -13px;
    width: 15px;
    content: '';
    box-sizing: border-box;
}


.treeview-item-title {
    font-size: 16px;
    margin-left: 2px;
    padding-left: 2px;
}

.treeview-item-checkbox {
    margin-left: 5px;
    display: block;
}
