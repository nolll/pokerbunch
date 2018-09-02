function isCapable() {
    return hasAddEventListener() && hasClassList() && hasArrayIndexOf();
}

function hasAddEventListener() {
    return document.addEventListener;
}

function hasClassList() {
    return document.body.classList;
}

function hasArrayIndexOf() {
    return Array.prototype.indexOf;
}

export default {
    isCapable: isCapable
};
