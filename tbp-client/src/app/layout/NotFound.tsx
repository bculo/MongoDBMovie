
import React from 'react';
import { Segment, Button, Header, Icon } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

const NotFound: React.FC = () => {
    return (
        <Segment placeholder>
            <Header icon>
                <Icon name='search' />
                Not found :(
            </Header>
            <Segment.Inline>
                <Button as={Link} to='/' primary>
                    Nazad na početnu stranicu
                </Button>
            </Segment.Inline>
        </Segment>
    );
};

export default NotFound;