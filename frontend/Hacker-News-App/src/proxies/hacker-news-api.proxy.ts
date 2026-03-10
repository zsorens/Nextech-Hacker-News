import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';




export interface HackerNewsItem {
    id: number;
    deleted: boolean;
    type?: string;
    by?: string;
    time: number;
    text?: string;
    dead?: boolean;
    parent?: number;
    poll?: number;
    kids?: number[];
    url?: string;
    score?: number;
    title?: string;
    parts?: number[];
    descendants?: number;
}

export interface HackerNewsPreview {
    id: number;
    title?: string;
    url?: string;
    time?: number;
}

@Injectable({ providedIn: 'root' })
export class HackerNewsApiProxy {
    private readonly apiBase = environment.apiUrl;
    constructor(private http: HttpClient) { }

    /** GET /api/HackerNews/item/{id} */
    getItem(id: number): Observable<HackerNewsItem> {
        return this.http.get<HackerNewsItem>(`${this.apiBase}/item/${id}`);
    }

    /** GET /api/HackerNews/new?page=1&count=25 */
    getNewStories(page = 1, count = 25): Observable<HackerNewsPreview[]> {
        const params = new HttpParams()
            .set('page', page)
            .set('count', count);
        return this.http.get<HackerNewsPreview[]>(`${this.apiBase}/new`, { params });
    }
}
