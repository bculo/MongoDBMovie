import UserStore  from './userStore'
import { configure } from 'mobx';
import { createContext } from 'react';
import CommonStore from './commonStore';

configure({enforceActions: 'always'});

export class RootStore{
    userStore: UserStore;
    commonStore: CommonStore;

    constructor(){
        this.userStore = new UserStore(this);
        this.commonStore = new CommonStore(this);
    }
}

export const RootStoreContext = createContext(new RootStore());