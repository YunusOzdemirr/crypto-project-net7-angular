import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import User from '../models/UserModels/userModel';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  username: string = 'Yunus Özdemir';
  apiurl: string = 'https://jsonplaceholder.typicode.com/';
  constructor(private http: HttpClient) {}

  public getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiurl + 'users');
  }
}
