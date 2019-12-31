import React, { useContext } from "react";
import { observer } from "mobx-react-lite";
import { RootStoreContext } from "../../app/stores/rootStore";
import { Segment, Item, Divider, Button } from "semantic-ui-react";
import { IMovieWrapper } from "../../app/models/movie";

const AdminMovieList = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    getNewMoviesWrapper,
    adminDeleteMovie,
    adminAddMovie
  } = rootStore.movieStore;

  return (
    <Segment clearing>
      <Item.Group divided>
        {getNewMoviesWrapper.map((wrapper: IMovieWrapper) => (
          <Item key={wrapper.imdbId}>
            <Item.Image
              alt="movie image"
              size="small"
              src={wrapper.posterPath}
            />
            <Item.Content>
              <Item.Header as="a">{wrapper.title}</Item.Header>
              <Divider />
              <Item.Content>{wrapper.imdbRating} / 10</Item.Content>
              <Item.Description>
                <div>{wrapper.overview}</div>
              </Item.Description>
              {wrapper.existsInDb && (
                <Button
                  floated="right"
                  content="DELETE"
                  color="red"
                  onClick={() => adminDeleteMovie(wrapper.imdbId)}
                />
              )}
              {!wrapper.existsInDb && (
                <Button
                  floated="right"
                  content="ADD"
                  color="blue"
                  onClick={() => adminAddMovie(wrapper.imdbId)}
                />
              )}
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </Segment>
  );
};

export default observer(AdminMovieList);
