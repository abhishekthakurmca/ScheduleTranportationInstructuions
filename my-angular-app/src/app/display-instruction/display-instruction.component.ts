import { ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { GetInstructions, Instructions } from '../@shared/models/instructions';
import { instructionService } from '../instruction.service';
import { Router } from '@angular/router';
import { product } from '../@shared/models/Product';
import { DxDataGridComponent } from 'devextreme-angular';


@Component({
  selector: 'app-display-instruction',
  templateUrl: './display-instruction.component.html',
  styleUrls: ['./display-instruction.component.scss']
})
export class DisplayInstructionComponent {
  
  instructionList: GetInstructions[] = []
  productList: product[] = [];
  formData: GetInstructions = new GetInstructions

  constructor(private service: instructionService,
    private router: Router,
    private changeDetectorRef: ChangeDetectorRef
  ) {

  }

  ngOnInit() {
    this.getInstruction()
  }

  // method is used to get all instruction with products
  getInstruction() {
    this.service.getAllInstructions().subscribe({
      next: (response: GetInstructions[]) => {
        this.instructionList = response;
        console.log(this.instructionList);
      },
      error: (e) => {
        console.log('Failed to get Instruction');
      },
      complete: () => console.log('completed Instruction get Request')
    })
  }

  // method is used to implement condition to disable edit and delete button
  allowUpdatingandDeleting(e: any): boolean {
    return e.row.data.status === 'Pending';
  }

  // method is used to nevigate to add instruction page
  newInstruction() {
    this.router.navigateByUrl('/newinstruction');
  }

  // method is called on submit button click in edit and delete popup model (its an devextreme data-grid functionality).
  onSaving(e: any) {
    debugger;
    this.formData.products = this.productList
    if (e.changes.length > 0 && e.changes[0].type != 'remove'  || this.formData.products.length > 0) {
      if(e.changes.length > 0){
        this.formData.pickupAddress = e.changes[0].data.pickupAddress ?? this.formData.pickupAddress
        this.formData.deliveryAddress = e.changes[0].data.deliveryAddress ?? this.formData.deliveryAddress
      }      
      this.service.updateInstruction(this.formData).subscribe({
        next: (response: GetInstructions) => {
          console.log(response)
        },
        error: (e) => {
          console.log('Failed to update Instruction');
        },
        complete: () => console.log('completed Instruction update Request')
      })

    }
    else if (e.changes[0].type == 'remove') {
      this.service.deleteInstructution(e.changes[0].key.id).subscribe({
        next: (response: Instructions) => {
          console.log(response)
        },
        error: (e) => {
          console.log('Failed to delete Instruction');
        },
        complete: () => console.log('completed Instruction delete Request')
      })
    }
    else {
      console.log("No Changes Done")
    }
  }

  // this method is called on edit button click and bind data in formData and the list of products in productList var
  selectionChanged(a: any) {  
    debugger  
    this.formData = a.data
    this.productList = []
    this.productList = a.data.products
    this.changeDetectorRef.detectChanges(); 
  }

  // this method is called when value change in Product Dropdown then call the method with logic
  // we have done like this because it is creating new instance and cant able to excess old instance 
  // variable values
  handleProductValueChanged = (e: any) => {    
    this.ProductValueChange(e.value);
  }

  // this method contain logic on product change in dropdown and called by handleProductValueChanged method 
  ProductValueChange(data: string) {   
    var selectedvalue = this.productList.filter(x => { return x.productCode == data });
    if (selectedvalue) {
      this.formData.products[0].productCode = selectedvalue[0].productCode;
      this.formData.products[0].productDescription = selectedvalue[0].productDescription;
    } else
      console.log('data not found');
  }

  // we are refreshing the page on canceling.
  onCancelClick(){
    window.location.reload();
  }

}
