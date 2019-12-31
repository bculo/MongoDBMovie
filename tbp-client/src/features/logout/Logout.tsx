import React, { useContext } from "react";
import { Redirect } from "react-router-dom";
import { RootStoreContext } from "../../app/stores/rootStore";

const Logout: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { logout } = rootStore.userStore;

  logout();

  return <Redirect to="/login" />;
};

export default Logout;
