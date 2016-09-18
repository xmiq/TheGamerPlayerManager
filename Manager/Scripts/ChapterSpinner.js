function fillData(option) {
    $("#chapter-select").val(option.val());
    $("#chapter-number").text(option.text());

    var output = $("output");

    output.children().remove();

    $.get("/Stats/Details/" + option.val(), function (data) {
        output.append($("<div></div>").attr("id", "dvStats").html(data));
        $("#edit-stats").on("touch click", function () {
            window.location = "/Stats/Edit/" + option.val() + "?username=" + $("#Username").val() + "&player=" + $("#PlayerID").val();
        });
        $("#AddXP").on("touch click", function () {
            window.location = "/Stats/AddXP/" + option.val() + "?username=" + $("#Username").val() + "&player=" + $("#PlayerID").val();
        });
    })
}

$(document).ready(function () {
    fillData($("#chapter-select option:selected"));

    $("#chapter-previous").on("click touch", function () {
        var selected = $("#chapter-select option:selected");
        var previous = selected.prev();
        if (previous.length > 0) {
            fillData(previous);
        } else {
            fillData($("#chapter-select option:last"));
        }
    });

    $("#chapter-next").on("click touch", function () {
        var selected = $("#chapter-select option:selected");
        var next = selected.next();
        if (next.length > 0) {
            fillData(next);
        } else {
            fillData($("#chapter-select option:first"));
        }
    });

    $("#chapter-new").on("click touch", function () {
        window.location = "/Chapter/Create/" + $("#PlayerID").val() + "?username=" + $("#Username").val();
    });
});