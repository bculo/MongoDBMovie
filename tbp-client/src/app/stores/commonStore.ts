import {RootStore} from "./rootStore";
import { action, computed } from "mobx";

export default class CommonStore{
    store: RootStore;
    
    constructor(store: RootStore){
        this.store = store;
    }

    @computed get jwtToken(): string | null{
        return window.localStorage.getItem('jwt');
    }

    @action setJWTToken = (token: string | null) => {
        if(token){
            window.localStorage.setItem('jwt', token);
        }
        else{
            window.localStorage.removeItem('jwt');
        }
    }
}