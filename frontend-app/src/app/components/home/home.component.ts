import { Component } from '@angular/core';
import { ContentListComponent } from 'src/app/content/content-list/content-list.component';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  users: User[] = [];

  ngOnInit(): void {
    this.users = [
      {
        name: 'Mehmet',
        age: 25,
      },
      {
        name: 'Ahmet',
        age: 22,
      },
      {
        name: 'Yunus',
        age: 19,
      },
    ];
  }
}

class User {
  name?: string;
  age?: number;
}
