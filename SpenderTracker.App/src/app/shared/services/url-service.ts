import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UrlService {
    url = signal("");
}
