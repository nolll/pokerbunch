import cancelButton from './cancel-button';

const modules: Record<string, any> = {
    'cancel-button': cancelButton
};

function get(name: string): any
{
    return modules[name];
}

export default {
    get
};