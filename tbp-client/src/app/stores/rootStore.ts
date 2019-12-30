import UserStore  from './userStore'
import { configure } from 'mobx';
import { createContext } from 'react';
import CommonStore from './commonStore';
import MovieStore from './movieStore';

configure({enforceActions: 'always'});

export class RootStore{
    userStore: UserStore;
    commonStore: CommonStore;
    movieStore: MovieStore;

    pageSize: number = 10
    baseUrl: string = "http://image.tmdb.org/t/p/w342/";

    constructor(){
        this.userStore = new UserStore(this);
        this.commonStore = new CommonStore(this);
        this.movieStore = new MovieStore(this);
    }
}

export const RootStoreContext = createContext(new RootStore());