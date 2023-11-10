import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from 'src/app/services/category.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  categoryId!:number;

  categoryForm = new FormGroup({
    name: new FormControl(''),
    description: new FormControl(''),
  })

  data:any[] = []

  constructor(
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute,
    private storage: StorageService,
    private toastr: ToastrService
  ){}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const id = params['id'];
      this.categoryId = +id.replace(':', '');
    })
    this.load();
  }

  load(){
    this.categoryService.getById(this.categoryId).subscribe((response:any) => {
      const responseData = response.response;
      this.data = responseData;
      this.categoryForm.controls['name'].setValue(responseData.name);
      this.categoryForm.controls['description'].setValue(responseData.description);
    })
  }

  onSubmit(){
    this.categoryService.update(this.categoryId, this.categoryForm.value).subscribe({
      next: (data:any) => {
        if(data.success === 'false'){
          console.log(data);
          this.toastr.error(data.message, 'Error');
        }
        else {
          this.toastr.info('Item updated successfully.', data.message);
          this.router.navigate(['category/list'])
        }
      }, error: err => {
        this.toastr.error(err.message, 'Error');
      }
    })
  }
}
