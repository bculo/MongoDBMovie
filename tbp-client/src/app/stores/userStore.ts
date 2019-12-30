import {RootStore} from "./rootStore";
import { observable, action, runInAction, computed } from "mobx";
import { IUser, ILoginUserRequest, IRegisterUserRequest } from "../models/user";
import agent from "../api/agent";
import { history } from "../..";
import jwt_decode from "jwt-decode";

export default class UserStore{
    public store: RootStore;
    
    constructor(store: RootStore){
        this.store = store;
    }

    @observable user: IUser | null = null;

    @action login = async (loginCredentials: ILoginUserRequest) => {
        try{
            const response = await agent.Authentication.login(loginCredentials);
            runInAction(() => {
                this.store.commonStore.setJWTToken(response.token);
                this.user = this.getUserFromToken(response.token);
                history.push('/movies');
            });
        }catch(error){
            console.log(error);
        }
    }

    @action register = async (registerRequest: IRegisterUserRequest) => {
        try{
            const response = await agent.Authentication.register(registerRequest);
            history.push('/login');
        }catch(error){
            console.log(error);
        }
    }

    @action getUser = () => {
        let token: string | null = this.store.commonStore.jwtToken;
        try{
            this.user = this.getUserFromToken(token);
        }
        catch(error){
            this.removeAllData();
        }
    }

    @action logout = () => {
        this.removeAllData();
        this.store.movieStore.restart();
        this.user = null;
        history.push('/login');
    }

    @computed get userLogedIn(): boolean {
        if(!this.user){
            this.getUser();
        }

        if(this.user){
            return true;
        }else{
            return false;
        }
    }

    @computed get getUserForRouting(): IUser | null {
        try{
            return this.getUserFromToken(this.store.commonStore.jwtToken);
        }
        catch(error){
            return null;
        }
    }

    removeAllData(): void {
        this.store.commonStore.setJWTToken(null);
    }

    getUserFromToken(token: string | null) : IUser {
        if(!token){
            throw new Error("Token je null");
        }
        try{
            var decoded: any = jwt_decode(token);
            return {
                id: decoded.nameid,
                username: decoded.unique_name,
            };
        }
        catch(error){
            throw new Error("Pogrešan format tokena");
        }
    }
}