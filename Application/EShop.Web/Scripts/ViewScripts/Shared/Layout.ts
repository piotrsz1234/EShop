class Layout {
    public init = (): void => {
       //$('form-select').
    }
    
    public addToBasket = (productId: number): void => {
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
    }
}

const layout: Layout = new Layout();
$(document).ready(() => {
   layout.init(); 
});