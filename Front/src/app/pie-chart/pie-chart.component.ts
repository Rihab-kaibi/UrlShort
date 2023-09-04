import { Component , OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute } from '@angular/router';
import { UrlShortenerService } from 'src/services/url-shortener.service';
import { Chart } from 'chart.js';
import { ChartType } from 'angular-google-charts';
@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.scss']
})
export class PieChartComponent implements OnInit {

  infoCounterData: any[];
  pieChart: any;
  ShortUrlId : number=149;
  constructor(private dataService: UrlShortenerService,     private route: ActivatedRoute
    ) { }

  ngOnInit(): void {
    // Récupérez le shortUrlId à partir de la route actuelle
    this.route.params.subscribe(params => {
      this.ShortUrlId = +params['ShortUrlId'];
      this.dataService.getInfoUserAgent(this.ShortUrlId).subscribe(data => {
        this.infoCounterData = data;
        this.createPieChart();
      });
    });
  }
 
  createPieChart(): void {
    if (this.infoCounterData && this.infoCounterData.length > 0) {
      const labels = ['Smartphone', 'Laptop', 'Tablet', 'Other'];
      const values = [
        this.infoCounterData[0].smartphone,
        this.infoCounterData[0].laptop,
        this.infoCounterData[0].tablet,
        this.infoCounterData[0].other,
      ];

      this.pieChart = new Chart('canvas', {
        type: 'pie',
        data: {
          labels: labels,
          datasets: [
            {
              data: values,
              backgroundColor: [
                'rgba(255, 99, 132, 0.5)',
                'rgba(54, 162, 235, 0.5)',
                'rgba(255, 206, 86, 0.5)',
                'rgba(75, 192, 192, 0.5)',  
              ],
            },
          ],
        },
        options: {
          responsive: true,
  maintainAspectRatio: false, 
        },
      });
    }
  }
}