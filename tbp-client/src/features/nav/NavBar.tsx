import React, { useContext } from "react";
import { Menu, Container, Button } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import { observer } from "mobx-react-lite";

const NavBar: React.FC = () => {

  return (
    <Menu fixed="top" inverted>

      <Container>
      </Container>

    </Menu>
  );
};

export default observer(NavBar);
