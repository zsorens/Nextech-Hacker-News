import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';


const API_BASE = '/api/HackerNews';


export interface HackerNewsItem {
  id: number;
  type: string;
  title?: string;
  url?: string;
  text?: string;
  by?: string;
  score?: number;
  time?: number;
  kids?: number[];
  descendants?: number;
}

export interface HackerNewsPreview {
  id: number;
  title?: string;
  url?: string;
  by?: string;
  score?: number;
  time?: number;
  descendants?: number;
}

@Injectable({ providedIn: 'root' })
export class HackerNewsApiProxy {
  constructor(private http: HttpClient) {}

  /** GET /api/HackerNews/item/{id} */
  getItem(id: number): Observable<HackerNewsItem> {
    return this.http.get<HackerNewsItem>(`${API_BASE}/item/${id}`);
  }

  /** GET /api/HackerNews/new?page=1&count=25 */
  getNewStories(page = 1, count = 25): Observable<HackerNewsPreview[]> {
    const params = new HttpParams()
      .set('page', page)
      .set('count', count);
    return this.http.get<HackerNewsPreview[]>(`${API_BASE}/new`, { params });
  }
}
