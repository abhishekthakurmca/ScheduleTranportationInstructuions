// this model is used to display list of instruction with products and its transporter.
export class schedule {
    id : number = 0
    instructionDate : string =""
    clientName : string =""
    pickupAddress: string =""
    deliveryAddress: string =""
    clientRef: string =""
    billingRef: string =""
    status : string =""
    products : scheduleProductDetails[] = []
}

export class scheduleProductDetails{
    productId : number = 0 
    productCode : string =""
    productDescription : string =""
    qty : string =""
    scheduleTransportID : number = 0
    dateScheduled : string =""
    transporter : string =""
}
