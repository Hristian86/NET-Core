import React, { Component } from 'react';
import PostForm from "./Form";
import Payload from './Payload';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
            <Payload />

      </div>
    );
  }
}
