import axios, { AxiosResponse } from "axios";
import {
  ILoginUserRequest,
  ILoginResponse,
  IRegisterUserRequest
} from "../models/user";

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
  const { status, config } = error.response;
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

export default {
  Authentication
};
