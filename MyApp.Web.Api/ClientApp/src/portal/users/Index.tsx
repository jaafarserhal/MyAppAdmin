import { Helmet } from 'react-helmet-async';
import PageHeader from './PageHeader';
import PageTitleWrapper from 'src/components/PageTitleWrapper';
import { Grid, Container } from '@mui/material';
import Footer from 'src/components/Footer';
import { useApiCall } from '../../api/hooks/useApi';
import userService from '../../api/userService';



function Users() {


  const { data: users, loading, error, refetch } = useApiCall(
    () => userService.getUsers(),
    []
  );


  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error.message}</div>;

  // Log the users data to the console
  console.log('Fetched users:', users);
  return (
    <>
      <Helmet>
        <title>Users - Applications</title>
      </Helmet>
      <PageTitleWrapper>
        <PageHeader />
      </PageTitleWrapper>
      <Container maxWidth="lg">
        <Grid
          container
          direction="row"
          justifyContent="center"
          alignItems="stretch"
          spacing={3}
        >
          <Grid item xs={12}>

          </Grid>
        </Grid>
      </Container>
      <Footer />
    </>
  );
}

export default Users;
