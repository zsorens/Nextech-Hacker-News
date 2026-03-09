import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HackerNewsApiProxy, HackerNewsPreview } from '../proxies/hacker-news-api.proxy';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  stories = signal<HackerNewsPreview[]>([]);
  loading = signal(true);
  error = signal<string | null>(null);
  page = signal(1);
  readonly pageSize = 25;

  constructor(private api: HackerNewsApiProxy) { }

  ngOnInit(): void {
    this.loadStories();
  }

  loadStories() {
    this.loading.set(true);
    this.api.getNewStories(this.page(), this.pageSize).subscribe({
      next: (data) => { this.stories.set(data); this.loading.set(false); },
      error: (e) => { this.error.set(e.message); this.loading.set(false); }
    });
  }

  nextPage() {
    this.page.update(p => p + 1);
    this.loadStories();
  }

  prevPage() {
    if (this.page() > 1) {
      this.page.update(p => p - 1);
      this.loadStories();
    }
  }
}
