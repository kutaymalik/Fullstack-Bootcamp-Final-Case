import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent{

  categoryForm = new FormGroup({
    name: new FormControl(''),
    description: new FormControl(''),
  })

  constructor(
    private categoryService: CategoryService,
    private router: Router,
    private toastr: ToastrService){
  }

  onSubmit(){
    console.log(this.categoryForm.value);
    console.log('work');
    this.categoryService.add(this.categoryForm.value).subscribe({
      next: (data:any) => {
        if(data.success === false){
          console.log(data);
          this.toastr.error(data.message, 'Error')
        } 
        else {
          this.toastr.info('Item added successfully.', data.message);
          console.log(data)
          this.router.navigate(['category/list'])
        }
      },
      error: err => {
        this.toastr.error(err.message, 'Error')
      }
    })
  }
}
