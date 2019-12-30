import React, { useContext } from "react";
import { Segment, Item, Button, Grid, Divider } from "semantic-ui-react";
import { Link } from "react-router-dom";
import { RootStoreContext } from "../../app/stores/rootStore";
import { IMovie } from "../../app/models/movie";
import { observer } from "mobx-react-lite";

const MovieList: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { getMovies } = rootStore.movieStore;

  return (
    <Segment clearing>
      <Item.Group divided>
        {getMovies.map((movie: IMovie) => (
          <Item key={movie.id}>
            <Item.Image alt="movie image" size="small" src={movie.posterPath} />
            <Item.Content>
              <Item.Header as="a">{movie.title}</Item.Header>
              <Divider />
              <Item.Meta>{movie.releasedate}</Item.Meta>
              <Item.Content>{movie.imdbRating} / 10</Item.Content>
              <Item.Description>
                <div>{movie.overview}</div>
              </Item.Description>
              <Button
                  floated='right'
                  as={Link}
                  to={`/student/edit/${movie.id}`}
                  content="View"
                  color="blue"
                />
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </Segment>
  );
};

export default observer(MovieList);
