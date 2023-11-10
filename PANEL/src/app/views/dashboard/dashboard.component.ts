import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';

import { DashboardChartsData, IChartProps } from './dashboard-charts-data';
import { StorageService } from 'src/app/services/storage.service';
import { AuthService } from 'src/app/services/auth.service';
import { ReportService } from 'src/app/services/report.service';
import { ChartData } from 'chart.js';
import { ChartjsModule } from '@coreui/angular-chartjs';

@Component({
  templateUrl: 'dashboard.component.html',
  styleUrls: ['dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  loading: boolean = false;
  public Role:any;

  chartBarDataDay: ChartData = {
    labels: ['Label 1', 'Label 2', 'Label 3', 'Label 4', 'Label 5', 'Label 6', 'Label 7'],
    datasets: [
      {
        label: 'Sipariş Sayısı',
        data: [],
      },
    ],
  };

  chartBarDataWeek: ChartData = {
    labels: ['Label 1', 'Label 2', 'Label 3', 'Label 4'],
    datasets: [
      {
        label: 'Sipariş Sayısı',
        data: [],
      },
    ],
  };

  chartBarDataMonth: ChartData = {
    labels: ['Label 1', 'Label 2', 'Label 3', 'Label 4', 'Label 5', 'Label 6', 'Label 7', 'Label 8', 'Label 9', 'Label 10', 'Label 11', 'Label 12'],
    datasets: [
      {
        label: 'Sipariş Sayısı',
        data: [],
      },
    ],
  };

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
    private storage:StorageService,
    private auth:AuthService) {
  }

  ngOnInit(): void {
    this.Role = window.sessionStorage.getItem('role');
    this.load();
  }

  load(){
    this.loading = true;
    if (this.Role === 'admin') {
      this.reportService.create('Daily').subscribe((response:any) => {
        const responseData = response.response;
        console.log(responseData.$values);
        this.chartBarDataDay.datasets[0].data = responseData.$values;
        this.chartBarDataDay = { ...this.chartBarDataDay };
      })
  
      this.reportService.create('Monthly').subscribe((response:any) => {
        const responseData = response.response;
        console.log(responseData.$values);
        this.chartBarDataMonth.datasets[0].data = responseData.$values;
        this.chartBarDataMonth = { ...this.chartBarDataMonth };
      })
  
      this.reportService.create('Weekly').subscribe((response:any) => {
        const responseData = response.response;
        console.log(responseData.$values);
        this.chartBarDataWeek.datasets[0].data = responseData.$values;
        this.chartBarDataWeek = { ...this.chartBarDataWeek };
      })
    }    
    if (this.Role === 'dealer') {
      var sessionId = window.sessionStorage.getItem('id');
      this.reportService.createDealer('Daily', Number(sessionId)).subscribe((response:any) => {
        const responseData = response.response;
        console.log(responseData.$values);
        this.chartBarDataDayDealer.datasets[0].data = responseData.$values;
        this.chartBarDataDayDealer = { ...this.chartBarDataDayDealer };
      })
  
      this.reportService.createDealer('Monthly', Number(sessionId)).subscribe((response:any) => {
        const responseData = response.response;
        console.log(responseData.$values);
        this.chartBarDataMonthDealer.datasets[0].data = responseData.$values;
        this.chartBarDataMonthDealer = { ...this.chartBarDataMonthDealer };
      })
  
      this.reportService.createDealer('Weekly',  Number(sessionId)).subscribe((response:any) => {
        const responseData = response.response;
        console.log(responseData.$values);
        this.chartBarDataWeekDealer.datasets[0].data = responseData.$values;
        this.chartBarDataWeekDealer = { ...this.chartBarDataWeekDealer };
      })
    }    
    this.loading = false;
  }
}
