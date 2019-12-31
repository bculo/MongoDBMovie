import React, { useContext } from "react";
import { Segment, Container, Header, Image, Button } from "semantic-ui-react";
import { Link } from "react-router-dom";
import { RootStoreContext } from "../../app/stores/rootStore";

const HomePage: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { userLogedIn } = rootStore.userStore;

  return (
    <Segment inverted textAlign="center" vertical className="masthead">
      <Container text>
        <Header size='huge' inverted>
          <Image
            size="massive"
            src="/assets/popcorn.png"
            alt="logo"
            style={{ marginBottom: 12, width: "100px", heigth: "auto" }}
          />
        </Header>

        <Header as="h1" inverted style={{fontSize: "500%"}}>
          MOVIES TBP
        </Header>

        {userLogedIn && (
          <Button as={Link} to="/movies" size="huge" inverted>
            CHECK POPULAR MOVIES
          </Button>
        )}

        {!userLogedIn && (
          <Button as={Link} to="/login" size="huge" inverted style={{width: "250px"}}>
            JOIN
          </Button>
        )}

        {!userLogedIn && (
          <Button as={Link} to="/register" size="huge" inverted style={{width: "250px"}}>
            SIGN UP
          </Button>
        )}

      </Container>
    </Segment>
  );
};

export default HomePage;
