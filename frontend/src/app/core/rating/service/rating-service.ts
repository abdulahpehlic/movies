import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { throwError} from 'rxjs';
import { RatingRequest } from '../model/rating-request';

@Injectable({
  providedIn: 'root'
})

export class RatingService {

  invalidLogin: boolean;

  private url = 'https://localhost:44355/api/movies/updateRating/';

  constructor(private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
  }

  postRating(request: RatingRequest, movieId: number) {
    return this.http.post<RatingRequest>(this.url + movieId, request).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    return throwError(
      'Something bad happened; please try again later.');
  }
}