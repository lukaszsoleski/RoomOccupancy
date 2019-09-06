// auth.guard.ts
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { UsersService } from 'src/app/services/users.service';


@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private usersService: UsersService, private router: Router) {}

  canActivate() {

    if (!this.usersService.isAuthenticated) {
       this.router.navigate(['/login']);
       return false;
    }

    return true;
  }
}
