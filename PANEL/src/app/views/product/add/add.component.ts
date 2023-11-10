import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit{

  data:any[] = []
  loading: boolean = false;

  productForm = new FormGroup({
    productName: new FormControl(''),
    productDescription: new FormControl(''),
    price: new FormControl(''),
    stockQuantity: new FormControl(''),
    categoryId: new FormControl('Select Category'),
  })

  constructor(
    private productService: ProductService,
    private router: Router,
    private toastr: ToastrService,
    private categoryService: CategoryService){
  }

  ngOnInit(): void {
    this.load();
  }

  load(){
    this.loading = true;
    this.categoryService.get().subscribe((response:any) => {
      const responseData = response.response;
      this.data = responseData.$values;
      this.loading = false;
    })
  }

  onSubmit(){
    console.log(this.productForm.value);
    console.log('work');
    this.productService.add(this.productForm.value).subscribe({
      next: (data:any) => {
        if(data.success === false){
          console.log(data);
          this.toastr.error(data.message, 'Error')
        } 
        else {
          this.toastr.info('Item added successfully.', data.message);
          console.log(data)
          this.router.navigate(['product/list'])
        }
      },
      error: err => {
        this.toastr.error(err.message, 'Error')
      }
    })
  }
}
