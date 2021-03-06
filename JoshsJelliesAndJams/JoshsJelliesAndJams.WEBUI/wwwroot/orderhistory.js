﻿/* eslint-disable no-undef */
'use strict';

const id = 17;

function ProductTable() {
    return fetch(`/api/order/history/${id}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

ProductTable(id)
    .then(orders => {
        for (const order of orders) {
            console.log(orders)
            const row = ordertable.insertRow();
            row.innerHTML = `<td class="productId">${order.orderNumber}</td>
                             <td class="date">${order.orderPlaced}</td>
                             <td class="quantity">${order.numberOfProducts}</td>
                             <td class="total">${order.total}</td>`;
            row.addEventListener('click', () => {
                sessionStorage.setItem('orderId', order.orderNumber);
                location = 'orderdetails.html';
            });
        };
    });