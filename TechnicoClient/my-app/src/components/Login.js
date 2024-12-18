import React, { useState } from "react";
import {
  TextField,
  Button,
  Alert,
  Typography,
  CircularProgress,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import axios from "axios";

const OwnerLogin = () => {
  const navigate = useNavigate();

  const [credentials, setCredentials] = useState({
    Username: "",
    password: "",
  });
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);
  const [ownerData, setOwnerData] = useState(null);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCredentials({ ...credentials, [name]: value });
  };

  const handleSubmit = async () => {
    setError(null);
    setLoading(true);

    try {
      const response = await axios.post(
        "https://localhost:7004/api/Auth/login",
        credentials
      );
      const { token } = response.data;

      localStorage.setItem("token", token);

      await fetchOwnerData(token);

      setTimeout(() => {
        navigate("/repairs");
      }, 2000);
    } catch (err) {
      setError(
        err.response?.data?.message || "An error occurred. Please try again."
      );
    } finally {
      setLoading(false);
    }
  };

  const fetchOwnerData = async (token) => {
    try {
      const response = await axios.get("https://localhost:7004/api/Auth/me", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      setOwnerData(response.data);
    } catch (err) {
      setError("Failed to fetch owner data. Please try again.");
    }
  };

  return (
    <div
      style={{
        maxWidth: 400,
        margin: "50px auto",
        textAlign: "center",
        marginTop: "200px",
        padding: "16px",
        border: "1px solid #ccc",
        borderRadius: "12px",
        boxShadow: "0 2px 8px rgba(0, 0, 0, 0.1)",
        backgroundColor: "white",
      }}
    >
      <h1>Technico</h1>
      <h2>Login</h2>
      {error && (
        <Alert severity="error" style={{ marginBottom: "16px" }}>
          {error}
        </Alert>
      )}
      {ownerData ? (
        <div>
          <Typography variant="h5" gutterBottom>
            Welcome, {ownerData.name} {ownerData.surname}!
          </Typography>
          <Typography variant="body1">Email: {ownerData.email}</Typography>
          <Typography variant="body1">
            Phone: {ownerData.phoneNumber}
          </Typography>
          <Typography variant="body1">
            VAT Number: {ownerData.vatNumber}
          </Typography>
          <Alert severity="success" style={{ marginTop: "16px" }}>
            Redirecting to the dashboard in 2 seconds...
          </Alert>
        </div>
      ) : (
        <>
          <TextField
            //label="Username"
            name="Username"
            label="Username"
            placeholder=""
            value={credentials.Username}
            onChange={handleChange}
            fullWidth
            required
            margin="normal"
          />
          <TextField
            label="Password"
            name="password"
            type="password"
            value={credentials.password}
            onChange={handleChange}
            fullWidth
            required
            margin="normal"
          />
          <Button
            variant="contained"
            color="primary"
            onClick={handleSubmit}
            fullWidth
            style={{ marginTop: "16px" }}
            disabled={loading}
          >
            {loading ? <CircularProgress size={24} /> : "Login"}
          </Button>
          <Button
            variant="outlined"
            color="secondary"
            onClick={() => navigate("/register")}
            fullWidth
            style={{ marginTop: "16px" }}
          >
            Register
          </Button>
        </>
      )}
    </div>
  );
};

export default OwnerLogin;
