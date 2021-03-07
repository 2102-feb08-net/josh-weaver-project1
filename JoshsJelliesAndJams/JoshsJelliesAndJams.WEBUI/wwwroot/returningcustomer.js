'use strict';

var myModal = document.getElementById('myModal');
var myInput = document.getElementById('myInput');
let submit = document.getElementById('search');
let confirm = document.getElementById('confirm');
let fname;
let lname;
let sessionCustomer;

submit.addEventListener('click', () => {
    fname = document.getElementById('firstName').value;
    lname = document.getElementById('lastName').value;
    Lookup(fname, lname);
})

myModal.addEventListener('shown.bs.modal', function () {
    myInput.focus()
})


function Lookup(fname, lname) {
    return fetch(`/api/customer/${fname}/${lname}`).then(response => {
        if (!response.ok) {
            alert('Customer was not found');
        }
        return response.json();
    })
        .then(customer => {
            const modalBody = document.getElementById('modalBody');
            modalBody.innerHTML = `<div>${customer.firstName} ${customer.lastName}<br>
                                   ${customer.streetAddress1}<br>
                                   ${customer.streetAddress2}<br>
                                   ${customer.city}, ${customer.state}, ${customer.zipcode}</div>`
            sessionCustomer = customer

            confirm.addEventListener('click', () => {
                sessionStorage.setItem('customerId', customer.customerID);
                sessionStorage.setItem('storeId', customer.defaultStore);
            })
        })
}
