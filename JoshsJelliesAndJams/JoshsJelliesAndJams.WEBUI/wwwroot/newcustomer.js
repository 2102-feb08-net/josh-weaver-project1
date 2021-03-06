/* eslint-disable no-undef */
'use strict';

function NewUser(e) {
    e.preventDefault();

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
        defaultstore: 1
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
    });
}


submit.addEventListener('click', NewUser);
