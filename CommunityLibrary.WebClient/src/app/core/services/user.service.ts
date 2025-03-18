import { Response } from './../../models/response';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { IUser } from '../../models/Iuser';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService<IUser> {
  protected baseUrl = 'https://localhost:7093/api/user'; // Definição da URL específica
    constructor(http: HttpClient) {
      super(http);
    }


  override create(user: IUser): Observable<Response<IUser>> {
    return this.http.post<Response<IUser>>(`${this.baseUrl}/create`, user);
  }

  override getById(id: number | string): Observable<Response<IUser>> {
    return this.http.get<Response<IUser>>(`${this.baseUrl}/getbyId/${id}`);
  }

}
