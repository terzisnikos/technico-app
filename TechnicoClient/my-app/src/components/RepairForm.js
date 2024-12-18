import React, { useState, useEffect } from "react";
import {
  TextField,
  Button,
  MenuItem,
  Select,
  InputLabel,
  FormControl,
  InputAdornment,
  IconButton,
  Container,
} from "@mui/material";
import { useParams, useNavigate } from "react-router-dom";
import { fetchRepairById, createRepair, updateRepair } from "../services/api";
import axios from "axios";

const RepairForm = ({ isEditing, currentOwner }) => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [repair, setRepair] = useState({
    type: "",
    description: "",
    propertyItemId: "",
    address: "",
    cost: 0,
    scheduledDate: null,
    status: "Pending",
  });

  const [propertyItems, setPropertyItems] = useState([]);

  const [errors, setErrors] = useState({});

  useEffect(() => {
    if (id && isEditing) {
      fetchRepairById(id).then((response) => {
        const {
          type,
          description,
          propertyItemId,
          address,
          cost,
          scheduledDate,
          status,
        } = response.data;
        setRepair({
          type,
          description,
          propertyItemId,
          address,
          cost,
          scheduledDate,
          status,
        });
      });
    }
  }, [id, isEditing]);

  useEffect(() => {
    console.log(currentOwner);
    if (currentOwner.role === "Admin") {
      axios
        .get("https://localhost:7004/api/PropertyItems")
        .then((response) => {
          setPropertyItems(response.data);
        })
        .catch((error) => {
          console.error("Failed to fetch property items:", error);
        });
    } else {
      axios
        .get(
          `https://localhost:7004/api/PropertyItems/owner/${currentOwner.id}`
        )
        .then((response) => {
          setPropertyItems(response.data);
        })
        .catch((error) => {
          console.error("Failed to fetch property items:", error);
        });
    }
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setRepair({ ...repair, [name]: value });
  };

  const handleDateChange = (date) => {
    setRepair({ ...repair, scheduledDate: date });
  };

  const repairTypes = [
    "Plumbing",
    "Electrical",
    "HVAC",
    "Roofing",
    "Painting",
    "Carpentry",
  ];

  const validate = () => {
    const newErrors = {};
    if (!repair.type) newErrors.type = "Type is required";
    if (!repair.propertyItemId)
      newErrors.propertyItemId = "Property Item ID is required";
    if (!repair.address) newErrors.address = "Address is required";
    if (!repair.cost || repair.cost <= 0)
      newErrors.cost = "Cost must be a positive value";
    if (!repair.status) newErrors.status = "Status is required";
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = () => {
    if (!validate()) return;

    const apiCall =
      id && isEditing ? updateRepair(id, repair) : createRepair(repair);

    apiCall
      .then(() => navigate("/repairs"))
      .catch((error) => {
        console.error("Failed to save repair:", error);
      });
  };

  return (
    <>
      <Container maxWidth="sm">
        <h1>{isEditing ? "Edit" : "Add"} Repair</h1>
        <form>
          <FormControl fullWidth margin="normal" required error={!!errors.type}>
            <InputLabel id="type-select-label">Type</InputLabel>
            <Select
              labelId="type-select-label"
              name="type"
              value={repair.type}
              onChange={handleChange}
            >
              {repairTypes.map((type) => (
                <MenuItem key={type} value={type}>
                  {type}
                </MenuItem>
              ))}
            </Select>
            {errors.type && <div style={{ color: "red" }}>{errors.type}</div>}
          </FormControl>
          <FormControl
            fullWidth
            margin="normal"
            required
            error={!!errors.propertyItemId}
          >
            <InputLabel id="property-item-select-label">
              Property Item
            </InputLabel>
            <Select
              labelId="property-item-select-label"
              name="propertyItemId"
              value={repair.propertyItemId}
              onChange={handleChange}
            >
              {propertyItems.map((item) => (
                <MenuItem key={item.id} value={item.id}>
                  {item.name}
                </MenuItem>
              ))}
            </Select>
            {errors.propertyItemId && (
              <div style={{ color: "red" }}>{errors.propertyItemId}</div>
            )}
          </FormControl>

          <TextField
            label="Address"
            name="address"
            value={repair.address}
            onChange={handleChange}
            required
            fullWidth
            margin="normal"
            error={!!errors.address}
            helperText={errors.address}
          />

          <TextField
            label="Cost"
            name="cost"
            type="number"
            value={repair.cost}
            onChange={handleChange}
            required
            fullWidth
            margin="normal"
            error={!!errors.cost}
            helperText={errors.cost}
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">$</InputAdornment>
              ),
            }}
          />

          <TextField
            label="Scheduled Date"
            name="scheduledDate"
            type="datetime-local"
            value={repair.scheduledDate ? repair.scheduledDate : ""}
            onChange={handleChange}
            fullWidth
            margin="normal"
            InputLabelProps={{
              shrink: true,
            }}
          />

          <FormControl
            fullWidth
            margin="normal"
            required
            error={!!errors.status}
          >
            <InputLabel id="status-select-label">Status</InputLabel>
            <Select
              labelId="status-select-label"
              name="status"
              value={repair.status}
              onChange={handleChange}
            >
              <MenuItem value="Pending">Pending</MenuItem>
              <MenuItem value="In Progress">In Progress</MenuItem>
              <MenuItem value="Completed">Completed</MenuItem>
            </Select>
            {errors.status && (
              <div style={{ color: "red" }}>{errors.status}</div>
            )}
          </FormControl>

          <Button
            variant="contained"
            color="primary"
            onClick={handleSubmit}
            fullWidth
            style={{ marginTop: "16px" }}
          >
            {isEditing ? "Update Repair" : "Create Repair"}
          </Button>
        </form>
      </Container>
    </>
  );
};

export default RepairForm;
