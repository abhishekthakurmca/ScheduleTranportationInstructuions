import { Component } from '@angular/core';
import { ClientList, ProductList } from '../@shared/Constants/const';
import { Product, product } from '../@shared/models/Product';
import { Client } from '../@shared/models/Client';
import { PostInstruction, form } from '../@shared/models/instruction';
import { ToastrService } from 'ngx-toastr';
import { instructionService } from '../instruction.service';
import { Router } from '@angular/router';
import { GetInstructions } from '../@shared/models/instructions';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-instruction',
  templateUrl: './instruction.component.html',
  styleUrls: ['./instruction.component.scss']
})
export class InstructionComponent {
  selectedValue: Client[] = [];
  product: Product = new Product;
  productList: Product[] = [];
  selectedProductList: product[] = [];

  clientList: Client[] = []
  formData: form = new form(new GetInstructions);
  
  constructor(private service: instructionService,
    private router: Router
    ) {

  }

  ngOnInit() {
    this.productList = ProductList;
    this.clientList = ClientList;
  }

  // method is used to add update qty of selected product in grid to save. 
  AddProduct() {
    if (this.formData.qty > 0) {
      var selectedProductdetail = this.productList.filter(x => x.productCode == this.formData.product);
      var exist = this.selectedProductList.findIndex(x => x.productCode == selectedProductdetail[0].productCode)
      if (exist == -1) {
        var selectedProduct = new product(selectedProductdetail[0]);
        selectedProduct.qty = this.formData.qty;
        this.selectedProductList.push(selectedProduct);
        this.formData.qty = 0
        this.formData.product = ""
        this.formData.productCode = ""
        this.formData.productDiscription = ""
      } else {
        this.selectedProductList[exist].qty = this.selectedProductList[exist].qty + this.formData.qty;
        this.formData.qty = 0
        this.formData.product = ""
        this.formData.productCode = ""
        this.formData.productDiscription = ""
      }
      console.log(this.selectedProductList)
    }
    else
      alert("Qty can not Zero")
  }

  // method is called on edit click and call getbyid method to get latest data
  EditProduct(id: number){
    this.service.getInstructionById(id).subscribe({
      next: (response: GetInstructions) => {    
        console.log(response);
        this.formData = new form(response);
        this.selectedProductList = response.products
      },
      error: (e) => {
        console.log('Failed to get Instruction');
      },
      complete: () => console.log('completed Instruction get Request')
    })
  }
  

  // metod is call when clientName dropdown value is changed.
  ValueChange(data: string) {
    console.log(this.formData)
    var selectedClient = this.clientList.filter(x => x.Name == data);
    if (selectedClient) {
      this.formData.billingRef = selectedClient[0].billingRef;
      this.formData.clientRef = selectedClient[0].ClientRef;
    } else
      console.log('data not found');
  }

  // metod is call when product dropdown value is changed.
  ProductValueChange(data: string) {
    var selectedvalue = this.productList.filter(x => { return x.productCode == data });
    if (selectedvalue) {
      this.formData.productCode = selectedvalue[0].productCode;
      this.formData.productDiscription = selectedvalue[0].productDescription;
    } else
      console.log('data not found');

  }

  // this method is called when value change in client Dropdown then call the method with logic
  // we have done like this because it is creating new instance and cant able to excess old instance 
  // variable values
  handleClientValueChanged = (e: any) => {
    this.ValueChange(e.value);
  }

  // this method is called when value change in Product Dropdown then call the method with logic
  // we have done like this because it is creating new instance and cant able to excess old instance 
  // variable values
  handleProductValueChanged = (e: any) => {
    this.ProductValueChange(e.value);
  }

  // method called on final submit.
  Submit() {
    var result = new PostInstruction(this.formData,this.selectedProductList)
    this.service.saveInstruction(result).subscribe({
      next: (response:any) => {    
        console.log(response);
        this.navigate();
      },
      error: () => {
        console.log('Failed to Create Instruction');
      },
      complete: () => console.log('completed Instruction Create Request')
    })
  }

  // method is used to nevigate to home page. 
  navigate() {
    this.router.navigateByUrl('home');
  }
}



