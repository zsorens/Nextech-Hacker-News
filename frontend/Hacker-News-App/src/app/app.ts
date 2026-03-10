import { Component, computed, OnInit, signal } from '@angular/core';
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
  searchQuery = signal('');
  readonly pageSize = 25;

  filteredStories = computed(() => {
    const query = this.searchQuery().toLowerCase().trim();
    if (!query) return this.stories();
    return this.stories().filter(s => s.title?.toLowerCase().includes(query));
  });

  constructor(private api: HackerNewsApiProxy) { }

  ngOnInit(): void {
    this.loadStories();
  }

  onSearch(value: string) {
    this.searchQuery.set(value);
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
