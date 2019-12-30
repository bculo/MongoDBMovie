import React, { useContext } from "react";
import { Redirect, Route, RouteProps } from "react-router-dom";
import { RootStoreContext } from "../../app/stores/rootStore";
import { IUser } from "../models/user";

const PrivateRoute: React.FC<RouteProps> = props => {
  const rootStore = useContext(RootStoreContext);
  const {getUserForRouting} = rootStore.userStore;
  let user: IUser | null = getUserForRouting;

  if (!user) {
    const renderComponent = () => <Redirect to={{ pathname: '/login' }} />;
    return <Route {...props} component={renderComponent} />;
  }

  return <Route {...props} />;
};

export default PrivateRoute;
