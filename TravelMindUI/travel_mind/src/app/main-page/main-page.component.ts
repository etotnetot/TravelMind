import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { Router } from '@angular/router';
import {RouterModule} from '@angular/router';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms'; 
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [NavbarComponent, RouterModule, ReactiveFormsModule],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.css'
})

export class MainPageComponent implements OnInit {
  constructor(private router: Router, private fb: FormBuilder, private http: HttpClient) {
    this.tripForm = this.fb.group({
      departureCity: [''],
      arrivalCity: [''],
      departureDate: [''],
      returnDate: [''],
      ownCar: [false]
    });
  }
  
  hotTours: any[] = [];
  isLoading = true;

  tripForm: FormGroup;
  route: any = null;


  ngOnInit(): void {
    this.tripForm = this.fb.group({
      departureCity: ['', Validators.required],
      arrivalCity: ['', Validators.required],
      departureDate: ['', Validators.required],
      car: [false],
      publicTransport: [false]
    });
  }

  onSubmit(): void {
    if (this.tripForm.valid) {
      const formData = this.tripForm.value;

      this.getTripPlan(formData).subscribe((data) => {
        this.route = data;
        this.initializeMap(data);
      });
    }
  }

  getTripPlan(formData: any): Observable<any> {
    // В данном случае предполагается, что API возвращает маршрут в JSON
    return this.http.post<any>('/api/get-trip-plan', formData); // API для получения маршрута
  }

  initializeMap(routeData: any): void {
    // Инициализация карты с использованием библиотеки (например, Google Maps, Leaflet)
    // const map = new google.maps.Map(document.getElementById('map') as HTMLElement, {
    //   center: { lat: 44.0, lng: 37.0 }, // Начальная позиция карты
    //   zoom: 6
    // });

    const routePath = routeData.TransportPlan.map((item: any) => {
      return { lat: item.latitude, lng: item.longitude }; // Координаты остановок
    });

    // const routeLine = new google.maps.Polyline({
    //   path: routePath,
    //   geodesic: true,
    //   strokeColor: '#00FF00',
    //   strokeOpacity: 1.0,
    //   strokeWeight: 3
    // });
    
    // routeLine.setMap(map);
  }

  // ngOnInit(): void {
    // this.toursService.getHotTours().subscribe(
    //   (data) => {
    //     this.hotTours = data;
    //     this.isLoading = false;
    //   },
    //   (error) => {
    //     console.error('Ошибка при загрузке горячих туров:', error);
    //     this.isLoading = false;
    //   }
    // );
  // }
}