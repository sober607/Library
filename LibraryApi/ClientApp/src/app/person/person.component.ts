import { Component, Inject, Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from './person';
import { Country } from '../country/country';
import { CountryComponent } from '../country/country.component';
import { LibraryDataService } from '../service/data.service';

@Injectable()
@Component({
  selector: 'app-person',
  templateUrl: './person.component.html'
})
export class PersonComponent implements OnInit {
  public persons: Person[];
  public countries: Country[];
  public personToEdit: Person;
  public personToAdd: Person;
  private firstName: string;
  private lastName: string;
  private dateOfBirth: Date;
  private countryId: number;
  private countrySelected: number;
  private url = "api/Person/";
  private countryUrl = "api/Country/";
  private baseLink: string;
  private errorText: string;
  private dataService: LibraryDataService<Person>;
  private countryDataService: LibraryDataService<Country>;

  constructor(private http: HttpClient, private countryComponent: CountryComponent,
    @Inject('BASE_URL') baseUrl: string) {
    this.dataService = new LibraryDataService<Person>(this.http, this.url, '?countryId=', baseUrl);
    this.countryDataService = new LibraryDataService<Country>(this.http, this.countryUrl, '?personId=', baseUrl);
  }

  ngOnInit() {
    this.getAll();
    this.countryDataService.getAll().subscribe((data: Country[]) => this.countries = data)
  }

  getAll() {
    return this.dataService.getAll()
      .subscribe((data: Person[]) => {
        this.persons = data
      }, error => {
        console.log(error)
      });
  }

  delete(id: number) {
    return this.dataService.delete(id).subscribe(() => {
      this.getAll();
    }, error => {
      console.log(error)
    })
  }

  editPerson(person: Person) {
    this.personToEdit = person;
  }

  save() {
    this.dataService.update(this.personToEdit).subscribe(() => {
      this.getAll();
    }, error => {
      console.log(error)
    } );

    this.personToEdit = undefined;
  }

  cancel() {
    this.personToEdit = undefined;
  }

  add(personFirstName: string, personLastName: string, personDateOfBirth: Date, personCountryOfBirth?: number) {
    this.personToAdd = new Person(personFirstName, personDateOfBirth, undefined, this.countrySelected, personLastName);

    this.dataService.create(this.personToAdd).subscribe(() => {
      this.getAll();
    }, error => console.log(error));

    this.personToAdd = undefined;
  }

  getCountryName(id: number) {
    const country = this.countries.find(x => x.id === id);

    const result = country === undefined ? "" : country.name;

    return result;
  }
}
