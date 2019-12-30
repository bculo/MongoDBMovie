import React, { useContext, useEffect, Fragment, useState } from "react";
import { observer } from "mobx-react-lite";
import { RouteComponentProps } from "react-router-dom";
import { RootStoreContext } from "../../app/stores/rootStore";
import {
  Segment,
  Header,
  Image,
  Container,
  Grid,
  Card,
  Divider,
  Label
} from "semantic-ui-react";
import { ICharacter, IGenre } from "../../app/models/movie";

interface DetailsParams {
  id: string;
}

const MovieDetails: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history
}) => {
  const rootStore = useContext(RootStoreContext);
  const {
    getMovieFromStore,
    restart,
    selectedMovie,
    getMovieCharactesApi,
    getCharacters,
    getMovieCategoriesApi,
    getGenres
  } = rootStore.movieStore;

  useEffect(() => {
    if (match.params.id) {
      getMovieFromStore(match.params.id);
      getMovieCharactesApi(match.params.id);
      getMovieCategoriesApi(match.params.id);
    }

    return () => {
      restart();
    };
  }, []);

  return (
    <Fragment>
      <Segment>
        <Grid>
          <Grid.Column width={3}>
            <Image src={selectedMovie?.posterPath} />
          </Grid.Column>
          <Grid.Column width={12}>
            <Header as="h1">{selectedMovie?.title}</Header>
            <Header as="h3">Rating: {selectedMovie?.imdbRating}</Header>
            <Container>{selectedMovie?.overview}</Container>
            <Divider />
            {getGenres.map((genre: IGenre) => (
              <Label.Group color="blue" tag>
                <Label as="a" key={genre.id}>
                  {genre.name}
                </Label>
              </Label.Group>
            ))}
          </Grid.Column>
        </Grid>
      </Segment>

      <Card.Group centered>
        {getCharacters.map((character: ICharacter) => (
          <Card key={character.id}>
            <Image src={character.profilePath} wrapped ui={false} />
            <Card.Meta>
              <Card.Header>Name: {character.name}</Card.Header>
              <Card.Header>Character: {character.characterInMovie}</Card.Header>
            </Card.Meta>
          </Card>
        ))}
      </Card.Group>
    </Fragment>
  );
};

export default observer(MovieDetails);
