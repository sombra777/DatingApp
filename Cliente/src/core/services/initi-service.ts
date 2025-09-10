import { Inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InitiService {
  private accountService = Inject(AccountService);

  init() {
    const userString = localStorage.getItem('user');

    if (userString) {
      const user = JSON.parse(userString);
      this.accountService.currentUser.set(user);
    } else return of(null);

    return of(null);
  }
}
