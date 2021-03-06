async function getAllAvailableCurrencies() {
    const url='http://localhost:5000/CurrencyExchange';

    await fetch(url)
        .then(response => {
            if(!response.ok) {
                throw new Error(response.status + ' - ' +response.statusText)
            } else {
                response.json().then(r => r.forEach(setCurrencySymbols));
            }
        })
        .catch(error => document.getElementById('errorMessage').innerHTML =
            error.message);
    
    function setCurrencySymbols(value, index, array){
        let baseCurrencyOption = document.createElement("option");
        baseCurrencyOption.text = value.currencySymbol+" - "+value.currencyName;
        baseCurrencyOption.value = value.currencySymbol;
        let baseCurrencySelect = document.getElementById('baseCurrency');
        baseCurrencySelect.appendChild(baseCurrencyOption);

        let targetCurrencyOption = document.createElement("option");
        targetCurrencyOption.text = value.currencySymbol+" - "+value.currencyName;
        targetCurrencyOption.value = value.currencySymbol;
        let targetCurrencySelect = document.getElementById('targetCurrency');
        targetCurrencySelect.appendChild(targetCurrencyOption);
    }
}

async function getTargetCurrencyAmount(){
    document.getElementById('errorMessage').innerHTML = ''
    document.getElementById('targetCurrencyAmount').value = '';
    const urlConvert='http://localhost:5000/CurrencyExchange/convert';
    
    let baseCurrencySelect = document.getElementById('baseCurrency');
    let targetCurrencySelect= document.getElementById('targetCurrency');
    let baseCurrencyInput = document.getElementById('baseCurrencyAmount');
    let targetCurrencyInput = document.getElementById('targetCurrencyAmount');
    let myJson = {BaseCurrencySymbol: baseCurrencySelect.value,
        TargetCurrencySymbol: targetCurrencySelect.value,
        BaseCurrencyAmount : baseCurrencyInput.value};
    
    await fetch(urlConvert, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(myJson)
    })
        .then(response => {
            if(!response.ok) {
                response.json().then(
                    err => document.getElementById('errorMessage').innerHTML =
                        response.status+
                        ' - '+
                        response.statusText+
                        ' : '+
                        err.Message
                );
            } else {
                response.json().then(r => targetCurrencyInput.value = r);
            }
        })
}

function validateBaseCurrencyAmountInput(){
    let el = document.getElementById('baseCurrencyAmount')
    if (el.value < 0) {
        alert('Amount cannot have negative value');
        el.value = 0;
    }
}