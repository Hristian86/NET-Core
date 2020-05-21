import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';
import PostData from "./PostData";

var id = 0;

export default class Message extends Component {
    static displayName = Message.name;
    constructor(props) {
        super(props);
        this.state = { seeds: [], loading: true };
    }

    componentDidMount() {
        this.Seeds()
    }

    static Messages(Seeds) {
        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Last name</th>
                            <th>Age</th>
                        </tr>
                    </thead>
                    <tbody>
                        {Seeds.map(Seeds =>
                            <tr key={Seeds.id}>
                                <td>{Seeds.id}</td>
                                <td>{Seeds.name}</td>
                                <td>{Seeds.lastName}</td>
                                <td>{Seeds.age}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Message.Messages(this.state.seeds);

        //let contents = this.state.seeds.map(x => <div key="x.id">{x.name} + {x.age} + {x.lastName}</div>)
        id = 2;

        return (
            <div>
                {contents}
                <PostData />
            </div>
        )
    }

    async Seeds() {

        const token = await authService.getAccessToken();
        console.log(token);
        const response = await fetch('Seed/Seeds');
        const data = await response.json();
        this.setState({ seeds: data, loading: false });
    }
}