import React, { Fragment, useEffect, useContext } from "react";
import { observer } from "mobx-react-lite";
import { Header, Button } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import AdminMovieList from "./AdminMovieList";

const AdminDashboard = () => {
  const rootStore = useContext(RootStoreContext);
  const { adminGetNewMoviesApi } = rootStore.movieStore;

  useEffect(() => {
    adminGetNewMoviesApi();
  }, [adminGetNewMoviesApi]);

  return (
    <Fragment>
      <Header as="h1" textAlign="center">
        MANAGE MOVIES
      </Header>
      <AdminMovieList />
      <Button
        onClick={adminGetNewMoviesApi}
        fluid
        primary
        color="blue"
        content="LOAD MORE..."
        style={{ marginBottom: "50px" }}
      />
    </Fragment>
  );
};

export default observer(AdminDashboard);
