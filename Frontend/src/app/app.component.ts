import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  title = 'Frontend';

  products: any[] = [];

  constructor(private http: HttpClient){}

  ngOnInit(): void {
    this.http.get(`https://localhost:7262/api/products?pageSize=10&sort=price`).subscribe((resp: any) => {
      console.log(resp);
      this.products = resp;
      console.log(this.products);
    });
  }


}
