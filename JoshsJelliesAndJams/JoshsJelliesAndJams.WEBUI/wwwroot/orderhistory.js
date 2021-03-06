"use strict;"

let id = 17;

function ProductTable(id) {
    return fetch(`/api/order/history/${id}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

ProductTable(id)
    .then(products => {

        for (const prod of products) {
            console.log(prod);
            const row = ordertable.insertRow();
            row.innerHTML = `<td class="productId">${prod.orderNumber}</td>
                             <td class="name">${prod.orderPlaced}</td>
                             <td class="quantity">${prod.numberOfProducts}</td>
                             <td class="total">${prod.total}</td>`;
            row.addEventListener('change', () => {
                console.log(document.getElementsByClassName('total')[prod]);
            });
        };
    });