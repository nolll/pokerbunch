import browser from './browser';
import domHookup from './dom-hookup';
import app from './app';
import styles from './styles';

function domReady(callback) {
    document.readyState === 'interactive' || document.readyState === 'complete' ? callback() : document.addEventListener("DOMContentLoaded", callback);
}

if (!browser.isCapable()) {
    alert('PokerBunch requires a better browser');
}

domReady(function () {
    domHookup.init();
    app.init();
});
