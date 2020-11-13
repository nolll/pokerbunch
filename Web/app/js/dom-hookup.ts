import domModules from './dom-modules';

export default {
    init() {
        const elements = getElementsWithHookupAttribute();
    
        for (let i = 0; i < elements.length; i++) {
            hookupModulesForElement(elements[i]);
        }
    }
};

function getElementsWithHookupAttribute() {
    const elements = [];
    const nodeList = document.querySelectorAll('[data-hookup]');

    for (let i = 0; i < nodeList.length; i++) {
        elements.push(nodeList[i]);
    }

    return elements;
}

function hookupModulesForElement(el: Element) {
    const modulesToHookup = el.getAttribute('data-hookup') || '';
    const modules = modulesToHookup.split(',');

    for (let i = 0; i < modules.length; i++) {
        const moduleName = modules[i];
        const Module = domModules.get(moduleName);
        const m = new Module(el);
        m.execute();
    }
}
