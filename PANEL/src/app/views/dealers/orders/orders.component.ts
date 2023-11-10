import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/services/dealer.service';
import { StorageService } from 'src/app/services/storage.service';
import { AuthService } from 'src/app/services/auth.service';
import { ReportService } from 'src/app/services/report.service';
import { ChartData } from 'chart.js';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  data:any[] = []
  loading: boolean = false;
  dealerId!:number;
  public Role:any;

  chartBarDataDayDealer: ChartData = {
    labels: ['Day 1', 'Day 2', 'Day 3', 'Day 4', 'Day 5', 'Day 6', 'Day 7'],
    datasets: [
      {
        label: 'Sipariş Sayısı',
        data: [],
      },
    ],
  };

  chartBarDataWeekDealer : ChartData = {
    labels: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
    datasets: [
      {
        label: 'Sipariş Sayısı',
        data: [],
      },
    ],
  };

  chartBarDataMonthDealer : ChartData = {
    labels: ['Month 1', 'Month 2', 'Month 3', 'Month 4', 'Month 5', 'Month 6', 'Month 7', 'Month 8', 'Month 9', 'Month 10', 'Month 11', 'Month 12'],
    datasets: [
      {
        label: 'Sipariş Sayısı',
        data: [],
      },
    ],
  };

  constructor(
    private reportService: ReportService,
    private dealerService: DealerService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute){}

    ngOnInit(): void {
      this.Role = window.sessionStorage.getItem('role');
      this.route.params.subscribe((params) => {
        const id = params['id'];
        this.dealerId = +id.replace(':', '');
      })
      this.load();
    }
    
    load(){
      this.loading = true;
      this.dealerService.getByAdmin(this.dealerId).subscribe((response:any) => {
        const responseData = response.response;
        this.data = responseData.orders.$values;
        console.log(this.data)
        this.loading = false;
      })
      if (this.Role === 'admin') {
        this.reportService.createDealer('Daily', this.dealerId).subscribe((response:any) => {
          const responseData = response.response;
          console.log(responseData.$values);
          this.chartBarDataDayDealer.datasets[0].data = responseData.$values;
          this.chartBarDataDayDealer = { ...this.chartBarDataDayDealer };
        })
    
        this.reportService.createDealer('Monthly', this.dealerId).subscribe((response:any) => {
          const responseData = response.response;
          console.log(responseData.$values);
          this.chartBarDataMonthDealer.datasets[0].data = responseData.$values;
          this.chartBarDataMonthDealer = { ...this.chartBarDataMonthDealer };
        })
    
        this.reportService.createDealer('Weekly',  this.dealerId).subscribe((response:any) => {
          const responseData = response.response;
          console.log(responseData.$values);
          this.chartBarDataWeekDealer.datasets[0].data = responseData.$values;
          this.chartBarDataWeekDealer = { ...this.chartBarDataWeekDealer };
        })
      }
      this.loading = false;
    }
}
