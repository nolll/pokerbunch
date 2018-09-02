import browser from './browser';
import domHookup from './dom-hookup';
import vueComponents from './vue-components/vue-components.js';
import styles from './styles';

function domReady(callback) {
    document.readyState === 'interactive' || document.readyState === 'complete' ? callback() : document.addEventListener("DOMContentLoaded", callback);
}

if (!browser.isCapable()) {
    alert('PokerBunch requires a better browser');
}

domReady(function () {
    domHookup.init();
    vueComponents.init();
});
