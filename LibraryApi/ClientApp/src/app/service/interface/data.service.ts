import { Observable } from "rxjs";

export interface DataService<T> {
  getAll<T>(): Observable<object>;
  delete(id: number);
  create<T>(entity: T);
  update<T>(entity: T);
}
