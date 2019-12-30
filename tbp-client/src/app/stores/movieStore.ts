import { RootStore } from "./rootStore";
import { action, computed, observable, runInAction } from "mobx";
import agent from "../api/agent";
import { IMovie } from "../models/movie";

export default class MovieStore {
  store: RootStore;
  page: number = 1;

  constructor(store: RootStore) {
    this.store = store;
  }

  @observable movieRegistry = new Map();
  @observable hasMoreRecords: boolean = true;
  @observable titleSearch: string = "";

  @computed get getMovies(): IMovie[] {
    return Array.from(this.movieRegistry.values());
  }

  @action restart = () => {
    this.movieRegistry.clear();
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
}
