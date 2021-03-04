"use strict;"

let id = 1;

function ProductTable(id) {
    return fetch(`api/inventory/${id}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

ProductTable(id)
    .then(products => {

        for (const prod in products) {
            const row = ordertable.insertRow();
            console.log(products[prod]);
            row.innerHTML = `<td>${products[prod].productId}</td>
                             <td>${products[prod].name}</td>
                             <td><label for="quantity" name="quantity"><input type="text" id="quantity"></input></label></td>
                             <td>${products[prod].costPerItem}</td>`
        };
    });
    

