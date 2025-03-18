import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../../models/response';

@Injectable({

  providedIn: 'root'
})
export abstract  class BaseService<T> {

  protected abstract baseUrl: string; // Agora pode ser abstrato corretamente

  constructor(protected http: HttpClient)
  {

  }

  getAll(): Observable<T[]> {
    return this.http.get<T[]>(`${this.baseUrl}`);
  }

  getById(id: number | string): Observable<Response<T>> {
    return this.http.get<Response<T>>(`${this.baseUrl}/${id}`);
  }

  create(item: T): Observable<Response<T>> {
    return this.http.post<Response<T>>(`${this.baseUrl}`, item);
  }

  update(id: number | string, item: T): Observable<Response<T>> {
    return this.http.put<Response<T>>(`${this.baseUrl}/${id}`, item);
  }

  delete(id: number | string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
