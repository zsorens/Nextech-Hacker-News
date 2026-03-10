import { TestBed, ComponentFixture } from '@angular/core/testing';
import { App } from './app';
import { HackerNewsApiProxy } from '../proxies/hacker-news-api.proxy';
import { of, throwError } from 'rxjs';
import { vi } from 'vitest';

const mockStories = [
  { id: 1, title: 'Story One', url: 'https://example.com', time: 1773096440 },
  { id: 2, title: 'Story Two', url: null, time: 1773096440 },
];

const mockProxy = {
  getNewStories: vi.fn().mockReturnValue(of(mockStories))
};

describe('App', () => {
  let component: App;
  let fixture: ComponentFixture<App>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App],
      providers: [
        { provide: HackerNewsApiProxy, useValue: mockProxy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(App);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should render title', async () => {
    await fixture.whenStable();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Hacker News App - Challenge');
  });

  it('should load stories on init', () => {
    expect(mockProxy.getNewStories).toHaveBeenCalled();
    expect(component.stories().length).toBe(2);
  });

  it('should start on page 1', () => {
    expect(component.page()).toBe(1);
  });

  it('should increment page on nextPage()', () => {
    component.nextPage();
    expect(component.page()).toBe(2);
  });

  it('should not go below page 1 on prevPage()', () => {
    component.prevPage();
    expect(component.page()).toBe(1);
  });

  it('should filter stories by search query', () => {
    component.searchQuery.set('Story One');
    expect(component.filteredStories().length).toBe(1);
    expect(component.filteredStories()[0].title).toBe('Story One');
  });

  it('should show all stories when search is empty', () => {
    component.searchQuery.set('');
    expect(component.filteredStories().length).toBe(2);
  });

  it('should set error on API failure', () => {
    mockProxy.getNewStories.mockReturnValue(throwError(() => new Error('API down')));
    component.loadStories();
    expect(component.error()).toBeTruthy();
  });
});