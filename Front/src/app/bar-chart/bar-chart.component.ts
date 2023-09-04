import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';
import { UrlShortenerService } from 'src/services/url-shortener.service';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.scss']
})
export class BarChartComponent implements OnInit {
  chart: any;
  visitData: any[] = [];

  constructor(private visitCountService: UrlShortenerService) { }

  ngOnInit(): void {
    this.visitCountService.getVisitCounts().subscribe(data => {
      this.visitData = data;
      this.createChart();
    });
  }

  createChart(): void {
    const monthNames = [
      'Janvier', 'Février', 'Mars', 'Avril', 'Mai', 'Juin',
      'Juillet', 'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre'
    ];

    // Transformez vos données en un tableau qui regroupe les comptages par mois et année
    const groupedData = {};

    this.visitData.forEach(item => {
      const key = `${monthNames[item.month - 1]} ${item.year}`;
      if (!groupedData[key]) {
        groupedData[key] = 0;
      }
      groupedData[key] += item.count;
    });

    const labels = Object.keys(groupedData);
    const data = Object.values(groupedData);

    this.chart = new Chart('canvas', {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [{
          label: 'Visit count by month',
          data: data,
          backgroundColor: 'rgba(75, 192, 192, 0.2)',
          borderColor: 'rgba(75, 192, 192, 1)',
          borderWidth: 1
        }]
      },
      options: {
        responsive: true,
  maintainAspectRatio: false, 
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }
}
