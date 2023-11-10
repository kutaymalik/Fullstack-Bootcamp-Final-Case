import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AddressService } from 'src/app/services/address.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent {

    addressForm = new FormGroup({
      addressLine1: new FormControl(''),
      addressLine2: new FormControl(''),
      city: new FormControl(''),
      county: new FormControl(''),
      postalCode: new FormControl(''),
  })

  constructor(
    private addressService: AddressService,
    private router: Router,
    private toastr: ToastrService){
  }

  onSubmit(){
    console.log(this.addressForm.value);
    console.log('work');
    this.addressService.add(this.addressForm.value).subscribe({
      next: (data:any) => {
        if(data.success === false){
          console.log(data);
          this.toastr.error(data.message, 'Error')
        } 
        else {
          this.toastr.info('Item added successfully.', data.message);
          console.log(data)
          this.router.navigate(['address/list'])
        }
      }
    })
  }
}
