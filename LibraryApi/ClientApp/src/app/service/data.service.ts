import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DataService } from '../service/interface/data.service';

@Injectable()
export class LibraryDataService<T> implements DataService<T> {

  constructor(private http: HttpClient, private url: string, private entityIdUrl: string,
    @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + url;
  }

  getAll<T>(): Observable<object> {
    return this.http.get<Array<T>>(this.url + 'All');
  }

  delete(id: number) {
    return this.http.delete(this.url + this.entityIdUrl + id);
  }

  create<T>(entityToAdd: T) {
    return this.http.post(this.url, entityToAdd);
  }

  update<T>(entityToUpdate: T) {
    return this.http.put(this.url, entityToUpdate);
  }
}
