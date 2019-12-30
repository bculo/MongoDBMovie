import React, { useContext } from "react";
import { Menu, Container, Button, Search, Input } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";

const NavBar: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { user, logout } = rootStore.userStore;

  return (
    <Menu fixed="top" inverted>
      <Container>
        <Menu.Item as={Link} to="/">
            <img
              src="/assets/popcorn.png"
              alt="logo"
              style={{ marginRight: "10px" }}
            />
          Filmovi TBP
        </Menu.Item>

        <Menu.Menu position="right">

          {user && (
            <Menu.Item>
              <img
                src="/assets/user.png"
                alt="user"
                style={{ marginRight: "10px" }}
              />
              {user.username}
            </Menu.Item>
          )}

          {user && (
            <Menu.Item>
              <Button negative onClick={logout}>
                Logout
              </Button>
            </Menu.Item>
          )}
        </Menu.Menu>
      </Container>
    </Menu>
  );
};

export default observer(NavBar);
