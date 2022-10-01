import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Country } from './country';

@Injectable()
export class CountryService {

    private url = "/api/Country";

    constructor(private http: HttpClient) {
    }

    findCountries(name: string) {
        return this.http.get(this.url + '/Find?countryName=' + name);
    }

    deleteCountry(id: number) {
        return this.http.delete(this.url + '/?countryId=' + id);
    }

    getCountry(id: number) {
        return this.http.get(this.url + '/?countryId=' + id);
    }

    getCountries() {
        return this.http.get(this.url + '/All');
    }

    getCountryByName(name: string) {
        return this.http.get(this.url + 'Name/' + name);
    }

    createProduct(country: Country) {
        return this.http.post(this.url, country);
    }
}