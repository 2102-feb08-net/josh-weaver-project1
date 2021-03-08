'use strict';

const storeId = sessionStorage.getItem('storeId')
const customerId = sessionStorage.getItem('customerId')

function CheckSession() {
    if (sessionStorage.getItem('storeId') == null) {
        alert('Please sign in or sign up to place an order');
        window.location.replace("https://joshsjelliesandjams.azurewebsites.net/newcustomer.html");
    }
    else {
        ProductTable(storeId);
    }
}


function ProductTable(storeId) {

    return fetch(`api/store/inventory/${storeId}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    })
        .then(products => {

            for (const prod in products) {
                const row = ordertable.insertRow();
                row.innerHTML = `<td class="productId">${products[prod].productId}</td>
                             <td class="name">${products[prod].name}</td>
                             <td><label for="quantity" name="quantity"><input type="number" class="quantity" min="0" max="15"></input></label></td>
                             <td class="costPerItem">${products[prod].costPerItem}</td>
                             <td><label for="total" name="total${[prod]}"><input type="text"  class="total" readonly></input></label></td>`;
                row.addEventListener('change', () => {
                    let total = document.getElementsByClassName('total')[prod];
                    total.value = (parseFloat(document.getElementsByClassName('quantity')[prod].value) * parseFloat(document.getElementsByClassName('costPerItem')[prod].innerHTML)).toFixed(2);
                });
            }
        })
}

CheckSession();


let order;
let productlist = [{}];

function SubmitOrder(e){
    e.preventDefault();
    productlist = [{}];
    let table = document.getElementById('ordertable');
    let i;
    for (i = 0; i < table.rows.length; i++) {
        let productID = document.getElementsByClassName('productId')[i].innerHTML;
        let productName = document.getElementsByClassName('name')[i].innerHTML;
        let numberOfItems = document.getElementsByClassName('quantity')[i].value;
        let cost = document.getElementsByClassName('costPerItem')[i].innerHTML;
        let total = document.getElementsByClassName('total')[i].value;

        let product = {
            "productId": productID,
            "name": productName,
            "quantity": numberOfItems,
            "costPerItem": cost,
            "totalLine": total
        };
        productlist.push(product);
    };
    RemoveBlanks(productlist);
};


function RemoveBlanks(productlist) {
    productlist = productlist.filter(x => (x.quantity > 0) && (x.quantity != undefined));
    ConstructOrder(productlist)
}



function ConstructOrder(productlist) {
    let totalCost = 0;
    let totalQuantity = 0;

    for (let item of productlist) {
        totalCost += parseFloat(item.totalLine);
        totalQuantity += parseInt(item.quantity);
    };

    let newOrder = {
        "orderNumber": undefined,
        "product": productlist,
        "orderPlaced": undefined,
        "total": parseFloat(totalCost).toFixed(2),
        "numberOfProducts": parseInt(totalQuantity),
        "customerNumber": customerId,
        "storeId": storeId
    };
    ToDb(newOrder)
}

function ToDb(newOrder) {
    return fetch('/api/order/add', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newOrder)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`Network response was not ok (${response.status})`);
            }
            alert("Thank you! Your order was accepted");
            return response.json();
        }); 
}
    
submit.addEventListener("click", SubmitOrder);
