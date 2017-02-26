function fillData(option) {
    $("#chapter-select").val(option.val());
    $("#chapter-number").text(option.text());

    var output = $("output");

    output.children().remove();

    $.get("/Stats/Details/" + option.val() + "?username=" + $("#Username").val(), function (data) {
        output.append($("<div></div>").attr("id", "dvStats").attr("style", "order: 1").html(data));
        if (IsOwner) {
            $("#edit-stats").on("touch click", function () {
                window.location = "/Stats/Edit/" + option.val() + "?username=" + $("#Username").val() + "&player=" + $("#PlayerID").val() + "&story=" + $("#Story").val();;
            });
            $("#AddXP").on("touch click", function () {
                window.location = "/Stats/AddXP/" + option.val() + "?username=" + $("#Username").val() + "&player=" + $("#PlayerID").val() + "&story=" + $("#Story").val();
            });
        }
    });

    $.get("/SkillStats/Index/" + option.val() + "?username=" + $("#Username").val() + "&player=" + $("#PlayerID").val(), function (data) {
        output.append($("<div></div>").attr("id", "dvSkills").attr("style", "order: 2").html(data));
        if (IsOwner) {
            $("#AddSkillStat").on("touch click", function () {
                window.location = "/SkillStats/Create/" + option.val() + "?username=" + $("#Username").val() + "&player=" + $("#PlayerID").val() + "&story=" + $("#Story").val();
            });
            $("[name='EditSkillStat']").on("touch click", function () {
                var id = $(this).parent().children("input").val();
                window.location = "/SkillStats/Edit/" + id + "?username=" + $("#Username").val() + "&player=" + $("#PlayerID").val() + "&chapter=" + option.val() + "&story=" + $("#Story").val();
            });
            $("[name='DeleteSkillStat']").on("touch click", function () {
                window.location = "/SkillStats/Delete/" + option.val() + "?username=" + $("#Username").val() + "&player=" + $("#PlayerID").val() + "&story=" + $("#Story").val();
            });
            $("[name='AddSkillXP']").on("touch click", function () {
                window.location = "/SkillStats/AddXP/" + option.val() + "?username=" + $("#Username").val() + "&player=" + $("#PlayerID").val() + "&story=" + $("#Story").val();
            });
        }
    });
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

    if (IsOwner) {
        $("#chapter-new").on("click touch", function () {
            window.location = "/Chapter/Create/" + $("#PlayerID").val() + "?username=" + $("#Username").val() + "&story=" + $("#Story").val();
        });
    }
});