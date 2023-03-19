import { Component } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css'],
})
export class ContactComponent {
  username: string = '';
  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.username = this.userService.username;
  }
}
