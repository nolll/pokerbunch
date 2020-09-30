import IDomModule from './IDomModule';

export default class CancelButton implements IDomModule {
    private readonly el: HTMLElement;
    private readonly cancelUrl: string | null;
    
    constructor(el: HTMLElement) {
        this.el = el;
        this.cancelUrl = el.getAttribute('data-cancel-url');
    }

    private clickHandler(event: Event): void {
        event.preventDefault();
        if (this.cancelUrl !== null) {
            location.href = this.cancelUrl;
        } else {
            history.back();
        }
    }

    public execute(): void {
        this.el.addEventListener('click', this.clickHandler.bind(this), false);
    }

    public dispose(): void{
        this.el.removeEventListener('click', this.clickHandler.bind(this), false);
    }
}