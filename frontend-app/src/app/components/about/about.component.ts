import { Component } from '@angular/core';
import User from 'src/app/models/UserModels/userModel';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css'],
})
export class AboutComponent {
  public users: User[] = [];
  constructor(public userService: UserService) {}

  ngOnInit(): void {
    this.getUsers();
  }
  getUsers() {
    this.userService.getUsers().subscribe((res) => {
      console.log(res);
      this.users = res;
    });
  }
}
