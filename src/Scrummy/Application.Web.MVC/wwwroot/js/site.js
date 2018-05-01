function remove_row(btn) {
    var parent = btn.parentNode.parentNode;
    parent.remove();
}

$(function () {
    $("#dodTable").delegate("button[name='add']", "click", function (e) {
        e.preventDefault();
        var row =
            '<tr>' +
                '<td><input name="DefinitionOfDone[]" class="form-control"></td>' +
                '<td><button class="btn btn-default tableButton" name="up">Up</button></td>' +
                '<td><button class="btn btn-default tableButton" name="down">Down</button></td>' +
                '<td><button class="btn btn-default tableButton" name="del" onclick="remove_row(this)">Delete</button></td>' +
            '</tr>';

        $('#dodTable tr:last').after(row);
    });

    //https://stackoverflow.com/a/12594240
    $("#dodTable").delegate("button[name='up']", "click", function (e) {
        e.preventDefault();
        var $element = this;
        var row = $($element).parents("tr:first");

        if (!row.prev().is($('#tableHeader'))) {
            row.insertBefore(row.prev());
        }
    });
    $("#dodTable").delegate("button[name='down']", "click", function (e) {
        e.preventDefault();
        var $element = this;
        var row = $($element).parents("tr:first");
        row.insertAfter(row.next());
    });

    $("#memberTable").delegate("button[name='add']", "click", function (e) {
        e.preventDefault();
        var row =
            '<tr>' +
                '<td><input name="DefinitionOfDone[]" class="form-control"></td>' +
                '<td><button class="btn btn-default tableButton" name="up">Up</button></td>' +
                '<td><button class="btn btn-default tableButton" name="down">Down</button></td>' +
                '<td><button class="btn btn-default tableButton" name="del" onclick="remove_row(this)">Delete</button></td>' +
            '</tr>';

        $('#dodTable tr:last').after(row);
    });
});