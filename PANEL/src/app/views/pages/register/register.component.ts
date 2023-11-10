import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { RegisterService } from '../../../services/register.service'
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  registerForm = new FormGroup({
    role: new FormControl('dealer'),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    passwordHash: new FormControl(''),
    dateOfBirth: new FormControl(''),
    phoneNumber: new FormControl(''),
    dividend: new FormControl(''),
    openAccountLimit: new FormControl(''),
})

  constructor(
    private registerService: RegisterService,
    private toastr: ToastrService,
    private router: Router) { }

  onSubmit(){
    console.log(this.registerForm.value);
    this.registerService.register(this.registerForm.value).subscribe({
      next: (data:any) => {
        if(data.success === false){
          console.log(data);
          this.toastr.error(data.message, 'Error')
        }
        else {
          this.toastr.info('Account created successfully.', data.message);
          console.log(data)
          this.router.navigate(['login'])
        }
      }, error: err => {
        this.toastr.error(JSON.stringify(err.error.errors), 'Error')
      }
    })
  }
}
