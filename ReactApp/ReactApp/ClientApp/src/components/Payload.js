import React, { Component } from 'react';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import FormControl from '@material-ui/core/FormControl';
import Carousel from 'react-bootstrap/Carousel';
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';


export default class Payload extends Component {
    constructor(props) {
        super(props)
    }

    render() {

        return (
            <div>

                <NavLink to="/Counter" tag={Link} className="pl-0 mb-2 countLink"><Button variant="contained" color="primary">Counter</Button></NavLink>
                    
                <Paper><h1 className="text-center">Welcome to nature</h1></Paper>

                <div className="container d-flex justify-content-center mb-2">
                    </div>

                <Carousel>
                    <Carousel.Item>
                        <img
                            className="d-block w-100"
                            src="https://wallpaperaccess.com/full/170249.jpg"
                            alt="First slide"
                        />
                        <Carousel.Caption>
                            <h3>First slide label</h3>
                            <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
                        </Carousel.Caption>
                    </Carousel.Item>
                    <Carousel.Item>
                        <img
                            className="d-block w-100"
                            src="https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcS6q9DonBVwlMTxOEtYXM65YuZNGTFEgsred8sRxV_S45omaplF&usqp=CAU"
                            alt="Second slide"
                        />

                        <Carousel.Caption>
                            <h3>Second slide label</h3>
                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                        </Carousel.Caption>
                    </Carousel.Item>
                    <Carousel.Item>
                        <img
                            className="d-block w-100"
                            src="https://newevolutiondesigns.com/images/freebies/nature-hd-background-1.jpg"
                            alt="Third slide"
                        />

                        <Carousel.Caption>
                            <h3>Third slide label</h3>
                            <p>Praesent commodo cursus magna, vel scelerisque nisl consectetur.</p>
                        </Carousel.Caption>
                    </Carousel.Item>
                </Carousel>


            </div>
        )
    }
}