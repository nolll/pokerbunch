import cancelButton from './cancel-button';
import deleteConfirmation from './delete-confirmation';
import headingNav from './heading-nav';
import currencyForm from './currency-form';
import focusTextSelector from './focus-text-selector';

var modules = {
    'cancel-button': cancelButton,
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