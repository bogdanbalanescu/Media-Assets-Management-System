import React from 'react';
import { connect } from 'react-redux';
import { RootState } from '../redux';
import { CardMedia, Card, CardActionArea, Typography } from '@material-ui/core';

const mapStateToProps = (state: RootState) => ({
    selected_image: state.selected_image
  });

type Props = ReturnType<typeof mapStateToProps>;
  
class ImageView extends React.Component<Props> {
    
    render = () => {
        var imageToDisplay = this.props.selected_image > 0 ?
            (
                <CardMedia
                  component="img"
                  alt="Contemplative Reptile"
                  height="1080"
                  image={`https://localhost:5001/api/images/${this.props.selected_image}`}
                  title="Contemplative Reptile"
                />
            ) :
            (
                <Typography>
                    No image is currently selected.
                </Typography>
            )

        return (
            <Card>
              <CardActionArea>
                {imageToDisplay}
              </CardActionArea>
            </Card>
        );
  }
}

const connector = connect(mapStateToProps);
export default connector(ImageView);
