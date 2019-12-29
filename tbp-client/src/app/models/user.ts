export interface IRegisterUserRequest{
    username: string,
    password: string,
    email: string
}

export interface ILoginUserRequest{
    username: string,
    password: string
}

export interface IUser{
    id: string,
    username: string
}

export interface ILoginResponse{
    token: string
}