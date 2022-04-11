import { Component, OnInit } from '@angular/core';
import { CountryService } from './country.service';
import { Country } from './country';

@Component({
    templateUrl: './product-list.component.html',
    providers: [CountryService]
})
export class CountryListComponent implements OnInit {

    countries: Country[];
    constructor(private countryService: CountryService) { }

    ngOnInit() {
        this.countryService.getCountries().subscribe((data: Country[]) => this.countries = data);
    }
}