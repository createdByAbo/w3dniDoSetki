import brandsJson from './brands.json' assert {type: 'json'};

for (let i = 0; i < brandsJson.Brands.length; i++) {
    let option = document.createElement("option");
    option.innerHTML = brandsJson.Brands[i];
    option.value = i;
    option.id = 'filtering-select-brand-option-' + i
    document.getElementById("filtering-select-brand").appendChild(option);
}

const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const token = urlParams.get('token');

