import { Component ,OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UrlShortenerService } from 'src/services/url-shortener.service';
 @Component({
  selector: 'app-details-short-url',
  templateUrl: './details-short-url.component.html',
  styleUrls: ['./details-short-url.component.scss']
})
export class DetailsShortUrlComponent  implements OnInit {
  urlDetails: any; // Define the structure of the URL details
  ShortUrlId: number=1;
 
  constructor(
    private route: ActivatedRoute,
    private urlService: UrlShortenerService // Inject your URL service
  ) {}

  ngOnInit(): void {
    // Get the shortUrl route parameter
    this.route.params.subscribe((params) => {
      this.ShortUrlId = params['ShortUrlId'];

      // Fetch the URL details based on the shortUrl using your service
      this.fetchUrlDetails(this.ShortUrlId);
    });
  }

  fetchUrlDetails(ShortUrlId: number) {
    // Use your URL service to fetch URL details
    this.urlService.getUrlDetailsId(ShortUrlId).subscribe((data) => {
      this.urlDetails = data; // Update this.urlDetails with the fetched data
    
    });
       }
}