import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Person, PersonCreateUpdate } from '../models/person.model';

@Injectable({
  providedIn: 'root',
})
export class PersonService {
  private baseUrl = 'https://localhost:7038/api/data';
  private http = inject(HttpClient);

  getAll(
    name?: string,
    gender?: string,
    maritalStatus?: string,
    isGraduated?: boolean,
    pageNumber: number = 1,
    pageSize: number = 10,
  ) {
    let params = new HttpParams().set('pageNumber', pageNumber).set('pageSize', pageSize);

    if (name) params = params.set('name', name);
    if (gender) params = params.set('gender', gender);
    if (maritalStatus) params = params.set('maritalStatus', maritalStatus);
    if (isGraduated !== undefined) params = params.set('isGraduated', isGraduated);

    return this.http.get<Person[]>(`${this.baseUrl}/GetAllPersonsData`, { params });
  }

  getById(id: number) {
    return this.http.get<Person>(`${this.baseUrl}/GetPersonDataById/${id}`);
  }

  create(data: PersonCreateUpdate) {
    return this.http.post(`${this.baseUrl}/CreatePersonData`, data);
  }

  update(id: number, data: PersonCreateUpdate) {
    return this.http.put(`${this.baseUrl}/UpdatePersonData/${id}`, data);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/DeletePersonData/${id}`);
  }
}
