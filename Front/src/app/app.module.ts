import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppUrlComponent } from './app-url/app-url.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AppRoutingModule } from './app-routing.module';
import { DetailsComponent } from './details_dashboard/details.component';
import { DashboardBarComponent } from './dashboard-bar/dashboard-bar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Importez les modules
import { NgChartsModule } from 'ng2-charts';
import { AccumulationChartModule } from '@syncfusion/ej2-angular-charts';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { NgApexchartsModule } from 'ng-apexcharts';
import { Routes, RouterModule } from '@angular/router';
// MDB Modules
import { MdbAccordionModule } from 'mdb-angular-ui-kit/accordion';
import { MdbCarouselModule } from 'mdb-angular-ui-kit/carousel';
import { MdbCheckboxModule } from 'mdb-angular-ui-kit/checkbox';
import { MdbCollapseModule } from 'mdb-angular-ui-kit/collapse';
import { MdbDropdownModule } from 'mdb-angular-ui-kit/dropdown';
import { MdbFormsModule } from 'mdb-angular-ui-kit/forms';
import { MdbModalModule } from 'mdb-angular-ui-kit/modal';
import { MdbPopoverModule } from 'mdb-angular-ui-kit/popover';
import { MdbRadioModule } from 'mdb-angular-ui-kit/radio';
import { MdbRangeModule } from 'mdb-angular-ui-kit/range';
import { MdbRippleModule } from 'mdb-angular-ui-kit/ripple';
import { MdbScrollspyModule } from 'mdb-angular-ui-kit/scrollspy';
import { MdbTabsModule } from 'mdb-angular-ui-kit/tabs';
import { MdbTooltipModule } from 'mdb-angular-ui-kit/tooltip';
import { MdbValidationModule } from 'mdb-angular-ui-kit/validation';
 
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DetailsShortUrlComponent } from './details-short-url/details-short-url.component';
import { PieChartComponent } from './pie-chart/pie-chart.component';
import { BarChartComponent } from './bar-chart/bar-chart.component';
import { GoogleChartsModule } from 'angular-google-charts';
import { MapComponent } from './map/map.component';
import { IpInfoComponent } from './ip-info/ip-info.component';
import { BulkUrlShortenerComponent } from './bulk-url-shortener/bulk-url-shortener.component';

@NgModule({
  declarations: [
 
    AppComponent,
    AppUrlComponent,
    DashboardComponent,
    DetailsComponent,
    DashboardBarComponent,
    DetailsShortUrlComponent,
    PieChartComponent,
    BarChartComponent,
    MapComponent,
    IpInfoComponent,
    BulkUrlShortenerComponent
     
     
  ],
  imports: [
    BrowserModule,
    GoogleChartsModule,
    NgChartsModule,
    NgApexchartsModule,
    AccumulationChartModule ,
    BrowserAnimationsModule,
    MdbAccordionModule,
    MdbCarouselModule,
    MdbCheckboxModule,
    MdbCollapseModule,
    MdbDropdownModule,
    MdbFormsModule,
    MdbModalModule,
    MdbPopoverModule,
    MdbRadioModule,
    MdbRangeModule,
    MdbRippleModule,
    MdbScrollspyModule,
    MdbTabsModule,
    MdbTooltipModule,
    MdbValidationModule,
    BrowserModule,
    ReactiveFormsModule,
    FormsModule, 
    HttpClientModule, AppRoutingModule, BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
