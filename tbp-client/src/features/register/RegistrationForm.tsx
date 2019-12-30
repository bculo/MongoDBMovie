import React, { useContext } from "react";
import { observer } from "mobx-react-lite";
import { Form, Button, Segment, Label, Header } from "semantic-ui-react";
import { Form as FinalForm, Field } from "react-final-form";
import { ILoginUserRequest, IRegisterUserRequest } from "../../app/models/user";
import TextInput from "../../app/common/form/TextInput";
import ErrorMessage from "../../app/common/form/ErrorMessage";
import {
  combineValidators,
  isRequired,
  composeValidators,
  hasLengthGreaterThan
} from "revalidate";
import { RootStoreContext } from "../../app/stores/rootStore";

const validate = combineValidators({
  username: isRequired({ message: "Potrebno je unesti korisničko ime" }),
  password: composeValidators(
    isRequired({ message: "Potrebno je unesti lozinku" }),
    hasLengthGreaterThan(4)({ message: "Potrebno je unesti barem 4 znaka" })
  )("password"),
  email: composeValidators(isRequired({ message: "Potrebno je unesti mail" }))(
    "email"
  )
});

const RegistrationForm = () => {
  const rootStore = useContext(RootStoreContext);
  const { register } = rootStore.userStore;

  return (
    <Segment clearing>
      <Header as="h1" icon textAlign="center">
        <Header.Content>Registracija</Header.Content>
      </Header>

      <FinalForm
        validate={validate}
        onSubmit={(values: IRegisterUserRequest) => register(values)}
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
              Korisničko ime
            </Label>
            <Field
              name="username"
              placeholder="culix77"
              component={TextInput}
            />

            <Label as="a" color="blue" ribbon>
              Lozinka
            </Label>
            <Field name="password" type="password" component={TextInput} />

            <Label as="a" color="blue" ribbon>
              Email adresa
            </Label>
            <Field
              name="email"
              placeholder="nesto@gmail.com"
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
              content="Registracija"
              color="blue"
            />
          </Form>
        )}
      />
    </Segment>
  );
};

export default observer(RegistrationForm);
