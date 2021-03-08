﻿/* eslint-disable no-undef */
'use strict';

function NewUser(e) {
    e.preventDefault();
    let storeId = document.getElementById('stores').value;
        const newuser =
        {
            customerid: undefined,
            firstname: document.getElementById('firstname').value,
            lastname: document.getElementById('lastname').value,
            streetaddress1: document.getElementById('streetaddress1').value,
            streetaddress2: document.getElementById('streetaddress2').value,
            city: document.getElementById('city').value,
            state: document.getElementById('state').value,
            zipcode: document.getElementById('zipcode').value,
            defaultstore: storeId
        };
        console.log(newuser);
        Ping(newuser);
    }

function Ping(newuser) {
    return fetch('/api/customer/add', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newuser)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`Network response was not ok (${response.status})`);
            }
            return response.json();
        })
        .then(customer => {
            console.log(customer)
            sessionStorage.setItem('customerId', customer.customerID);
            sessionStorage.setItem('storeId', customer.defaultStore);
        });
}

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
            selectStore.appendChild(new Option(`${store.storeName} - ${store.storeCity}, ${store.storeState}`, `${store.storeID}`));
        }
    })

submit.addEventListener('click', NewUser);
