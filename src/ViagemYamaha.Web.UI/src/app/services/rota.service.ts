import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { PostRotaRequest } from '../models/postRotaRequest.model';
import { GetRotaResponse } from '../models/getRotaResponse.model';

@Injectable()
export class RotaService {

    headers = new HttpHeaders().set('Content-Type', 'application/json');
    apiUrl: string =  environment.apiUrl + 'rotas';

    constructor(private http: HttpClient) {}

    obterMelhorRota(origem: string, destino: string): Observable<GetRotaResponse> {
        return this.http.get<GetRotaResponse>(`${this.apiUrl}?origem=${origem}&destino=${destino}`);
    }

    adicionarRota(data: PostRotaRequest): Observable<any>
    {
        return this.http.post(this.apiUrl,data, { headers: this.headers });
    }
}