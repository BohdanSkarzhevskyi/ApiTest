import React, { Component } from "react";
import { Container, List, Button, Segment } from 'semantic-ui-react'
import { toast } from 'react-semantic-toasts';
import dataService from '../services/dataService'


export default class ApiTester extends Component {
  constructor(props) {
    super(props);
    this.state = {
      studentAggregation: '',
      loading: false,
    };
  }

  onGetStatsClick = () => {
    this.setState({ loading: true });   
    dataService.getStudentStats()
      .then((data) => {
        this.setState({ studentAggregation: {...data} });
      })
      .finally(() => {
        this.setState({ loading: false });
      });
  };

  onSubmitAggregationClick = () => {
    this.setState({ loading: true }); 
    dataService.studentsAggregate({studentAggregate: this.state.studentAggregation})
      .then((data) => {
        toast({
          type: 'success',
          title: 'Success',
          description:  "Students aggregation submitted successfully!",
          time: 10000
        });
      })
      .finally(() => {
        this.setState({ loading: false, studentAggregation: '' });
      });
  };

  render() {
    const { loading, studentAggregation } = this.state;
    return (
      
      <Container>
        <Button loading={loading} onClick={this.onGetStatsClick} fluid primary>Load students stats</Button>
        <Segment loading={loading} placeholder>
          <List>
            {
              Object.entries(studentAggregation).map(([key, value],i) => (
                <List.Item>
                  <List.Icon name='circle' size='small' verticalAlign='middle' />
                  <List.Content>
                    <List.Header as='a'>{key.replace(/([a-z0-9])([A-Z])/g, '$1 $2').capitalize()}</List.Header>
                    <List.Description as='a'>{Array.isArray(value) ? value.join(", ") : value}</List.Description>
                  </List.Content>
                </List.Item>
                ))
            }
          </List>
        </Segment>
        <Button disabled={!studentAggregation} loading={loading} onClick={this.onSubmitAggregationClick} fluid primary>Submit student aggregation</Button>
      </Container>
    );
  }
}
