interface IVueConfig {
    apiUrl: string;
}

interface Window {
    vueConfig: IVueConfig;
}

interface HTMLInputElement {
    value: string;
    createTextRange(): any;
    focus(): any;
    setSelectionRange(start: number, end: number): any;
}
