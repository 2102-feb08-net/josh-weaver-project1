/* eslint-disable no-undef */
'use strict';

const customerId = sessionStorage.getItem('customerId');

function ProductTable(customerId) {
    return fetch(`/api/order/history/${customerId}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

ProductTable(customerId)
    .then(orders => {
        for (const order of orders) {
            const total = order.total;
            const row = ordertable.insertRow();
            row.innerHTML = `<td class="productId">${order.orderNumber}</td>
                             <td class="date">${new Date(order.orderPlaced)}</td>
                             <td class="quantity">${order.numberOfProducts}</td>
                             <td class="total">${total.toFixed(2)}</td>`;
            row.addEventListener('click', () => {
                sessionStorage.setItem('orderId', order.orderNumber);
                location = 'orderdetails.html';
            });
        };
    });
