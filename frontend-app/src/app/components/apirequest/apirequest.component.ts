import { HttpClient,HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-apirequest',
  templateUrl: './apirequest.component.html',
  styleUrls: ['./apirequest.component.scss'],
})
export class ApirequestComponent {
  responseData: any;

  constructor(private http: HttpClient) {this.makeRequest()}

  makeRequest() {
    console.log("fsafsa")
    //const endpoint = 'http://localhost:5204/api/Videos';
    const endpoint = 'http://ec2-18-184-142-123.eu-central-1.compute.amazonaws.com:9090/api/Videos';
    this.http.get(endpoint).subscribe(
      (data) => {
        console.log(data);
      },
      (error: HttpErrorResponse) => {
       console.log(error);
      }
    );
  }
}
