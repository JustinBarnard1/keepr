import Axios from "axios";

let baseUrl = location.host.includes("localhost") ? "http://localhost:5001/" : "/";

export const api = Axios.create({
  baseUrl: baseUrl + "api",
  timeout: 3000,
  withCredentials: true
});

export const setBearer = function (bearer) {
  api.defaults.headers.authorization = bearer;
};

export const resetBearer = function () {
  api.defaults.headers.authorization = "";
};
