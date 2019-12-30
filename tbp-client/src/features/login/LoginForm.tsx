import React, { useContext, useEffect } from "react";
import { observer } from "mobx-react-lite";
import {
  combineValidators,
  isRequired,
  composeValidators,
  hasLengthGreaterThan
} from "revalidate";
import { Form, Button, Segment, Label, Header } from "semantic-ui-react";
import { Form as FinalForm, Field } from "react-final-form";
import ErrorMessage from "../../app/common/form/ErrorMessage";
import { ILoginUserRequest } from "../../app/models/user";
import TextInput from "../../app/common/form/TextInput";
import { RootStoreContext } from "../../app/stores/rootStore";

const validate = combineValidators({
  username: isRequired({ message: "Potrebno je unesti korisničko ime" }),
  password: composeValidators(
    isRequired({ message: "Potrebno je unesti lozinku" }),
    hasLengthGreaterThan(4)({ message: "Potrebno je unesti barem 4 znaka" })
  )("password")
});

const LoginForm = () => {
  const rootStore = useContext(RootStoreContext);
  const { login, user } = rootStore.userStore;

  useEffect(() => {
  }, [user])

  return (
    <Segment clearing>
      <Header as="h1" icon textAlign="center">
        <Header.Content>Prijava</Header.Content>
      </Header>

      <FinalForm
        validate={validate}
        onSubmit={(values: ILoginUserRequest) => login(values)}
        render={({
          handleSubmit,
          submitting,
          form,
          submitError,
          invalid,
          pristine,
          dirtySinceLastSubmit
        }) => (
          <Form onSubmit={handleSubmit}>
            <Label as="a" color="blue" ribbon>
              Username
            </Label>
            <Field
              name="username"
              placeholder="username"
              component={TextInput}
            />

            <Label as="a" color="blue" ribbon>
              Password
            </Label>
            <Field
              name="password"
              placeholder="password"
              type="password"
              component={TextInput}
            />

            {submitError && !dirtySinceLastSubmit && (
              <ErrorMessage
                error={submitError}
                text="Pogrešno uneseno korisničko ime i lozinka"
              />
            )}

            <Button
              disabled={(invalid && !dirtySinceLastSubmit) || pristine}
              loading={submitting}
              type="submit"
              content="Prijava"
              color="blue"
            />
          </Form>
        )}
      />
    </Segment>
  );
};

export default observer(LoginForm);
