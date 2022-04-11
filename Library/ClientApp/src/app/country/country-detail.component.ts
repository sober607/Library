import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CountryService } from './country.service';
import { Country } from './country';

@Component({
    templateUrl: './country-detail.component.html',
    providers: [CountryService]
})
export class CountryDetailComponent implements OnInit {

    id: number;
    country: Country;
    loaded: boolean = false;

    constructor(private countryService: CountryService, activeRoute: ActivatedRoute) {
        this.id = Number.parseInt(activeRoute.snapshot.params["countryId"]);
    }

    ngOnInit() {
        if (this.id) {
            this.countryService.getCountry(this.id)
                .subscribe((data: Country) => { this.country = data; this.loaded = true; });
        }
            
    }
}