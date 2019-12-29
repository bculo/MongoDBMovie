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
        <Header as="h1" inverted>
          <Image
            size="massive"
            src="/assets/popcorn.png"
            alt="logo"
            style={{ marginBottom: 12 }}
          />
        </Header>

        <Header as="h2" inverted>
          Filmovi TBP
        </Header>

        {userLogedIn && (
          <Button as={Link} to="/" size="huge" inverted>
            Pregledaj filmove
          </Button>
        )}

        {!userLogedIn && (
          <Button as={Link} to="/login" size="huge" inverted>
            Prijava
          </Button>
        )}

        {!userLogedIn && (
          <Button as={Link} to="/register" size="huge" inverted>
            Registracija
          </Button>
        )}

      </Container>
    </Segment>
  );
};

export default HomePage;
