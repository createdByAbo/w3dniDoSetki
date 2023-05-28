import myJson from './brands.json' assert {type: 'json'};

for (let i = 0; i < myJson.Brands.length; i++) {
    let option = document.createElement("option");
    option.innerHTML = myJson.Brands[i];
    option.value = i;
    option.id = 'filtering-select-brand-option-' + i
    document.getElementById("filtering-select-brand").appendChild(option);
}

function addModelsByBrandName(brand){
    
    
    document.getElementById("filtering").appendChild(sel)
}

document.getElementById("filtering-select-brand").addEventListener('change', addModelsByBrandName(document.getElementById("filtering-select-brand").innerHTML))