import React, { useContext } from "react";
import { Menu, Container, Button } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import { observer } from "mobx-react-lite";
import { Link, NavLink } from "react-router-dom";

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
          MOVIES TBP
        </Menu.Item>

        {user && (
          <Menu.Item as={NavLink} to="/movies">
            MOVIES
          </Menu.Item>
        )}

        {user && user.role === "admin" && (
          <Menu.Item as={NavLink} to="/admin">
            MANAGE
          </Menu.Item>
        )}

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
                LOGOUT
              </Button>
            </Menu.Item>
          )}
        </Menu.Menu>
      </Container>
    </Menu>
  );
};

export default observer(NavBar);
