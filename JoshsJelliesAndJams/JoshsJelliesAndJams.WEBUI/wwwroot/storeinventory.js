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
        alert('Select a store to view inventory');
    }
    else {
        return fetch(`/api/store/inventory/${storeID}`).then(response => {
            if (!response.ok) {
                throw new Error(`Network response was not ok (${response.status})`);
            }
            return response.json();
        })
            .then(products => {
                inventorytable.innerHTML = ``;
                for (const inventory of products) {
                    const row = inventorytable.insertRow();
                    row.innerHTML = `<td class="productId">${inventory.productId}</td>
                             <td class="name">${inventory.name}</td>
                             <td class="quantity">${inventory.quantity}</td>`;
                };
            });
    }
}
