const hasAddEventListener = () => document.addEventListener;
const hasClassList = () => document?.body?.classList || false;
const hasArrayIndexOf = () => Array.prototype.indexOf;

export default {
  isCapable: () => hasAddEventListener() && hasClassList() && hasArrayIndexOf(),
};
