import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit, OnDestroy {
  data:any[] = []
  loading: boolean = false;

  constructor(
    private categoryService: CategoryService,
    private toastr: ToastrService,
    private router: Router){}

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

    isDelete(id:number){
      console.log('Delete function called with id:', id);
      if(confirm('Are you sure you want to delete this item?')){
        this.categoryService.delete(id).subscribe({
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
