import { Injectable } from '@angular/core';
import { UserRegisterDto } from '../models/user/user-register-dto';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  public registerUser(userRegisterDto: UserRegisterDto) {
    return this.http.post<any>(environment.apiUrl + "account/register", userRegisterDto);
  }
}
