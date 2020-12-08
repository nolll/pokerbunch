import browser from './browser';
import app from './app';
import './styles';

if (!browser.isCapable()) {
    alert('PokerBunch requires a better browser');
}

function domReady(callback: () => void) {
    const isDone = document.readyState === 'interactive' || document.readyState === 'complete';
    isDone
        ? callback()
        : document.addEventListener('DOMContentLoaded', callback);
}

domReady(() => {
    app.init();
});
