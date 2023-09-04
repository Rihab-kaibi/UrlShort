import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DetailsComponent } from './details_dashboard/details.component';
import { AppUrlComponent } from './app-url/app-url.component';
import { DetailsShortUrlComponent } from './details-short-url/details-short-url.component';
import { PieChartComponent } from './pie-chart/pie-chart.component';
import { MapComponent } from './map/map.component';
import { IpInfoComponent } from './ip-info/ip-info.component';
import { BulkUrlShortenerComponent } from './bulk-url-shortener/bulk-url-shortener.component';
import { BarChartComponent } from './bar-chart/bar-chart.component';
 const routes: Routes = [
  // { path: 'dashboard', component: DashboardComponent },
  { path: 'dashboard', component: DetailsComponent },
  { path: 'shorten', component: AppUrlComponent },
  { path: 'maps', component: MapComponent },
  // { path: 'details', component: DetailsComponent },
  // { path: '{details/53drv9Wb}', component: DetailsShortUrlComponent },
  { path: 'pie-chart/:ShortUrlId', component: PieChartComponent },
  {path : 'bulkshorten', component : BulkUrlShortenerComponent},
{path: 'VistitsTable' , component :IpInfoComponent },
{path :  'barChart' , component : BarChartComponent},
  {
    path: 'details/:ShortUrlId', component: DetailsShortUrlComponent
  },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
];
@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]

})
export class AppRoutingModule { }
