import { Component, Input, OnInit } from '@angular/core';
  
import * as L from 'leaflet';

@Component({
  selector: 'app-maps',
  templateUrl: './maps.component.html',
  styleUrls: ['./maps.component.css']
})
export class MapsComponent implements OnInit {
  private map!: L.Map;
  @Input() latitude!: any;
  @Input() longitude!: any;
  @Input() name!: any;

  constructor() { }

  ngOnInit(): void {
    this.initializeMap();
  }

  private initializeMap(): void {
    this.map = L.map('map', {
      center: [this.latitude, this.longitude],
      zoom: 13,
      dragging: false, // Desativa o recurso de arrastar
      zoomControl: false // Desativa o controle de zoom
    });

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors',
      maxZoom: 18
    }).addTo(this.map);

    L.marker([this.latitude, this.longitude]).addTo(this.map)
      .bindPopup(this.name)
      .openPopup();
  }
}
