import React from 'react'
import { Redirect } from 'react-router-dom';

const Logout: React.FC = () => {
    return (
        <Redirect to="/login"/>
    )
}

export default Logout;
