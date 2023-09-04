import { Component,OnInit , ViewChild } from '@angular/core';
import { UrlShortenerService } from 'src/services/url-shortener.service';
import { Router  } from '@angular/router';
 @Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit{
  urls: any[] = [];
  currentPage = 1;
  pageSize = 5;
  totalItems = 0;
  totalPages = 0;
  orderBy : string="latest"; 
   visitData: any[];
infoCounters: any[any]=[];
searchText: string = '';  


  constructor(private urlShortenerService: UrlShortenerService,private router: Router) { }
 
  // ngOnInit(): void {
    
  //   // Make an HTTP GET request to fetch the data
  //   this.urlShortenerService.getUrls().subscribe(data => {
  //     this.urls = data;
  //   });
  // }

  ngOnInit(): void {
    this.loadUrls();
  }

  loadUrls(): void {
     this.urlShortenerService.getUrls(this.currentPage, this.pageSize ,this.orderBy)
      .subscribe(data => {
        this.urls = data; // Assuming the API returns an array of objects with shortUrl and Visits attributes
         
      });
  }
  
  
  nextPage(): void {
    this.currentPage++;
    this.loadUrls();
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadUrls();
    }
  }
  showUrlDetails(ShortUrlId: number) {
     this.router.navigate(['/details', ShortUrlId]);
  }
  onColumnClick(Id: number) {
     this.router.navigate([`/pie-chart/${Id}`]);
    this.router.navigate([`/details/${Id}`]);
  }
   
}