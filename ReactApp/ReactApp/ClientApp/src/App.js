import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';
import Messages from "./components/Messages";
import Cards from "./components/Cards";
import CardsController from "./components/CardsController";


import './custom.css'
import { Forum } from './components/Forum';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
            <Route path='/counter' component={Counter} />
            <Route path='/Forum' component={Forum} />
            <Route exact path='/Seed/Seeds' component={Messages} />
            <AuthorizeRoute path='/fetch-data' component={FetchData} />
            <AuthorizeRoute path='/Cards' component={Cards} />
            <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
            <Route exact path='/Seed/GetCards' component={CardsController} />
      </Layout>
    );
  }
}