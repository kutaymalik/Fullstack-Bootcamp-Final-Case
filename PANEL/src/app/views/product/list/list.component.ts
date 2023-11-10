import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  data:any[] = []
  loading: boolean = false;

  constructor(
    private productService: ProductService,
    private toastr: ToastrService,
    private router: Router){}

    ngOnInit(): void {
      this.load();
    }

    load(){
      this.loading = true;
      this.productService.get().subscribe((response:any) => {
        const responseData = response.response;
        this.data = responseData.$values;
        this.loading = false;
      })
    }

    isDelete(id:number){
      if(confirm('Are you sure you want to delete this item?')){
        this.productService.delete(id).subscribe({
          next: (response: any) => {
            if(response.success === false){
              console.log(response);
              this.toastr.error(response.message, 'Error')
            }
            else {
              this.toastr.info('Item deleted successfully.', response.message);
              this.load();
              this.router.navigate(['category/list'])
            } 
          }
        })
      }
    }
  
    ngOnDestroy(): void {
      
    }
}
