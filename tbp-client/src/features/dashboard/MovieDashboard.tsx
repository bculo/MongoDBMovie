import React, { useContext, useEffect, Fragment } from "react";
import { observer } from "mobx-react-lite";
import { Button, Header } from "semantic-ui-react";
import MovieList from "./MovieList";
import { RootStoreContext } from "../../app/stores/rootStore";

const MovieDashboard: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { loadMoreMovies, hasMoreRecords } = rootStore.movieStore;

  useEffect(() => {
    loadMoreMovies();
  }, [loadMoreMovies]);

  return (
    <Fragment>
        <Header as="h1" textAlign="center">MOVIE LIST</Header>
        <MovieList />
        {hasMoreRecords && (
          <Button
            onClick={loadMoreMovies}
            fluid
            primary
            color="blue"
            content="LOAD MORE..."
            style={{marginBottom: "50px"}}
          />
        )}
    </Fragment>
  );
};

export default observer(MovieDashboard);
