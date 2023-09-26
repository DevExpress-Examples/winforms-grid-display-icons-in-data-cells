<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128631092/23.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E605)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# WinForms Data Grid - Display icons in grid cells

This example demonstrates various techniques to display images/icons within data cells. For example, the `CustomDrawCell` event is handled to draw icons within **CustomDraw** column cells.

```csharp
private void GridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e) {
    if (e.RowHandle != GridControl.NewItemRowHandle && e.Column.FieldName == "CustomDraw") {
        e.DisplayText = string.Empty;
        e.DefaultDraw();
        e.Cache.DrawImage(GetImageFromResource(e.CellValue.ToString()), e.Bounds.X, e.Bounds.Y);
    }
}
```
Read the following help topic for more information: [Cell Icons - How to Display an Image in a Grid Cell](https://docs.devexpress.com/WindowsForms/643/controls-and-libraries/data-grid/views/grid-view/cells#icons)


## Files to Review

* [Form1.cs](./CS/Form1.cs) (VB: [Form1.vb](./VB/Form1.vb))


## See Also

* [Cell Icons - How to Display an Image in a Grid Cell](https://docs.devexpress.com/WindowsForms/643/controls-and-libraries/data-grid/views/grid-view/cells#icons)
* [How to Display Images from a Data Field with Image Paths](https://docs.devexpress.com/WindowsForms/403845/controls-and-libraries/data-grid/examples/data-presentation/how-to-display-images-from-url)
* [DevExpress WinForms Cheat Sheet - Display Images in Controls](https://go.devexpress.com/CheatSheets_WinForms_Examples_T914488.aspx)
* [DevExpress WinForms Troubleshooting - Grid Control](https://go.devexpress.com/CheatSheets_WinForms_Examples_T934742.aspx)



