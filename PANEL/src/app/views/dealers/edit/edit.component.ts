import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/services/dealer.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {

  dealerId!:number;
  
  dealerForm = new FormGroup({
    role: new FormControl(''),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    passwordHash: new FormControl(''),
    dateOfBirth: new FormControl(''),
    phoneNumber: new FormControl(''),
    dividend: new FormControl(0),
    openAccountLimit : new FormControl(0),
  })

  data:any[] = []

  constructor(
    private dealerService: DealerService,
    private router: Router,
    private route: ActivatedRoute,
    private storage: StorageService,
    private toastr: ToastrService){
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
        const id = params['id'];
        this.dealerId = +id.replace(':', '');
      }
    );
    this.load();
  }
  
  load(){
    this.dealerService.getByAdmin(this.dealerId).subscribe((response:any) => {
      const responseData = response.response;
      this.data = responseData;
      this.dealerForm.controls['role'].setValue(responseData.role);
      this.dealerForm.controls['firstName'].setValue(responseData.firstName);
      this.dealerForm.controls['lastName'].setValue(responseData.lastName);
      this.dealerForm.controls['email'].setValue(responseData.email);
      this.dealerForm.controls['passwordHash'].setValue(responseData.passwordHash);
      this.dealerForm.controls['dateOfBirth'].setValue(responseData.dateOfBirth);
      this.dealerForm.controls['phoneNumber'].setValue(responseData.phoneNumber);
      this.dealerForm.controls['dividend'].setValue(responseData.dividend);
      this.dealerForm.controls['openAccountLimit'].setValue(responseData.openAccountLimit);
    })
  }

  onSubmit(){
    console.log(this.dealerForm.value)
    this.dealerService.update(this.dealerId, this.dealerForm.value).subscribe({
      next: (data:any) => {
        if(data.success === 'false'){
          console.log(data);
          this.toastr.error(data.message, 'Error');
        }
        else {
          this.toastr.info('Item updated successfully.', data.message);
          this.router.navigate(['dealers/list'])
        }
      },
      error: err => {
        this.toastr.error(err.message, 'Error');
      }
    })
  }
}
