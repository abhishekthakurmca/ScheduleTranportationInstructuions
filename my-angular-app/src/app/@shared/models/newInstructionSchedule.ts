// this model is used to assign new transporter to product
export class newSchedule{ 
    constructor(InstructionId:number, ProductId: number, DateScheduled:Date, Transporter: string) {
       this.scheduleTransportID = 0
        this.transporter = Transporter
       this.dateScheduled = DateScheduled
       this.instructionId = InstructionId  
       this.productId = ProductId
    }
     scheduleTransportID :number = 0
     dateScheduled : Date = new Date    
     transporter : string =""      
     instructionId : number = 0
     productId : number = 0
  }