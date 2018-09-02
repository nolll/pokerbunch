import cancelButton from './cancel-button';
import cashgameActionChart from './cashgame-action-chart';
import cashgameChart from './cashgame-chart';
import cashgameGameChart from './cashgame-game-chart';
import deleteConfirmation from './delete-confirmation';
import headingNav from './heading-nav';
import currencyForm from './currency-form';
import focusTextSelector from './focus-text-selector';

var modules = {
    'cancel-button': cancelButton,
    'cashgame-action-chart': cashgameActionChart,
    'cashgame-chart': cashgameChart,
    'cashgame-game-chart': cashgameGameChart,
    'delete-confirmation': deleteConfirmation,
    'heading-nav': headingNav,
    'currency-form': currencyForm,
    'focus-text-selector': focusTextSelector
};

function get(name)
{
    return modules[name];
}

export default {
    get: get
};