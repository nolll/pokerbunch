import cancelButton from './cancel-button';
import deleteConfirmation from './delete-confirmation';

const modules: Record<string, any> = {
    'cancel-button': cancelButton,
    'delete-confirmation': deleteConfirmation
};

function get(name: string): any
{
    return modules[name];
}

export default {
    get
};