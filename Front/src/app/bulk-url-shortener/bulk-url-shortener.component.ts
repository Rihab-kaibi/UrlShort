import { Component } from '@angular/core';
import { UrlShortenerService } from 'src/services/url-shortener.service';

@Component({
  selector: 'app-bulk-url-shortener',
  templateUrl: './bulk-url-shortener.component.html',
  styleUrls: ['./bulk-url-shortener.component.scss']
})
export class BulkUrlShortenerComponent {
  urls: any[] = [{ Url: '', Name: '' }];
  ShortUrls: any[] = [];

  constructor(private urlShortenerService: UrlShortenerService) { }

  bulkShorten() {
    this.urlShortenerService.bulkShortenUrls({ urls: this.urls })
      .subscribe(response => {
        this.ShortUrls = response; 
        console.log('this is the shortened urls ', JSON.stringify(this.ShortUrls));
      });
  }

  addUrl() {
    this.urls.push({ Url: '', Name: '' });
  }
}
