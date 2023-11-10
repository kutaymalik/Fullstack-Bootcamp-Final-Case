import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/services/product.service';
import { CategoryService } from 'src/app/services/category.service';
import { OrderService } from 'src/app/services/order.service';
import { DealerService } from 'src/app/services/dealer.service';
import { Product } from '../product.model';

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.scss']
})
export class CreateOrderComponent {

  data:any[] = []
  dealerData: any
  loading: boolean = false;
  textColor: string = 'blue';
  bgColor: string = 'white';

  selectedProducts : {product: Product, quantity: number}[] = [];
  totalPrice: number = 0;

  orderForm = new FormGroup({
    openAccountOrder: new FormControl(false),
    orderItems: new FormArray([
    ])
  })

  constructor(
    private formBuilder: FormBuilder,
    private productService: ProductService,
    private categoryService: CategoryService,
    private orderService: OrderService,
    private router: Router,
    private toastr: ToastrService,
    private dealerService: DealerService){
  }

  ngOnInit(): void {
    this.load();
  }
  
  selectProduct(product: Product, quantityval: any, id:any){
    let existingProduct = this.selectedProducts.find(p => p.product.productId === product.productId)

    if(existingProduct){
      existingProduct.quantity += parseInt(quantityval);
    } else {
      product.productId = id
      this.selectedProducts.push({product: product , quantity: parseInt(quantityval)});
    }
    this.calculateTotalPrice();
    console.log(this.selectedProducts);
  }

  calculateTotalPrice() {
    this.totalPrice = this.selectedProducts.reduce((total, product) => {
      return total + product.product.price * product.quantity;
    }, 0);
  }

  removeProduct(index: number) {
    this.selectedProducts.splice(index, 1);
    this.calculateTotalPrice(); // Toplam fiyatı yeniden hesaplayın
  }


  load(){
    this.loading = true;
    this.productService.get().subscribe((response:any) => {
      const responseData = response.response;
      this.data = responseData.$values;
      this.loading = false;
    })

    this.loading = true;
    this.dealerService.getByDealer().subscribe({
      next: (data:any) => {
        this.dealerData = data.response;
      },
      error: err => {
        this.toastr.error(err.message, 'Error');
      }
    })
    this.loading = false;
  }

  toggleOpenAccountOrder() {
    const openAccountOrderControl = this.orderForm.get('openAccountOrder');
    openAccountOrderControl?.setValue(!openAccountOrderControl?.value);

    if (this.textColor === 'blue') {
      this.textColor = 'white';
      this.bgColor = 'blue';
    } else {
      this.textColor = 'blue';
      this.bgColor = 'white';
    }
  }
  

  onSubmit(){
    const orderItemsArray = this.orderForm.get('orderItems') as FormArray;
    orderItemsArray.clear();

    this.selectedProducts.forEach(selectedProduct => {
      console.log(selectedProduct.product.productId)
      orderItemsArray.push(this.formBuilder.group({
        productId: selectedProduct.product.productId,
        quantity: selectedProduct.quantity
      }));
    });

    console.log(this.orderForm.value);
    this.orderService.create(this.orderForm.value).subscribe({
      next: (data:any) => {
        if(data.success === 'false'){
          console.log(data);
          this.toastr.error(data.message, 'Error')
        } 
        else {
          this.toastr.info('Item added successfully.', data.message);
          console.log(data)
          this.router.navigate(['order/list'])
        }
      }
    })
  }
}
