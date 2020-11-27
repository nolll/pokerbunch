import browser from './browser';
import app from './app';
import './styles';

function domReady(callback: () => void) {
    const isDone = document.readyState === 'interactive' || document.readyState === 'complete';
    isDone
        ? callback()
        : document.addEventListener('DOMContentLoaded', callback);
}

if (!browser.isCapable()) {
    alert('PokerBunch requires a better browser');
}

domReady(() => {
    app.init();
});
