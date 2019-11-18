function init() {
    var me = this;
    updateLayouts(me);
    this.addEventListener('blur', function() {
        updateLayouts(me);
    });
}

function updateLayouts(symbolElement){
    var symbol = symbolElement.value;
    var layoutElement = document.getElementById('currencylayout');
    var optionElements = layoutElement.querySelectorAll('option');
    var i, option;
    for (i = 0; i < optionElements.length; i++) {
        option = optionElements[i];
        option.text = option.value.replace('{SYMBOL}', symbol).replace('{AMOUNT}', '123');
    }
}

export default {
    init: init
};
