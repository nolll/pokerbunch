import IDomModule from './IDomModule';

export default class DeleteConfirmation implements IDomModule {
    private readonly el: HTMLElement;
    private readonly message: string | null;
    
    constructor(el: HTMLElement) {
        this.el = el;
        this.message = el.getAttribute('data-message');
    }

    private clickHandler(event: Event): boolean {
        if(!this.message)
            return false;
        
        const response = window.confirm(this.message);
        if (!response)
            event.preventDefault();
        return response;
    }

    public execute(): void {
        this.el.addEventListener('click', this.clickHandler.bind(this), false);
    }

    public dispose(): void {
        this.el.removeEventListener('click', this.clickHandler.bind(this), false);
    }
}