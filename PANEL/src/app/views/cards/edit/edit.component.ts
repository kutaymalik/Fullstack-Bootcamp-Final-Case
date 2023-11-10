import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CardService } from 'src/app/services/card.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit{
  cardId!:number;
  
  cardForm = new FormGroup({
    expenseLimit: new FormControl(''),
  })

  data:any[] = []

  constructor(
    private cardService: CardService,
    private router: Router,
    private route: ActivatedRoute,
    private storage: StorageService,
    private toastr: ToastrService){
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
        const id = params['id'];
        this.cardId = +id.replace(':', '');
      }
    );
    this.load();
  }
  
  load(){
    this.cardService.getById(this.cardId).subscribe((response:any) => {
      const responseData = response.response;
      this.data = responseData;
      this.cardForm.controls['expenseLimit'].setValue(responseData.expenseLimit);
    })
  }

  onSubmit(){
    this.cardService.update(this.cardId, this.cardForm.value).subscribe({
      next: (data:any) => {
        if(data.success === false){
          console.log(data);
          this.toastr.error(data.message, 'Error');
        }
        else {
          this.toastr.info('Item updated successfully.', data.message);
          this.router.navigate(['card/list'])
        }
      },
      error: err => {
        this.toastr.error(err.message, 'Error');
      }
    })
  }
}

