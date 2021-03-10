/* eslint-disable no-undef */
'use strict';

stores.addEventListener(`change`, () => {
    let storeID = document.getElementById('stores').value;
    ProductTable(storeID);
})

function GetStores() {
    return fetch(`/api/store/list`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`)
        }
        return response.json();
    })
}

GetStores()
    .then(stores => {
        for (const store of stores) {
            const selectStore = document.getElementById('stores');
            const option = document.createElement('option');
            option.text = `${store.storeName} - ${store.storeCity}, ${store.storeState}`
            option.value = `${store.storeID}`
            selectStore.appendChild(new Option(`${store.storeName} - ${store.storeCity}, ${store.storeState}`, `${store.storeID}`));
        }

    })

function ProductTable(storeID) {
    if (storeID === "null" || storeID === undefined) {
        alert('Select a store to view history');
    }
    else {
        return fetch(`/api/store/${storeID}`).then(response => {
            if (!response.ok) {
                throw new Error(`Network response was not ok (${response.status})`);
            }
            return response.json();
        })
            .then(orders => {
                ordertable.innerHTML = ``;
                for (const order of orders) {
                    let total = order.total;
                    total = total.toFixed(2);
                    const row = ordertable.insertRow();
                    row.innerHTML = `<td class="productId">${order.orderNumber}</td>
                             <td class="date">${new Date(order.orderPlaced)}</td>
                             <td class="quantity">${order.numberOfProducts}</td>
                             <td class="total">${total}</td>`;
                    row.addEventListener('click', () => {
                        sessionStorage.setItem('orderId', order.orderNumber);
                        location = 'orderdetails.html';
                    });
                };
            });
    }
}



