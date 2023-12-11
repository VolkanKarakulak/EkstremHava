// site.js
document.addEventListener("DOMContentLoaded", function () {
    updateCartCount();
});

function updateCartCount() {
    var cartCountElement = document.getElementById("cart-count");
    if (cartCountElement) {
        cartCountElement.textContent = @Html.Raw(Json.Serialize(Model.ItemsInBasketCount));
    }
}
