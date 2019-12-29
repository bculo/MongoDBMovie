import React from "react";
import { AxiosResponse } from "axios";
import { Message } from "semantic-ui-react";

interface IProps {
  error: AxiosResponse;
  text: string;
}

const ErrorMessage: React.FC<IProps> = ({ error, text }) => {
  return (
    <Message>
      <Message.Header>{error.statusText}</Message.Header>
      <Message.Content content={text} />
    </Message>
  );
};

export default ErrorMessage;
