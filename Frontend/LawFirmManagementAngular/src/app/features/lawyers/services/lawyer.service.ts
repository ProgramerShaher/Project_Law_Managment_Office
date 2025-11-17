import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from './../../../../environments/environment';

export enum LawyerType {
  Trainee = 1,
  Consultant = 2,
  Expert = 3,
  Other = 4
}

export interface Lawyer {
  id?: number;
  fullName: string;
  phoneNumber: string;
  address: string;
  identityImagePath?: string;
  qualificationDocumentPath?: string;
  email?: string;
  dateOfBirth?: Date;
  type: LawyerType;
}
export interface LawyerCreate {
  fullName: string;
  phoneNumber: string;
  address: string;
  identityImagePath?: string;
  qualificationDocumentPath?: string;
  email?: string;
  dateOfBirth?: Date;
  type: LawyerType;
}

@Injectable({
  providedIn: 'root'
})
export class LawyerService {
  private apiUrl = `${environment.apiUrl}Lawyers`;

  constructor(private http: HttpClient) { }

  getAllLawyers(): Observable<Lawyer[]> {
    return this.http.get<Lawyer[]>(this.apiUrl);
  }

  getLawyerById(id: number): Observable<Lawyer> {
    return this.http.get<Lawyer>(`${this.apiUrl}/${id}`);
  }

  // Accept either FormData (for file uploads) or a plain object (JSON)
  createLawyer(payload: LawyerCreate): Observable<Lawyer> {
    return this.http.post<Lawyer>(this.apiUrl, payload, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
  }

  updateLawyer(id: number, payload: any): Observable<Lawyer> {
    return this.http.put<Lawyer>(`${this.apiUrl}/${id}`, payload, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
  }

  deleteLawyer(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
