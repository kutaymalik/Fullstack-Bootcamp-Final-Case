import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AddressService } from 'src/app/services/address.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit{
  addressId!:number;

  address2Form = new FormGroup({
    addressLine1: new FormControl(''),
    addressLine2: new FormControl(''),
    city: new FormControl(''),
    county: new FormControl(''),
    postalCode: new FormControl(''),
  })

  data:any[] = []

  constructor(
    private addressService: AddressService,
    private router: Router,
    private route: ActivatedRoute,
    private storage: StorageService,
    private toastr: ToastrService
  ){}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const id = params['id'];
      this.addressId = +id.replace(':', '');
    })
    this.load();
  }

  load(){
    this.addressService.getById(this.addressId).subscribe((response:any) => {
      const responseData = response.response;
      this.data = responseData;
      this.address2Form.controls['addressLine1'].setValue(responseData.addressLine1);
      this.address2Form.controls['addressLine2'].setValue(responseData.addressLine2);
      this.address2Form.controls['city'].setValue(responseData.city);
      this.address2Form.controls['county'].setValue(responseData.county);
      this.address2Form.controls['postalCode'].setValue(responseData.postalCode);
    })
  }

  onSubmit(){
    this.addressService.update(this.addressId, this.address2Form.value).subscribe({
      next: (data:any) => {
        if(data.success === 'false'){
          console.log(data);
          this.toastr.error(data.message, 'Error');
        }
        else {
          this.toastr.info('Item updated successfully.', data.message);
          this.router.navigate(['address/list'])
        }
      }, error: err => {
        this.toastr.error(err.message, 'Error');
      }
    })
  }
}
