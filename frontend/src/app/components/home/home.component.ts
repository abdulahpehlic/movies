import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { StarRatingComponent } from 'ng-starrating';
import { LoginRequest } from 'src/app/core/login/model/login-request';
import { Movie } from 'src/app/core/movie/model/movie';
import { MovieService } from 'src/app/core/movie/service/movie-service';
import { RatingRequest } from 'src/app/core/rating/model/rating-request';
import { RatingService } from 'src/app/core/rating/service/rating-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  movies: Movie[];
  closeResult = '';
  totalstar = 5;
  ratingPost: any;
  
  constructor(private router: Router, private jwtHelper: JwtHelperService, 
    private movieService: MovieService, private modalService: NgbModal, private ratingService: RatingService) { }

  ngOnInit() {
    this.getMovies();
    this.isUserLoggedIn();
  }

  getMovies(){
    this.movieService.fetchMovies().subscribe(
      (data: any) => {
        this.movies = data;
      },
      (err) => {
        console.error(JSON.stringify(err));
      },
      () => {
        
      }
    );
  }
  navigate(url){
    this.router.navigate(url);
  }
  isUserAuthenticated() {
    const token: string = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }
  open(content) {
    this.modalService.open(content, {centered: true, ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
  onRate($event:{oldValue:number, newValue:number, starRating:StarRatingComponent}, movieId: number) {
    let request = new RatingRequest();
    request.userRating = $event.newValue;
    request.username = localStorage.getItem("currentUser");
    request.movieId = movieId;
    this.postRating(request, movieId);
  }
  postRating(request: RatingRequest, movieId: number){
    this.ratingService.postRating(request, movieId).subscribe(
      (data: any) => {
        this.ratingPost = data;
      },
      (err) => {
        console.error(JSON.stringify(err));
      },
      () => {
        
      }
    );
  }
  isUserLoggedIn(){
    return localStorage.getItem("jwt") != null;
  }
  logout() {
    localStorage.removeItem("jwt");
    localStorage.removeItem("currentUser");
  }
}
