import { UserAuth } from '../../models/userAuth';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UseAuthService {
  protected baseUrl = 'https://localhost:7093/api'; // Definição da URL específica
  constructor(private http: HttpClient) {
  }
  login(user: UserAuth): Observable<{ token: any }> {
    return this.http.post<{ token: any }>(`${this.baseUrl}/User/Auth`, user);
  }

}
