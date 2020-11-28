function hasAddEventListener() {
    return document.addEventListener;
}

function hasClassList() {
    return document?.body?.classList || false;
}

function hasArrayIndexOf() {
    return Array.prototype.indexOf;
}

export default {
    isCapable: () => hasAddEventListener() && hasClassList() && hasArrayIndexOf(),
};
