import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';

import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';

export default class Cardt extends Component {
    constructor(props) {
        super(props);
        this.state = {
            cards: [], loading: true
        };
    }

    componentDidMount() {
        this.Cards()
    }

    static Rearrange(cardses) {
        let arr = cardses;
        let arr1 = [];
        let nax = {};

        for (var i = 0; i < arr.Naxxramas.length - 1; i++) {
            nax = arr.Naxxramas[i];
            if (nax.name == arr.Naxxramas[i + 1].name) {

            } else {
                arr1.push(nax);
            }
        }

        try {
            console.log(arr1[0].name)
        } catch (e) {
            console.log(e)
        }

        return arr1;
    }

    static ShowCards(Cards) {

        let cardses = Cardt.Rearrange(Cards);

        return (
            <div>


                <div className="row d-flex justify -content-center">

                    {cardses.filter(cards => cards.img != null).map(cards => <Card key={cards.cardId} style={{ width: '18rem' }}>
                        <Card.Img variant="top" src={cards.img} />
                        <Card.Body>
                            <Card.Title>{cards.name}</Card.Title>
                            <Card.Text>
                                {cards.flavor}
                            </Card.Text>
                        </Card.Body>
                    </Card>
                    )}






                    
                </div>

            </div>
        );
    }

    render() {

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Cardt.ShowCards(this.state.cards);


        //let contents = this.state.seeds.map(x => <div key="x.id">{x.name} + {x.age} + {x.lastName}</div>)

        return (
            <div>
                {contents}
            </div>
        )
    }

    async Cards() {

        const token = await authService.getAccessToken();
        const response = await fetch("Seed/GetCards", {
            "method": "GET",
            "headers": {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        }).then(res => res.json()).catch(err => console.log(err));

        const data = await JSON.parse(response);
        this.setState({ cards: data, loading: false });
    }
}
