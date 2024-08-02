// this model is for product list we have const.
export class Product {
    product : string = "";
    productCode : string = "";
    productDescription: string = "";
  }

  // this model is to display products from database
  export class product {
    constructor(data : Product) {
      this.productCode = data.productCode
      this.productDescription = data.productDescription
    }
    productCode : string = "";
    productDescription: string = "";
    qty : number = 0;
  }