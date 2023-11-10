import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { StorageService } from 'src/app/services/storage.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  })

  constructor(
    private authService: AuthService,
    private router: Router,
    private storage: StorageService,
    private toastr: ToastrService
  ) {

  }
  onSubmit(){
    const{ email, password } = this.loginForm.value;
    console.log(this.loginForm.value);
    this.authService.login(email, password).subscribe({
      next: (data:any) => {
        if(data.success === 'false'){
          console.log(data.message)
          this.toastr.error(data.message, 'Error')
        }
        else {
          this.storage.saveUser(data)
          this.storage.saveTokens(data);
          this.router.navigate(['/dashboard'])
        }
      }
    })
  }
}
