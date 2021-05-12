$(document).ready(function () {
    $('.add-to-cart').on('click', function () {
        _this = $(this);
        var { code, quantityTarget, url, target } = _this.data();
        var quantity = parseInt($(quantityTarget).val() ?? '0');
        if (quantity <= 0) {
            alert("You have to add at least 1 product into Cart");
            return;
        }
        var data = {
            code: code,
            quantity: quantity
        }
        $.post(url, data, function () {

        }).done(function (response) {
            let productText = quantity > 1 ? "products" : 'product';
            alert("You added " + quantity + " " + productText + " into Cart");
            $(target).html(response);
        });
    });

});