class AddEditProduct {
    constructor() {
        this.fileCounter = 0;
        this.init = () => {
            this.assignControls();
            this.initControls();
        };
        this.assignControls = () => {
            this.template = $('#customFileTemplate');
            this.container = $('#extraFiles');
            this.btnAdd = $('#btnAddFile');
        };
        this.initControls = () => {
            this.btnAdd.click(() => {
                this.container.append(this.template.html().replace('{0}', (this.fileCounter).toString())
                    .replace('{0}', (this.fileCounter).toString()));
                this.fileCounter++;
            });
        };
    }
}
const addEditProduct = new AddEditProduct();
$(document).ready(() => {
    addEditProduct.init();
});
//# sourceMappingURL=AddEditProduct.js.map