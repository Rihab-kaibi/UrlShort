import { Component ,OnInit , ChangeDetectorRef } from '@angular/core';
import { UrlShortenerService } from 'src/services/url-shortener.service';

@Component({
  selector: 'app-ip-info',
  templateUrl: './ip-info.component.html',
  styleUrls: ['./ip-info.component.scss']
})
export class IpInfoComponent implements OnInit  {
  countryVisits: any[] = [];

  constructor(private countryVisitService: UrlShortenerService , private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.countryVisitService.getCountryVisitsSummary().subscribe(
      data => {
        this.countryVisits = data;
        console.log(data);
        this.cdr.detectChanges(); // Manually trigger change detection
      },
      error => {
        console.error('Error loading data:', error);
      }
    );
  }
  
}
