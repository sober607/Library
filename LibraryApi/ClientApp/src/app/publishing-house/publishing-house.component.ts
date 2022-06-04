import { HttpClient } from "@angular/common/http";
import { Component, Inject, Injectable, OnInit } from "@angular/core";
import { LibraryDataService } from "../service/data.service";
import { PublishingHouse } from "./publishing-house";

@Injectable()
@Component({
  selector: 'app-publishing-house',
  templateUrl: './publishing-house.component.html'
})
export class PublishingHouseComponent implements OnInit {
  private publishingHouse: PublishingHouse;
  private publishingHouses: PublishingHouse[];
  private publishingHouseToAdd: PublishingHouse;
  private publishingHouseToEdit: PublishingHouse;
  private publishingHouseName: string;
  private url = "api/PublishingHouse/";
  private dataService: LibraryDataService<PublishingHouse>;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.dataService = new LibraryDataService<PublishingHouse>(this.http, this.url, '?publishingHouseId=', baseUrl);
  }

  ngOnInit(): void {
    this.getAll();
    }

  getAll() {
    return this.dataService.getAll()
      .subscribe((data: PublishingHouse[]) => {
        this.publishingHouses = data
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

  editPublishingHouse(publHouse: PublishingHouse) {
    this.publishingHouseToEdit = publHouse;
  }

  save() {
    this.dataService.update(this.publishingHouseToEdit).subscribe(() => {
      this.getAll();
    }, error => {
      console.log(error)
    });

    this.publishingHouseToEdit = undefined;
  }

  cancel() {
    this.publishingHouseToEdit = undefined;
  }

  add(publishingHouseName: string) {
    this.publishingHouseToAdd = new PublishingHouse(publishingHouseName);

    this.dataService.create(this.publishingHouseToAdd).subscribe(() => {
      this.getAll();
    }, error => console.log(error));

    this.publishingHouseToAdd = undefined;
  }
}
