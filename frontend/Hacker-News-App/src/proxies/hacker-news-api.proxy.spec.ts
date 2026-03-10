import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HackerNewsApiProxy } from './hacker-news-api.proxy';
import { environment } from '../environments/environment';

describe('HackerNewsApiProxy', () => {
  let proxy: HackerNewsApiProxy;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [HackerNewsApiProxy]
    });

    proxy = TestBed.inject(HackerNewsApiProxy);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should call getNewStories with correct params', () => {
    proxy.getNewStories(1, 5).subscribe();

    const req = httpMock.expectOne(r =>
      r.url.includes('/new') &&
      r.params.get('page') === '1' &&
      r.params.get('count') === '5'
    );

    expect(req.request.method).toBe('GET');
    req.flush([{ id: 1, title: 'Test' }]);
  });

  it('should call getItem with correct id', () => {
    proxy.getItem(42).subscribe();

    const req = httpMock.expectOne(r => r.url.includes('/item/42'));
    expect(req.request.method).toBe('GET');
    req.flush({ id: 42, title: 'Test Item' });
  });
});