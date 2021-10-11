import React, { Component } from "react";
import { SemanticToastContainer } from 'react-semantic-toasts';
import 'react-semantic-toasts/styles/react-semantic-alert.css';
import 'semantic-ui-css/components/reset.min.css';
import 'semantic-ui-css/components/site.min.css';
import 'semantic-ui-css/components/container.min.css';
import 'semantic-ui-css/components/icon.min.css';
import 'semantic-ui-css/components/message.min.css';
import 'semantic-ui-css/components/header.min.css';
import { Route } from "react-router";
import { Layout } from "./components";
import { ApiTester } from "./pages";

import "./custom.css";

String.prototype.capitalize = function() {
    return this.charAt(0).toUpperCase() + this.slice(1);
}

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path="/" component={ApiTester} />
                <SemanticToastContainer />
            </Layout>
        );
    }
}
