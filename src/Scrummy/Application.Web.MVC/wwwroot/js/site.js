﻿function remove_row(btn) {
    var parent = btn.parentNode.parentNode;
    parent.remove();
}

$(function () {
    $("#dodTable").delegate("button[name='add']", "click", function (e) {
        e.preventDefault();
        var row =
            '<tr>' +
                '<td><input name="DefinitionOfDone[]" class="form-control"></td>' +
                '<td><button class="btn btn-default" name="up"><span class="glyphicon glyphicon-arrow-up"></span></button></td>' +
                '<td><button class="btn btn-default" name="down"><span class="glyphicon glyphicon-arrow-down"></span></button></td>' +
                '<td><button class="btn btn-default" name="del" onclick="remove_row(this)"><span class="glyphicon glyphicon-remove"></span></button></td>' +
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
        var roleOptionList = $("#roleOptions").data("value");
        var personOptionList = $("#personOptions").data("value");

        e.preventDefault();
        var row =
            '<tr>' +
                '<td><select class="form-control" name="SelectedMemberIds[]">' + personOptionList + '</select></td></td>' +
                '<td><select class="form-control" name="SelectedRoles[]">' + roleOptionList + '</select></td></td>' +
                '<td><button class="btn btn-default" name="del" onclick="remove_row(this)"><span class="glyphicon glyphicon-remove"></span></button></td>' +
            '</tr>';

        $('#memberTable tr:last').after(row);
    });

    $("#personTable").delegate("button[name='add']", "click", function (e) {
        var personOptionList = $("#personOptions").data("value");

        e.preventDefault();
        var row =
            '<tr>' +
                '<td><select class="form-control" name="SelectedPersonIds[]">' + personOptionList + '</select></td></td>' +
                '<td><button class="btn btn-default" name="del" onclick="remove_row(this)"><span class="glyphicon glyphicon-remove"></span></button></td>' +
            '</tr>';

        $('#personTable tr:last').after(row);
    });

    $("#linkedTaskTable").delegate("button[name='add']", "click", function (e) {
        var childOptionList = $("#childOptions").data("value");

        e.preventDefault();
        var row =
            '<tr>' +
                '<td><select class="form-control" name="ChildTaskIds[]">' + childOptionList + '</select></td></td>' +
                '<td><button class="btn btn-default" name="del" onclick="remove_row(this)"><span class="glyphicon glyphicon-remove"></span></button></td>' +
            '</tr>';

        $('#linkedTaskTable tr:last').after(row);
    });

    $("#backlogTable").delegate("button[name='up']", "click", function (e) {
        e.preventDefault();
        var $element = this;
        var row = $($element).parents("tr:first");

        if (!row.prev().is($('#tableHeader'))) {
            row.insertBefore(row.prev());
        }
    });
    $("#backlogTable").delegate("button[name='down']", "click", function (e) {
        e.preventDefault();
        var $element = this;
        var row = $($element).parents("tr:first");
        row.insertAfter(row.next());
    });

    $("#storiesTable").delegate("button[name='add']", "click", function (e) {
        var childOptionList = $("#storyOptions").data("value");

        e.preventDefault();
        var row =
            '<tr>' +
                '<td><select class="form-control" name="SelectedStories[]">' + childOptionList + '</select></td></td>' +
                '<td><button class="btn btn-default" name="up"><span class="glyphicon glyphicon-arrow-up"></span></button></td>' +
                '<td><button class="btn btn-default" name="down"><span class="glyphicon glyphicon-arrow-down"></span></button></td>' +
                '<td><button class="btn btn-default" name="del" onclick="remove_row(this)"><span class="glyphicon glyphicon-remove"></span></button></td>' +
            '</tr>';

        $('#storiesTable tr:last').after(row);
    });

    $("#storiesTable").delegate("button[name='up']", "click", function (e) {
        e.preventDefault();
        var $element = this;
        var row = $($element).parents("tr:first");

        if (!row.prev().is($('#tableHeader'))) {
            row.insertBefore(row.prev());
        }
    });
    $("#storiesTable").delegate("button[name='down']", "click", function (e) {
        e.preventDefault();
        var $element = this;
        var row = $($element).parents("tr:first");
        row.insertAfter(row.next());
    });

    $("#deleteBtn").click(function () {
        var deleteUrl = $("#deleteUrl").data("value");
        var deleteMessage = $("#deleteMessage").data("value");
        if (confirm(deleteMessage)) {
            $(location).attr('href', deleteUrl);
        }
    });

    $("#stepTable").delegate("button[name='add']", "click", function (e) {
        e.preventDefault();
        var row =
            '<tr>' +
                '<td><input name="Steps[]" class="form-control"></td>' +
                '<td><button class="btn btn-default" name="up"><span class="glyphicon glyphicon-arrow-up"></span></button></td>' +
                '<td><button class="btn btn-default" name="down"><span class="glyphicon glyphicon-arrow-down"></span></button></td>' +
                '<td><button class="btn btn-default" name="del" onclick="remove_row(this)"><span class="glyphicon glyphicon-remove"></span></button></td>' +
            '</tr>';

        $('#stepTable tr:last').after(row);
    });

    $("#stepTable").delegate("button[name='up']", "click", function (e) {
        e.preventDefault();
        var $element = this;
        var row = $($element).parents("tr:first");

        if (!row.prev().is($('#tableHeader'))) {
            row.insertBefore(row.prev());
        }
    });
    $("#stepTable").delegate("button[name='down']", "click", function (e) {
        e.preventDefault();
        var $element = this;
        var row = $($element).parents("tr:first");
        row.insertAfter(row.next());
    });

    $("#documentTable").delegate("button[name='add']", "click", function (e) {
        var childOptionList = $("#documentOptions").data("value");

        e.preventDefault();
        var row =
            '<tr>' +
                '<td><select class="form-control" name="SelectedDocumentIds[]">' + childOptionList + '</select></td></td>' +
                '<td><button class="btn btn-default" name="del" onclick="remove_row(this)"><span class="glyphicon glyphicon-remove"></span></button></td>' +
            '</tr>';

        $('#documentTable tr:last').after(row);
    });

    $("#linkTable").delegate("button[name='add']", "click", function (e) {
        e.preventDefault();
        var row =
            '<tr>' +
                '<td><input name="Links[]" class="form-control"></td>' +
                '<td><button class="btn btn-default" name="del" onclick="remove_row(this)"><span class="glyphicon glyphicon-remove"></span></button></td>' +
            '</tr>';

        $('#linkTable tr:last').after(row);
    });
});