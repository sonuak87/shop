import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-test-errors',
  imports: [MatButton],
  templateUrl: './test-errors.component.html',
  styleUrl: './test-errors.component.scss'
})
export class TestErrorsComponent {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  validationErrors?: string[];
  
  get404Error() {
    this.http.get(this.baseUrl + 'buggy/notfound').subscribe({
      next: respponse => console.log(respponse),      
    })
  }

get400Error() {
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe({
      next: respponse => console.log(respponse), 
      error:error=>console.log(error)
    })
  }

  get401Error() {
    this.http.get(this.baseUrl + 'buggy/unauthorized').subscribe({
      next: respponse => console.log(respponse), 
      error:error=>console.log(error)
    })
  }
  get500Error() {
    this.http.get(this.baseUrl + 'buggy/internalserver').subscribe({
      next: respponse => console.log(respponse), 
      error:error=>console.log(error)
    })
  }
   getValidationError() {
    this.http.post(this.baseUrl + 'buggy/validationerror',{}).subscribe({
      next: respponse => console.log(respponse), 
      error:error=>this.validationErrors=error
    })
  }
}
