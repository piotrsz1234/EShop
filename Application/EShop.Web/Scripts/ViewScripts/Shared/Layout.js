class Layout {
    constructor() {
        this.init = () => {
            //$('form-select').
        };
        this.addToBasket = (productId) => {
            $.ajax({
                url: '/Basket/AddOneMore',
                method: 'POST',
                data: {
                    productId: productId
                },
                //dataType: 'application/json'
            }).then(() => {
                console.log('test');
            });
        };
    }
}
const layout = new Layout();
$(document).ready(() => {
    layout.init();
});
//# sourceMappingURL=Layout.js.map