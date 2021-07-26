import { LoginRequest } from "../../login/model/login-request";

export class RatingRequest {
    userRating: number;
    username: string;
    movieId: number;
}