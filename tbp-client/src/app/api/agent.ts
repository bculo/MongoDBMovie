import axios, { AxiosResponse } from "axios";
import {
  ILoginUserRequest,
  ILoginResponse,
  IRegisterUserRequest
} from "../models/user";
import {
  IMovie,
  ITitleMovieRequest,
  ICharacterRequest,
  ICharacter,
  IGenre,
  IGenreRequest,
  IMovieWrapper,
  IManageMovieRequest
} from "../models/movie";
import { history } from "../..";
import { IErrorResponse } from "../models/error";

axios.defaults.baseURL = "https://localhost:44301/api";

axios.interceptors.request.use(
  config => {
    const token = window.localStorage.getItem("jwt");
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(undefined, error => {
  const { status } = error.response;

  if (status === 401) history.push("/logout");

  if (status === 403) history.push("/");

  throw error;
});

const responseBody = (response: AxiosResponse) => response.data;

const requesttype = {
  get: (url: string) => axios.get(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  del: (url: string) => axios.delete(url).then(responseBody)
};

const Authentication = {
  login: (request: ILoginUserRequest): Promise<ILoginResponse> =>
    requesttype.post("/authentication/login", request),
  register: (request: IRegisterUserRequest) =>
    requesttype.post("/authentication/register", request)
};

const Movie = {
  getMovies: (request: ITitleMovieRequest): Promise<IMovie[]> =>
    requesttype.post(`/movie/moviepage`, request),
  getCharacters: (request: ICharacterRequest): Promise<ICharacter[]> =>
    requesttype.post(`/movie/characters`, request),
  getGenres: (request: IGenreRequest): Promise<IGenre[]> =>
    requesttype.post(`/movie/genres`, request)
};

const Admin = {
  getNewMovies: (request: ITitleMovieRequest): Promise<IMovieWrapper[]> =>
    requesttype.post(`/movie/adminnewmovies`, request),
  deleteMovie: (request: IManageMovieRequest): Promise<IErrorResponse> =>
    requesttype.post(`/movie/admindeletemovie`, request),
  addMovie: (request: IManageMovieRequest): Promise<IErrorResponse> =>
    requesttype.post(`/movie/adminaddmovie`, request)
};

export default {
  Authentication,
  Movie,
  Admin
};
