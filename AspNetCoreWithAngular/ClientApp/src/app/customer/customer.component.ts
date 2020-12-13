import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html'
})
  export class CustomerComponent {
  public customers: Customer[];

  constructor(http: HttpClient, @Inject('BASE_URL')baseUrl: string) {
    http.get<Customer[]>(baseUrl + 'customer').subscribe(result => {
      this.customers = result;
    }, error => console.error(error));
  }
}

class Customer {
  Id: number;
  FirstName: string;
  Name: string;

}
