import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Country } from './country';

@Injectable()
@Component({
  selector: 'app-country',
  templateUrl: './country.component.html'
})
export class CountryComponent {
  public countries: Country[];
  public countryToEdit: Country;
  public countryNameToAdd: string;
  private url = "api/Country/";
  private baseLink: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseLink = baseUrl + this.url;
    this.getAll();
  }

  getAll() {
    return this.http.get<Country[]>(this.baseLink + 'All').subscribe(result => {
      this.countries = result;
    }, error => console.error(error));
  }

  delete(id: number) {
    return this.http.delete<boolean>(this.baseLink + '?countryId=' + id).subscribe(() => {
      this.getAll();
    }, error => console.error(error));
  }

  editCountry(country: Country) {
    this.countryToEdit = country;
  }

  save() {
    this.http.put(this.baseLink, this.countryToEdit).subscribe(() => {
      this.getAll();
    }, error => console.error(error));

    this.countryToEdit = undefined;
  }

  cancel() {
    this.countryToEdit = undefined;
  }

  add(newCountryName: string) {
    const countryToAdd = new Country(newCountryName);

    this.http.post(this.baseLink, countryToAdd).subscribe(() => {
      this.getAll();
    }, error => console.error(error));

    this.countryNameToAdd = '';
  }

  getCountryName(id: number): string {
    const country = this.countries.find(x => x.id === id);

    const result = country === undefined ? "" : country.name;

    return result;
  }
}
