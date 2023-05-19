import { Container, Divider, Paper, Typography, Button } from "@mui/material";
import { Link } from "react-router-dom";

const ServerError = () => {
  return (
    <Container component={Paper}>
      <Typography variant="h3" gutterBottom>Oops - we could not found what you are looking for</Typography>
      <Divider />
      <Button fullWidth component={Link} to='/catalog'>
        Go back to shop
      </Button>
    </Container>
  )
}

export default ServerError;