import React, { useContext } from "react";
import { Redirect, Route, RouteProps } from "react-router-dom";
import { RootStoreContext } from "../../app/stores/rootStore";
import { IUser } from "../models/user";

interface ExtendedRouteProps extends RouteProps{
  availableForRoles: string[],
}

const PrivateRoute: React.FC<ExtendedRouteProps> = props => {
  const rootStore = useContext(RootStoreContext);
  const {getUserForRouting} = rootStore.userStore;
  let user: IUser | null = getUserForRouting;

  let roles: string[] = props.availableForRoles;

  if (!user) {
    const renderComponent = () => <Redirect to={{ pathname: '/login' }} />;
    return <Route {...props} component={renderComponent} />;
  }

  let userRoleLowerCase = user.role.toLocaleLowerCase();
  if(!roles.some(e => e === userRoleLowerCase)){
    console.log("Korisnik nema ovlasti za ovu akciju");
    const renderComponent = () => <Redirect to={{ pathname: `/` }} />;
    return <Route {...props} component={renderComponent} />;
  }

  return <Route {...props} />;
};

export default PrivateRoute;
