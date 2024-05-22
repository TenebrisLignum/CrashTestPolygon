import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

interface WeatherForecast {
	date: string;
	temperatureC: number;
	temperatureF: number;
	summary: string;
}

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
	public forecasts: string = '';

	constructor(private http: HttpClient) { }

	ngOnInit() {
		this.getForecasts().subscribe(res => {
			console.log(res);
			this.forecasts = res;
		});
	}

	getForecasts(): Observable<any> {
		return this.http.get('https://localhost:44373/api/ping', { responseType: 'text' });
	}
}
