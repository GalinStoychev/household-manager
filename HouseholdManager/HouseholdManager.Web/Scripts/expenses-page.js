$(".info-container").on("click", function (ev) {
    $target = $(ev.target);
    var id = $target.parents(".expense-container").attr("id");
    var currentUrl = window.location.href;
    var expenses = "Expenses";
    var finalUrl = "";
    var len = len = currentUrl.indexOf(expenses) + expenses.length;
    for (var i = 0; i < len; i++) {
        finalUrl += currentUrl[i];
    }

    window.location.href = finalUrl + "/id/" + id;
})