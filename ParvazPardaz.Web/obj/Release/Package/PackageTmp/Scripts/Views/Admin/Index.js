function CheckSelectedGrid() {
    var gview = $("#Grid").data("kendoGrid");
    var selectedItem = gview.dataItem(gview.select());
    if (selectedItem == null) {
        toastr.options = { "positionClass": "toast-top-center" }
        toastr.error(ParvazPardaz.Resource.Title.Titles.SelectedGridRowAlert, ParvazPardaz.Resource.Title.Titles.SelectedGridRowAlertTitle, { timeOut: 5000 })
        return false;
    }
    else {
        var selectedId = selectedItem.Id;
        return selectedId;
    }
}
var wndRemove = $("#deleteConfirmation").kendoWindow({
    title: "",
    modal: true,
    visible: false,
    resizable: false,
    width: 400,
}).data("kendoWindow");

function create(parameters) {
    window.location.replace(CreateUrl);
}

function edit(parameters) {
    
    var result = CheckSelectedGrid();
    if (result) {
        window.location.replace(EditUrl + "/" + result);
    }
}

function del(parameters) {
    var result = CheckSelectedGrid();
    if (result) {
        wndRemove.center();
        wndRemove.open();
    }
}
$("#btnYes").click(function () {
    var gview = $("#Grid").data("kendoGrid");
    var selectedItem = gview.dataItem(gview.select());
    var selectedId = selectedItem.Id;
    $.ajax({
        url: DeleteUrl,
        type: "post",
        dataType: "json",
        cache: false,
        data: { Id: selectedId },
        success: function (result) {
            var gridm = $("#Grid").data("kendoGrid");
            if (gridm != undefined && result) {
                if (gridm.dataSource.transport.cache._store)
                    gridm.dataSource.transport.cache._store = {};
                gridm.dataSource.fetch();
                toastr.options = { "positionClass": "toast-top-center" }
                toastr.error(ParvazPardaz.Resource.Title.Titles.DeleteAlert, ParvazPardaz.Resource.Title.Titles.DeleteAlertTitle, { timeOut: 5000 })
            }
            else {
                toastr.options = { "positionClass": "toast-top-center" }
                toastr.error(ParvazPardaz.Resource.Title.Titles.ErrorAlert, ParvazPardaz.Resource.Title.Titles.ErrorAlertTitle, { timeOut: 5000 })
            }
        },
        error: function (xhr) {
            toastr.options = { "positionClass": "toast-top-center" }
            toastr.error(ParvazPardaz.Resource.Title.Titles.ErrorAlert, ParvazPardaz.Resource.Title.Titles.ErrorAlertTitle, { timeOut: 5000 })
        }
    });
    wndRemove.close();
})
$("#btnNo").click(function () {
    wndRemove.close();
})
