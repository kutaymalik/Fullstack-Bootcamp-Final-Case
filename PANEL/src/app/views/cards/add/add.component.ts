import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { CardService } from '../../../services/card.service'
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent {

  cardForm = new FormGroup({
    cardNumber: new FormControl(''),
    expiryDate: new FormControl(''),
    cvv: new FormControl(''),
    expenseLimit: new FormControl(''),
    cardHolderType: new FormControl('credit'),
  })

  constructor(
    private cardService: CardService,
    private router: Router,
    private toastr: ToastrService){

  }

  onSubmit(){
    console.log(this.cardForm.value);
    console.log('work');
    this.cardService.add(this.cardForm.value).subscribe({
      next: (data:any) => {
        if(data.success === false){
          console.log(data);
          this.toastr.error(data.message, 'Error')
        } 
        else {
          this.toastr.info('Item added successfully.', data.message);
          console.log(data)
          this.router.navigate(['card/list'])
        }
      },
      error: err => {
        this.toastr.error(err.message, 'Error')
      }
    })
  }
}
