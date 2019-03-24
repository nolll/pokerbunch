interface IVueConfig {
    apiHost: string;
}

interface Window {
    vueConfig: IVueConfig;
}

interface ITextInput {
    value: string;
    createTextRange(): any;
    focus(): any;
    setSelectionRange(start: number, end: number): any;
    selectionStart: number;
    selectionEnd: number;
}
