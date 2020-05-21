import React, { Component } from 'react';
import { Progress, ToastHeader } from 'reactstrap';
import authService from './api-authorization/AuthorizeService';
import Payload from './Payload';
import Button from '@material-ui/core/Button';

var responceContent;
var pushed = false;

var array = [];

export default class PostData extends Component {
    constructor(props) {
        super(props)
        this.state = {
            getData: {
                "id": "",
                "lastName": "",
                "name": "",
                "age": ""
            }, loading: true
        }

        this.sendUpdates = this.sendUpdates.bind(this);
    }

    submitForm = (e) => {
        e.preventDefault();
        let modId = e.target.id.value;
        let modName = e.target.firstName.value;
        this.sendUpdates(modId, modName);
    }

    componentDidUpdate() {
        this.sendUpdates();
    }

    async sendUpdates(ids, name) {

        let payload = {
            "Id": Number(ids),
            "Name": name
        }

        const token = await authService.getAccessToken();

        const responce = await fetch("/Seed/Post", {
            "method": "POST",
            "headers": {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }, body: JSON.stringify(payload)
        }).then(res => res.json()).catch(x => console.log('Login is required'));

        const data = await responce;
        responceContent = responce;

        var arr = document.getElementById("array");
        var chield = document.createElement('text');

        try {
            if (typeof (responce.name) != "undefined") {
                chield.innerText = ` ${responce.id}, ${responce.name} ${responce.lastName}, ${responce.age} `;
            }
        } catch (e) {
            chield.innerText = ` You need to log in to make requests`;
        }

        arr.appendChild(chield);

        this.setState = ({ getData: data, loading: false });
        
    }


    render() {

        return (
            <div className="App">
                <div className="App-header"></div>
                <form onSubmit={this.submitForm}>
                    <h2>
                        Getting id's of table
                    </h2>
                    <label>
                        <span className="text">ID</span><br />
                        <input type="text" name="id" /><br />
                        <span>Optional</span><br /><input type="text" name="firstName" /><br />
                    </label>
                    <div className="align-right">
                        <Button type="submit" variant="contained" color="primary" >Submit</Button>
                    </div>
                </form><br /><br />

                <div id="array"></div>

            </div>
        );
    }
}