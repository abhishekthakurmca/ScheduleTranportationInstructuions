import { product } from "./Product"
import { GetInstructions } from "./instructions"

export class form {

  constructor(data: GetInstructions) {
    this.client = data.clientName
    this.clientRef = data.clientRef
    this.billingRef = data.billingRef
    this.status = data.status
    this.pickUpAddress = data.pickupAddress
    this.deliveryAddress = data.deliveryAddress
  }
  client: string = "";
  clientRef: string = "";
  billingRef: string = "";
  status: string = "";
  pickUpAddress: string = "";
  deliveryAddress: string = "";
  product: string = "";
  qty: number = 0;
  productCode: string = "";
  productDiscription: string = "";
}


export class PostInstruction {

  constructor(formdata: form, productlist: product[]) {
    this.clientName = formdata.client
    this.pickupAddress = formdata.pickUpAddress
    this.deliveryAddress = formdata.deliveryAddress
    this.clientRef = formdata.clientRef
    this.billingRef = formdata.billingRef
    this.products = productlist
  }
  clientName: string = "";
  pickupAddress: string = "";
  deliveryAddress: string = "";
  clientRef: string = "";
  billingRef: string = "";
  products: product[] = [];
}