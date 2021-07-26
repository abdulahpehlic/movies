import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Movie } from '../model/movie';
import { catchError } from 'rxjs/operators';
import { throwError} from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class MovieService {

  invalidLogin: boolean;

  private url = 'https://localhost:44355/api/movies/toprated';



  constructor(private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
  }

  fetchMovies(){
    return this.http.get<Movie[]>(this.url).pipe(
      catchError(this.handleError)
    )
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