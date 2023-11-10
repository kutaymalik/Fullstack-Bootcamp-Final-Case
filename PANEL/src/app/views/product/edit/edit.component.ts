import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  productId!:number;

  productForm2 = new FormGroup({
    productName: new FormControl(''),
    productDescription: new FormControl(''),
    price: new FormControl(''),
    stockQuantity: new FormControl(''),
    categoryId: new FormControl('Select Category'),
  })

  data:any[] = []
  categoryData:any[] = []
  loading: boolean = false;

  constructor(
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute,
    private storage: StorageService,
    private toastr: ToastrService,
    private productService: ProductService,
  ){}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const id = params['id'];
      this.productId = +id.replace(':', '');
    })
    this.load();


  }

  load(){
    this.loading = true;
    this.categoryService.get().subscribe((response:any) => {
      const responseData = response.response;
      this.categoryData = responseData.$values;
      this.loading = false;
    })
    this.productService.getById(this.productId).subscribe((response:any) => {
      const responseData = response.response;
      this.data = responseData;
      this.productForm2.controls['productName'].setValue(responseData.productName);
      this.productForm2.controls['productDescription'].setValue(responseData.productDescription);
      this.productForm2.controls['price'].setValue(responseData.price);
      this.productForm2.controls['stockQuantity'].setValue(responseData.stockQuantity);
      this.productForm2.controls['categoryId'].setValue(responseData.id);
    })
  }

  onSubmit(){
    console.log(this.productForm2.value)
    this.productService.update(this.productId, this.productForm2.value).subscribe({
      next: (data:any) => {
        if(data.success === 'false'){
          console.log(data);
          this.toastr.error(data.message, 'Error');
        }
        else {
          this.toastr.info('Item updated successfully.', data.message);
          this.router.navigate(['product/list'])
        }
      }, error: err => {
        this.toastr.error(err.message, 'Error');
      }
    })
  }
}
