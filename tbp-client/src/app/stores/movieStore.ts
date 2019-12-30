import { RootStore } from "./rootStore";
import { action, computed, observable, runInAction } from "mobx";
import agent from "../api/agent";
import { IMovie, ICharacter, IGenre } from "../models/movie";
import { history } from "../..";

export default class MovieStore {
  store: RootStore;

  page: number = 1;
  pageSize: number = 10;

  constructor(store: RootStore) {
    this.store = store;
  }

  @observable movieRegistry = new Map();
  @observable characterRegistry = new Map();
  @observable genreRegistry = new Map();
  @observable hasMoreRecords: boolean = true;
  @observable titleSearch: string = "";
  @observable selectedMovie: IMovie | null = null;

  @computed get getMovies(): IMovie[] {
    return Array.from(this.movieRegistry.values());
  }

  @computed get getCharacters(): ICharacter[] {
    return Array.from(this.characterRegistry.values());
  }

  @computed get getGenres(): IGenre[] {
    return Array.from(this.genreRegistry.values());
  }

  @action restart = () => {
    this.movieRegistry.clear();
    this.characterRegistry.clear();
    this.genreRegistry.clear();
    this.selectedMovie = null;
    this.hasMoreRecords = false;
    this.page = 1;
  };

  @action setTitle = (title: string | null) => {
    if (!title) this.titleSearch = "";
    else this.titleSearch = title;
  };

  @action loadMoreMovies = async () => {
    try {
      const response = await agent.Movie.getMovies({
        page: this.page,
        title: this.titleSearch
      });
      runInAction(() => {
        response.forEach(movie => {
          movie.posterPath = `http://image.tmdb.org/t/p/w154/${movie.posterPath}`;
          this.movieRegistry.set(movie.id, movie);
        });
        if (response.length == 0) {
          this.hasMoreRecords = false;
        } else {
          this.page++;
        }
      });
    } catch (error) {
      console.log(error);
    }
  };

  @action getMovieFromStore = async (movieId: string) => {
    try {
      this.selectedMovie = this.movieRegistry.get(movieId);
      if (!this.selectedMovie) throw new Error("Movie not found");
    } catch (error) {
      console.log(error);
    }
  };

  @action getMovieCharactesApi = async (movieId: string) => {
    try {
      const response = await agent.Movie.getCharacters({ movieId: movieId });
      runInAction(() => {
        response.forEach(character => {
          character.profilePath = `http://image.tmdb.org/t/p/w185/${character.profilePath}`;
          this.characterRegistry.set(character.id, character);
        });
      });
    } catch (error) {
      console.log(error);
    }
  };

  @action getMovieCategoriesApi = async (movieId: string) => {
    try {
      const response = await agent.Movie.getGenres({ movieId: movieId });
      runInAction(() => {
        response.forEach(genre => {
          this.genreRegistry.set(genre.id, genre);
        });
      });
    } catch (error) {
      console.log(error);
    }
  };
}
