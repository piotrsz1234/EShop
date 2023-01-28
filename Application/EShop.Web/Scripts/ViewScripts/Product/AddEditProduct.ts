class AddEditProduct {
    
    private template: JQuery;
    private container: JQuery;
    private btnAdd: JQuery;
    private fileCounter: number = 0;
    
    public init = (): void => {
        this.assignControls();
        this.initControls();
    };
    
    private assignControls = (): void => {
        this.template = $('#customFileTemplate');
        this.container = $('#extraFiles');
        this.btnAdd = $('#btnAddFile');
    }
    
    private initControls = (): void => {
        this.btnAdd.click(() => {
           this.container.append(this.template.html().replace('{0}', (this.fileCounter).toString())
               .replace('{0}', (this.fileCounter).toString()));
            this.fileCounter++
        });
    }
}

const addEditProduct = new AddEditProduct();

$(document).ready(() => {
    addEditProduct.init();
});