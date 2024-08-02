import { Product, product } from "./Product";

export class Instructions{
    constructor(data: GetInstructions) {
        this.id = data.id;
        this.instructionDate = data.instructionDate;
        this.clientName = data.clientName;
        this.pickupAddress = data.pickupAddress;
        this.deliveryAddress = data.deliveryAddress
        this.clientRef = data.clientRef
        this.billingRef = data.billingRef
        this.status = data.status
    }
    id:number = 0; 
    instructionDate:string = "";
    clientName:string = "";
    pickupAddress:string = "";
    deliveryAddress:string = "";
    clientRef:string = "";
    billingRef:string = "";
    status:string = "";   
    productCode:string = "";
    productDescription:string = "";
    qty:string = "";
}

export class GetInstructions{    
    id:number = 0; 
    instructionDate:string = "";
    clientName:string = "";
    pickupAddress:string = "";
    deliveryAddress:string = "";
    clientRef:string = "";
    billingRef:string = "";
    status:string = "";
    products: product[] = [];
}
