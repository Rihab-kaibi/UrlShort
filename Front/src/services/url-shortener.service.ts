import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders,HttpParams } from '@angular/common/http';
import { Observable , throwError } from 'rxjs';
//import { ShorteningMethod } from './app-url/constants';
import { BulkUrlDto } from 'src/app/bulk-url-shortener/BulkUrlDto';
import { catchError } from 'rxjs/operators';

import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root' 
})
export class UrlShortenerService {
  private apiUrl = 'https://localhost:44341'; //  l'URL de  l'API

  constructor(private http: HttpClient) { }
 
  createShortUrl(longUrl: string): Observable<any> {
    var url = new URL(longUrl);
    var domain = url.hostname;
    return this.http.post(`${this.apiUrl}/shorturl`, { url: longUrl,name:domain });
  }
  createShortUrlCustom(longUrl: string,  ShortUrl: string): Observable<any> {
    var url = new URL(longUrl);
    var domain = url.hostname;
    return this.http.post(`${this.apiUrl}/customshorturl`, { url: longUrl, name: domain, customShortUrl: ShortUrl });
  }
  getUrlDetails1(shortUrl: string) {
    return this.http.get(`/api/url/${shortUrl}`); 
  }
  shortenUrls1(data: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/bulkshorten`, { data });
  }
  bulkShortenUrls(urls: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/bulkshorten`, urls);
  }
  
  generateQRCode(shortUrl: string): Observable<Blob> {
    //const endpoint = 'qrcodeimage'; 
    const url = `${this.apiUrl}/qrcodeimage/${shortUrl}`;
    debugger
    return this.http.get(url, { responseType: 'blob' });
  }
  getUrls11(page: number, pageSize: number): Observable<any> {
    // Faites la requête HTTP GET avec les paramètres page et pageSize
    const url = `${this.apiUrl}/api/urls/${page}/${pageSize}`;
    return this.http.get(url);
  }
  getUrls(page: number , pageSize: number, orderBy: string ): Observable<any> {
    const params = { page, pageSize, orderBy };
    const url = `${this.apiUrl}/api/urls`;

    return this.http.get(url, { params });
  }
  getUrlsWithGeolocation(): Observable<any[]> {
    const url = `${this.apiUrl}/api/urls-with-geolocation`;

    return this.http.get<any[]>(url);
  }
  getCountryVisitsSummary(): Observable<any[]> {
    const url =`${this.apiUrl}/api/country-visits-summary`;
    return this.http.get<any[]>(url);
  }
  getVisitCounts(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/api/visitcounts`);
  }
   getUserAgentInfoStatistics(url: string) {
    const params = {url};

    return this.http.get (`${this.apiUrl}/api/deviceinfo`, { params })
    .pipe(
      map((result) => {
        return result;
      })
    );
  }
  
  getUrlDetails5(shortUrl: string): Observable<any> {
    const url = `${this.apiUrl}/api/url/${shortUrl}`;  
    return this.http.get(url);
  }
  getUrlDetailsId(ShortUrlId: number): Observable<any> {
    const url = `${this.apiUrl}/api/url/${ShortUrlId}`;  
    return this.http.get(url);
  }
  getInfoUserAgent(ShortUrlId : number):Observable<any>{
   // const params ={shortUrlId};
    const url =`${this.apiUrl}/api/info-counter/${ShortUrlId}`;
    return this.http.get(url);
  }
  getUserAgentInfo(url: string): Observable<any> {
    // Construct the parameters for the GET request
    const params = {url};
 
    return this.http.get (`${this.apiUrl}/api/deviceinfo`, { params });
  }




  getUrls99(page = 1, pageSize = 10, orderBy = 'mostVisited'): Observable<any> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString())
      .set('orderBy', orderBy);

    return this.http.get(this.apiUrl, { params });
  }
  getVisitCount(shortUrl: string): Observable<number> {
    const url = `${this.apiUrl}/${shortUrl}`;
    return this.http.get<number>(url);
  }
  getUrls1(): Observable<any[]> {
    const url = `${this.apiUrl}/api/urls`;  
    return this.http.get<any[]>(url);
  }
 
}  

 