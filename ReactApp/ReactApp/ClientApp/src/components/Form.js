import React, { Component } from 'react';
import Button from '@material-ui/core/Button';

export default class PostForm extends Component {
    constructor(props) {
        super(props)
        this.state = {
            user: {
                username: '',
                password: ''
            },
            error: ''
        }

        this.onInputChange = this.onInputChange.bind(this)
        this.onFormSubmit = this.onFormSubmit.bind(this)
    }

    onInputChange(event) {
        let user = this.state.user;
        let inputName = event.target.id;
        let inputValue = event.target.value;

        user[inputName] = inputValue;

        this.setState({user})
    }

    onFormSubmit(event) {
        event.preventDefault();

        if (this.state.user.password.length < 4) {
            this.setState({
                error: 'Password lenght must be more that 4 characters'
            })
            return;
        } else {
            this.setState({
                error: ''
            })
        }

        debugger
        console.log(this.state.user);
    }

    render() {
        return (
            <form onSubmit={this.onFormSubmit}>
                <div>{this.state.error}</div>
                Username:<br/>
                <input type="text" name="username" onChange={this.onInputChange} /> <br />
                Password:<br/>
                <input type="password" name="password" onChange={this.onInputChange} /> <br />
                <Button type="submit" variant="contained" color="primary" className="mt-3">Submit</Button>
            </form>
        )
    }
}