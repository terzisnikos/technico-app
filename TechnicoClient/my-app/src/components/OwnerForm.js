import React, { useEffect, useState } from "react";
import {
  TextField,
  Button,
  MenuItem,
  Container,
  Typography,
} from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import { createOwner, updateOwner, fetchOwnerById } from "../services/api";
import uuid from "react-uuid";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const OwnerForm = ({ isEditing, currentOwner }) => {
  const navigate = useNavigate();
  const { id } = useParams();

  const [owner, setOwner] = useState({
    name: "",
    surname: "",
    vatNumber: "",
    address: "",
    phoneNumber: "",
    email: "",
    password: "password",
    ownerType: "User",
  });

  const [errors, setErrors] = useState({});

  useEffect(() => {
    console.log("props.currentOwner", currentOwner.id);
    if (isEditing) {
      if (currentOwner.role === "Admin") {
        fetchOwnerById(id)
          .then((response) => {
            const {
              name,
              surname,
              vatNumber,
              address,
              phoneNumber,
              email,
              password,
              ownerType,
            } = response.data;
            setOwner({
              name,
              surname,
              vatNumber,
              address,
              phoneNumber,
              email,
              password,
              ownerType,
            });
          })
          .catch((error) => {
            console.error("Failed to fetch owner by ID:", error);
            toast.error("Failed to fetch owner details.");
          });
        console.log("4444");
      } else {
        fetchOwnerById(currentOwner.id)
          .then((response) => {
            const {
              name,
              surname,
              vatNumber,
              address,
              phoneNumber,
              email,
              password,
              ownerType,
            } = response.data;
            setOwner({
              name,
              surname,
              vatNumber,
              address,
              phoneNumber,
              email,
              password,
              ownerType,
            });
          })
          .catch((error) => {
            console.error(" to fetch owner by ID:", error);
            toast.error(" to fetch owner details.");
          });
      }
    }
  }, [isEditing, currentOwner.id, id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setOwner({ ...owner, [name]: value });
  };

  const validate = () => {
    const newErrors = {};
    if (!owner.name) newErrors.name = "Name is required";
    if (!owner.surname) newErrors.surname = "Surname is required";
    if (!owner.phoneNumber) newErrors.phoneNumber = "Phone number is required";
    if (!owner.email) newErrors.email = "Email is required";
    if (!owner.password && !isEditing)
      newErrors.password = "Password is required";
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = () => {
    if (!validate()) return;

    if (isEditing) {
      owner.id = id;
    }
    console.log("isEditing", isEditing);
    console.log("ID", owner.id);
    console.log("Owner", owner);
    owner.ownerType = 0;

    const action = isEditing ? updateOwner(id, owner) : createOwner(owner);

    action
      .then(() => {
        toast.success(
          isEditing
            ? "Owner updated successfully!"
            : "Owner created successfully!"
        );
        setTimeout(() => navigate("/owners"), 0);
      })
      .catch((error) => {
        console.error("Failed to save owner:", error);
        toast.error("Failed to save owner. Please try again.");
        if (error.response && error.response.data.errors) {
          setErrors(error.response.data.errors);
        }
      });
  };

  return (
    <Container maxWidth="sm">
      <h1>{isEditing ? "Edit Owner" : "Add Owner"}</h1>
      <form>
        <TextField
          label="Name"
          name="name"
          value={owner.name}
          onChange={handleChange}
          required
          fullWidth
          margin="normal"
          error={!!errors.name}
          helperText={errors.name}
        />
        <TextField
          label="Surname"
          name="surname"
          value={owner.surname}
          onChange={handleChange}
          required
          fullWidth
          margin="normal"
          error={!!errors.surname}
          helperText={errors.surname}
        />
        <TextField
          label="VAT Number"
          name="vatNumber"
          value={owner.vatNumber}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Phone Number"
          name="phoneNumber"
          value={owner.phoneNumber}
          onChange={handleChange}
          required
          fullWidth
          margin="normal"
          error={!!errors.phoneNumber}
          helperText={errors.phoneNumber}
        />
        <TextField
          label="Email"
          name="email"
          value={owner.email}
          onChange={handleChange}
          required
          fullWidth
          margin="normal"
          error={!!errors.email}
          helperText={errors.email}
        />
        <TextField
          label="Password"
          name="password"
          type="password"
          value={owner.password}
          onChange={handleChange}
          required={!isEditing}
          fullWidth
          margin="normal"
          error={!!errors.password}
          helperText={errors.password}
        />
        <TextField
          select
          label="Owner Type"
          name="ownerType"
          value={owner.ownerType || "User"}
          disabled
          onChange={handleChange}
          fullWidth
          margin="normal"
        >
          <MenuItem value="User">user</MenuItem>
          <MenuItem value="Admin">admin</MenuItem>
        </TextField>

        <Button
          variant="contained"
          color="primary"
          onClick={handleSubmit}
          fullWidth
          style={{ marginTop: "16px" }}
        >
          {isEditing ? "Update Owner" : "Create Owner"}
        </Button>

        {/* ToastContainer for displaying toast messages */}
        <ToastContainer />
      </form>
    </Container>
  );
};

export default OwnerForm;
