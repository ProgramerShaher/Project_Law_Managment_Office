import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Client {
  id?: number;
  fullName: string;
  birthDate: string | Date;
  urlImageNationalId: string;
  clientType: number;
  clientRoleId: number;
  email?: string;
  phoneNumber: string;
  address?: string;
}

export interface ClientRole {
  id: number;
  name: string;
}

// واجهة جديدة متوافقة مع الـ API
export interface CreateClientRequest {
  fullName: string;
  birthDate: string;
  clientType: number;
  clientRoleId: number;
  phoneNumber: string;
  email?: string;
  address?: string;
  urlImageNationalId?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ClientsService {
  private apiUrl = `${environment.apiUrl}Clients`;
  private rolesUrl = `${environment.apiUrl}ClientRoles`;

  constructor(private http: HttpClient) { }

  // === Client Roles Management ===
  getClientRoles(): Observable<ClientRole[]> {
    return this.http.get<ClientRole[]>(this.rolesUrl);
  }

  createClientRole(roleName: string): Observable<ClientRole> {
    return this.http.post<ClientRole>(this.rolesUrl, { name: roleName });
  }

  // === Clients Management ===
  getClients(): Observable<Client[]> {
    return this.http.get<Client[]>(this.apiUrl);
  }

  getClientById(id: number): Observable<Client> {
    return this.http.get<Client>(`${this.apiUrl}/${id}`);
  }

  createClient(clientData: CreateClientRequest): Observable<Client> {
    // تحويل clientType إلى number إذا كان string
    const requestData = {
      ...clientData,
      clientType: Number(clientData.clientType),
      clientRoleId: Number(clientData.clientRoleId),
      urlImageNationalId: clientData.urlImageNationalId || null
    };

    console.log('🔄 إرسال بيانات العميل للـ API:', JSON.stringify(requestData, null, 2));

    return this.http.post<Client>(this.apiUrl, requestData, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  updateClient(id: number, clientData: CreateClientRequest): Observable<Client> {
    // تحويل clientType إلى number إذا كان string
    const requestData = {
      ...clientData,
      clientType: Number(clientData.clientType),
      clientRoleId: Number(clientData.clientRoleId),
      urlImageNationalId: clientData.urlImageNationalId || null
    };

    console.log('🔄 إرسال بيانات التحديث للـ API:', JSON.stringify(requestData, null, 2));

    return this.http.put<Client>(`${this.apiUrl}/${id}`, requestData, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  deleteClient(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
