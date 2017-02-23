(function () {

    $(document).on("click", ".modal-body", function () {
        $(".date").datepicker({
            dateFormat: 'yy-mm-dd'
        });
    });

    $('#modalContainer').on('submit', '#updateLineItemForm', function (e) {
        e.preventDefault();

        var form = $(this);
        $.ajax({
            url: 'UpdateLineItem',
            type: 'POST',
            data: form.serialize(),
            success: function () {
                $('#modalContainer').modal('toggle');
                location.reload();
            },
            error: function () {
                alert("All fields are required to update line item.");
            }
        });
    });

}());