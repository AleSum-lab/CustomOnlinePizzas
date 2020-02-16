// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

uncheckCheckboxes();
setBasePrice();

function uncheckCheckboxes() {
    var cb = document.getElementsByClassName("form-check-input");
    for (var i = 0; i < cb.length; i++) {
        cb[i].checked = false;
    }
}


function setBasePrice() {
    var base = document.getElementById('pizzaBase').value;

    document.getElementById('total').innerHTML = "= $" + parseFloat(base).toFixed(2);
    document.getElementById('total').value = base;
}


function add(ingredient) {
    var total = document.getElementById('total');
    var sum = parseFloat(total.value);
    var term;
    switch (ingredient) {
        case "tomatoSauce":
            term = parseFloat(document.getElementById('tomatoSauce').value);
            document.getElementById('cbTomato').checked ? sum += term : sum -= term;
            break;
        case "mozzarellaCheese":
            term = parseFloat(document.getElementById('mozzarellaCheese').value);
            document.getElementById('cbMozzarella').checked ? sum += term : sum -= term;
            break;
        case "ham":
            term = parseFloat(document.getElementById('ham').value);
            document.getElementById('cbHam').checked ? sum += term : sum -= term;
            break;
        case "kebab":
            term = parseFloat(document.getElementById('kebab').value);
            document.getElementById('cbKebab').checked ? sum += term : sum -= term;
            break;
        default:
    }

    
    total.innerHTML = "= $" + parseFloat(sum).toFixed(2);
    total.value = sum;
}

function calculateDeliveryFee() {
    var distance = parseFloat(document.getElementById('distance').value);
    var feeLabel = document.getElementById('deliveryFeeLb');
    var totalLabel = document.getElementById('orderTotal');
    var totalValue = parseFloat(document.getElementById('totalPrice').value);
    var fiveToTen = parseFloat(document.getElementById('fiveToTen').value);
    var tenToTwenty = parseFloat(document.getElementById('tenToTwenty').value);
    var result;

    if (distance < 5) {
        feeLabel.innerHTML = "Free";
    }
    else if (distance >= 5 && distance < 10) {
        feeLabel.innerHTML = "$" + fiveToTen;
        result = totalValue + fiveToTen;
        totalLabel.innerHTML = "$" + result;

    }
    else if (distance >= 10 && distance <= 20) {
        feeLabel.innerHTML = "$" + tenToTwenty;
        result = totalValue + tenToTwenty;
        totalLabel.innerHTML = "$" + result;
    }
    else if (distance > 20) {
        feeLabel.innerHTML = "Delivery Unavailable";
    }


}
