import React, { useContext, useEffect, Fragment } from "react";
import { observer } from "mobx-react-lite";
import { Button, Header, Search, Input } from "semantic-ui-react";
import MovieList from "./MovieList";
import { RootStoreContext } from "../../app/stores/rootStore";

const MovieDashboard: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { loadMoreMovies, hasMoreRecords } = rootStore.movieStore;

  useEffect(() => {
    loadMoreMovies();
  }, []);

  return (
    <Fragment>
        <Header as="h1" textAlign="center">List of movies</Header>
        <MovieList />
        {hasMoreRecords && (
          <Button
            onClick={loadMoreMovies}
            primary
            color="blue"
            content="Load more..."
          />
        )}
    </Fragment>
  );
};

export default observer(MovieDashboard);
