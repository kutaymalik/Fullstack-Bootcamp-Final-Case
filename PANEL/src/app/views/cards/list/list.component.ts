import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CardService } from 'src/app/services/card.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit, OnDestroy {

  data:any[] = []
  loading: boolean = false;

  constructor(
    private cardService: CardService,
    private toastr: ToastrService,
    private router: Router){}

  ngOnInit(): void {
    this.load();
  }
  
  load(){
    this.loading = true;
    this.cardService.get().subscribe((response:any) => {
      const responseData = response.response;
      this.data = responseData.$values;
      this.loading = false;
    })
  }

  isDelete(id:number){
    console.log('Delete function called with id:', id);
    if(confirm('Are you sure you want to delete this item?')){
      this.cardService.delete(id).subscribe({
        next: (response: any) => {
          this.toastr.info('Item deleted successfully.', JSON.stringify(response));
          this.load();
          this.router.navigate(['card/list'])
        },
        error: (err) => {
          this.toastr.error(err, 'Error')
        }
      })
    }
  }

  ngOnDestroy(): void {
    
  }
}
