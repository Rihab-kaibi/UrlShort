import { UrlShortenerService } from 'src/services/url-shortener.service';
import { Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
import * as mapboxgl from 'mapbox-gl';
 import { HttpClient  } from '@angular/common/http';
@Component({
 selector: 'app-map',
 templateUrl: './map.component.html',
 styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {
  private map: mapboxgl.Map;
  private geoJsonData: any; // Store your GeoJSON data here

  constructor(private apiService: UrlShortenerService) {}

  ngOnInit() {
    // Initialize the map
    mapboxgl.accessToken = 'pk.eyJ1IjoicmloYWIta2FpYmk5NyIsImEiOiJjbG00cG85NnA0NHozM2pwdnZ0dm9wYjZyIn0.jjYebJ7vfVjAIHX8VqB_Vg';
    this.map = new mapboxgl.Map({
      container: 'map',
      style: 'mapbox://styles/mapbox/streets-v11',
      center: [10.7822, 36.4692], // Use the coordinates of one of your locations as the initial center
      zoom: 9
    });

    // Fetch data from your API and format it into GeoJSON
    this.apiService.getUrlsWithGeolocation().subscribe((result) => {
      this.geoJsonData = this.formatDataToGeoJSON(result[0].ipInfo);
      this.displayDataOnMap( );
      console.log(result);
      console.log('GeoJSON Data:', this.geoJsonData);

    });
  }

  // Format your data into GeoJSON
  formatDataToGeoJSON(data: any): any {
    debugger
    const features = data.map((item) => ({
      type: 'Feature',
      geometry: {
        type: 'Point',
        coordinates: [item.longtitude, item.latitude]
      },
      properties: {

        // city: item.ipInfo.city,
        // country: item.ipInfo.country,
        // continent: item.ipInfo.continent
      }
    }));

    return {
      type: 'FeatureCollection',
      features: features
    };
  }

 // Display GeoJSON data on the map with icons
displayDataOnMap() {
  this.map.on('load', () => {
    // Add a GeoJSON source to the map
    this.map.addSource('geojson-data', {
      type: 'geojson',
      data: this.geoJsonData
    });

    // Add a layer to display icons as markers
    this.map.addLayer({
      id: 'geojson-data-layer',
      type: 'symbol',
      source: 'geojson-data',
      layout: {
        'icon-image': 'custom-icon',  
        'icon-allow-overlap': true
      }
    });
debugger
    // Loop through features and create markers with popups
    this.geoJsonData.features.forEach((feature) => {
      const coordinates = feature.geometry.coordinates;
      if (coordinates && !isNaN(coordinates[0]) && !isNaN(coordinates[1])) {
        // Create a popup for each feature
        const popup = new mapboxgl.Popup()
          .setHTML(
            `<h3>${feature.properties.url}</h3>` +
            `<p>City: ${feature.properties.city}</p>` +
            `<p>Country: ${feature.properties.country}</p>` +
            `<p>Continent: ${feature.properties.continent}</p>`
          );

        // Add a click event to show the popup when a marker is clicked
        new mapboxgl.Marker()
          .setLngLat(coordinates)
          .setPopup(popup)
          .addTo(this.map);
      } else {
        console.warn('Invalid coordinates:', coordinates);
      }
    });
  });
}
}
