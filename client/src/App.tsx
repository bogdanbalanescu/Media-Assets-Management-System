import React from 'react';
import './App.css';
import FoldersTreeView from './components/FoldersTreeView';
import ImageView from './components/ImageView';
import { RootState } from './redux';
import { addFolders } from './redux/modules/folders';
import { connect } from 'react-redux';
import { Box, AppBar, Toolbar, Typography } from '@material-ui/core';

const mapStateToProps = (state: RootState) => ({
  foldersRepository: state.foldersRepository
});
const mapDispatchToProps = { 
  addFolders: addFolders
}

type Props = ReturnType<typeof mapStateToProps> & typeof mapDispatchToProps;

class App extends React.Component<Props> {
  render() {
    return (
      <Box justifyContent="center" height="100%">
        <AppBar position="static">
          <Toolbar variant="dense">
            <Typography variant="h6" color="inherit">
              Images
            </Typography>
          </Toolbar>
        </AppBar>
        <Box display="flex" >
          <Box order={1} flex={1}>
            <FoldersTreeView />
          </Box>
          <Box order={1} flex={4} alignItems="center">
            <ImageView />
          </Box>
        </Box>
      </Box>
    );
  }
}

const connector = connect(mapStateToProps, mapDispatchToProps);
export default connector(App);
