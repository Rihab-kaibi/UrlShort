import { Component,ElementRef, ViewChild} from '@angular/core';
//import { UrlShortenerService } from '../url-shortener.service';
import { UrlShortenerService } from 'src/services/url-shortener.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

 @Component({
  selector: 'app-app-url',
  templateUrl: './app-url.component.html',
  styleUrls: ['./app-url.component.css']
 //template: '<img [src]="qrCodeImageUrl" alt="QR Code">',

})
export class AppUrlComponent {
  @ViewChild('targetElement') targetElement: ElementRef;

  longUrl: string="";
  shortUrl: string="";
  shortening : string="Random";
  //visitCount: number;
  qrCodeImageUrl: string = ''; // To hold the QR code image URL
  showQRCode: boolean = false;
  constructor(private urlShortenerService: UrlShortenerService, ) {   }
  setRandom(){
    this.shortening = "Random";
  }
  setCustom(){
    this.shortening = "Custom";
  }
  scrollToElement() {
    // Scroll to the target element
    this.targetElement.nativeElement.scrollIntoView({ behavior: 'smooth' });
  }
  createShortUrl() {
    this.urlShortenerService.createShortUrl(this.longUrl).subscribe(
      (response: any) => {
        console.log('Short URL created:', response);
        this.shortUrl = response.url;
      },
      error => {
        if (error.status === 400 && error.error && !error.error.isUnique) {
          // Show an alert indicating that the URL already exists in the database
          alert('URL already exists in the database. Please enter a different URL.');
        } else {
          console.error('Error creating short URL:', error);
        }
      }
    );
  }
  createShortUrlCustom() {
    this.urlShortenerService.createShortUrlCustom(this.longUrl, this.shortUrl).subscribe(
      (response: any) => {
        console.log('Custom Short URL created:', response);
        this.shortUrl = response.url;
      },
      error => {
        if (error.status === 400) {
          if (error.error === "Custom short code must be at least 8 characters long!") {
            alert('Custom short code must be at least 8 characters long!');
          } else if (error.error === "Custom short code is already in use.") {
            alert('Custom short code is already in use.');
          } else if (error.error === "Custom short code is required for custom URL shortening.") {
            alert('Custom short code is required for custom URL shortening.');
          } else {
            alert('An error occurred during custom short URL creation.');
          }
        } else {
          console.error('Error creating custom short URL:', error);
        }
      }
    );
  }
  
  
  createShortUrlCustom1() {
    this.urlShortenerService.createShortUrlCustom(this.longUrl, this.shortUrl).subscribe(
      (response: any) => {
        console.log('Short URL created:', response);
        this.shortUrl = response.url;
      },
      error => {
        if (error.status === 400 && error.error && !error.error.isUnique) {
          // Show an alert indicating that the URL already exists in the database
          alert('URL already exists in the database. Please enter a different URL.');
        } else {
          console.error('Error creating short URL:', error);
        }
      }
    );
  }
   generateQRCode() {
    console.log('Generating QR code for:', this.shortUrl);
     if (this.shortUrl) {
    debugger
      var shortForm:string = '';
    var x = this.shortUrl.split('/');
    shortForm = x[x.length-1];
    this.urlShortenerService.generateQRCode(shortForm).subscribe(
        (response) => {
          console.log('QR code image fetched successfully:', response);
debugger
        const imageUrl = URL.createObjectURL(response);
        this.blobToBase64(response).then(res => {
          console.log(res); // res is base64 now
        });
           this.qrCodeImageUrl = imageUrl;
           this.showQRCode=true;
         },
         (error) => {
         console.error('Error fetching QR code image:', error);
        }
       );
    }
  }
  
  
  
  hideQRCode() {
    this.qrCodeImageUrl = '';
    this.showQRCode = false;
  }
  createShortUrl1() {
    this.urlShortenerService.createShortUrl(this.longUrl).subscribe(
     response => {
        console.log('Short URL created:', response);
         this.shortUrl = response.url;
     },
      error => {
        console.error('Error creating short URL:', error);
     }
   );

   
 }
  
 createShortUrl2() {
   this.urlShortenerService.createShortUrl(this.longUrl).subscribe(
       (response: any) => {
           if (response.isUnique) {
               console.log('Short URL created:', response);
               this.shortUrl = response.url;
           } else {
               // Show an alert indicating that the URL is not unique
               alert('URL already exists. Please enter a different URL.');
           }
       },
       error => {
           console.error('Error creating short URL:', error);
       }
   );
}
blobToBase64(blob: Blob){
  const reader = new FileReader();
  reader.readAsDataURL(blob);
  return new Promise(resolve => {
    reader.onloadend = () => {
      resolve(reader.result);
    };
  });
};
}