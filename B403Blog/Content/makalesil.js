$(function () {
    $("#tblMakaleler").on("click", ".btnMakaleSil", function () {
        var btn = $(this);
        bootbox.confirm("Makaleyi Silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                var id = btn.data("id");

                $.ajax({
                    type: "GET",
                    url: "/Makale/Sil" + id,
                    success: function () {
                        btn.parent().parent().remove();
                    }

                });
            }
        })
    });
});