/* eslint-disable no-undef */
'use strict';

const orderId = sessionStorage.getItem('orderId');


function ProductTable() {
    return fetch(`/api/order/detail/${orderId}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

ProductTable(orderId)
    .then(details => {
        for (const line of details) {
            const total = line.totalLine;
            const row = ordertable.insertRow();
            row.innerHTML = `<td class="productName">${line.name}</td>
                             <td class="quantity">${line.quantity}</td>
                             <td class="costPerItem">${line.costPerItem}</td>
                             <td class="total">${total.toFixed(2)}</td>`;
        };
    })
