import React from 'react';
import './App.css';
import FoldersTreeView from './components/FoldersTreeView';
import { RootState } from './redux';
import { addFolders } from './redux/modules/folders';
import { connect } from 'react-redux';

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
      <FoldersTreeView />
    );
  }
}

const connector = connect(mapStateToProps, mapDispatchToProps);
export default connector(App);
