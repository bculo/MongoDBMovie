import React, { Fragment, useContext, useEffect } from "react";
import { observer } from "mobx-react-lite";
import { ToastContainer } from "react-toastify";
import {
  RouteComponentProps,
  withRouter,
  Route,
  Switch
} from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import NavBar from "../../features/nav/NavBar";
import { Container } from "semantic-ui-react";
import LoginForm from "../../features/login/LoginForm";
import NotFound from "./NotFound";
import PrivateRoute from "./PrivateRoute";
import Logout from "../../features/logout/Logout";
import RegistrationForm from "../../features/register/RegistrationForm";
import { RootStoreContext } from "../stores/rootStore";
import MovieDashboard from "../../features/dashboard/MovieDashboard";
import MovieDetails from "../../features/dashboard/MovieDetails";
import AdminDashboard from "../../features/admin/AdminDashboard";

const App: React.FC<RouteComponentProps> = ({ location }) => {
  const rootStore = useContext(RootStoreContext);
  const { getUser } = rootStore.userStore;

  useEffect(() => {
    getUser();
  }, [getUser]);

  return (
    <Fragment>
      <ToastContainer position="bottom-right" />
      <Route exact path="/" component={HomePage} />

      <Route
        path={"/(.+)"}
        render={() => (
          <Fragment>
            <NavBar />
            <Container style={{ marginTop: "7em" }}>
              <Switch>
                <Route path="/login" component={LoginForm} />
                <Route path="/register" component={RegistrationForm} />
                <Route path="/logout" component={Logout} />
                <PrivateRoute
                  exact
                  path="/movies"
                  availableForRoles={["admin", "user"]}
                  component={MovieDashboard}
                />
                <PrivateRoute
                  path="/movies/:id"
                  availableForRoles={["admin", "user"]}
                  component={MovieDetails}
                />
                <PrivateRoute
                  path="/admin"
                  availableForRoles={["admin"]}
                  component={AdminDashboard}
                />
                <Route component={NotFound} />
              </Switch>
            </Container>
          </Fragment>
        )}
      />
    </Fragment>
  );
};

export default withRouter(observer(App));
