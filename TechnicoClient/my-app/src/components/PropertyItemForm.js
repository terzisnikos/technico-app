import React, { useState, useEffect } from "react";
import {
  TextField,
  Button,
  MenuItem,
  Select,
  InputLabel,
  FormControl,
  Container,
} from "@mui/material";
import { useParams, useNavigate } from "react-router-dom";
import {
  fetchPropertyItemById,
  createPropertyItem,
  updatePropertyItem,
} from "../services/api";
import axios from "axios";

const PropertyItemForm = (props) => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [propertyItem, setPropertyItem] = useState({
    name: "",
    address: "",
    propertyIdentificationNumber: "",
    yearOfConstruction: "",
    VatNumber: "",
    ownerId: "",
  });
  const [owners, setOwners] = useState([]);

  useEffect(() => {
    if (id) {
      fetchPropertyItemById(id).then((response) => {
        setPropertyItem(response.data);
      });
    }
  }, [id]);

  useEffect(() => {
    axios
      .get("https://localhost:7004/api/Owners")
      .then((response) => {
        setOwners(response.data);
      })
      .catch((error) => {
        console.error("Failed to fetch owners:", error);
      });
  }, []);

  const handleSubmit = () => {
    propertyItem.id = id;
    if (props.currentOwner.role !== "Admin") {
      propertyItem.ownerId = props.currentOwner.id;
    }
    const action = props.isEditing
      ? updatePropertyItem(id, propertyItem)
      : createPropertyItem(propertyItem);

    action
      .then(() => {
        setTimeout(() => navigate("/property-items"), 0);
      })
      .catch((error) => {
        console.error("Failed to save property item:", error);
      });
  };

  const isValidOwner = (ownerId) =>
    owners.some((owner) => owner.id === ownerId);

  return (
    <>
      <Container maxWidth="sm">
        <h1>{props.isEditing ? "Edit" : "Add"} Property</h1>
        <form>
          <TextField
            label="Property Identification Number"
            name="propertyIdentificationNumber"
            value={propertyItem.propertyIdentificationNumber}
            onChange={(e) =>
              setPropertyItem({
                ...propertyItem,
                [e.target.name]: e.target.value,
              })
            }
            fullWidth
            margin="normal"
          />
          <TextField
            label="Name"
            name="name"
            value={propertyItem.name}
            onChange={(e) =>
              setPropertyItem({
                ...propertyItem,
                [e.target.name]: e.target.value,
              })
            }
            fullWidth
            margin="normal"
          />
          <TextField
            label="Address"
            name="address"
            value={propertyItem.address}
            onChange={(e) =>
              setPropertyItem({
                ...propertyItem,
                [e.target.name]: e.target.value,
              })
            }
            fullWidth
            margin="normal"
          />
          <TextField
            label="Year of Construction"
            name="yearOfConstruction"
            value={propertyItem.yearOfConstruction}
            onChange={(e) =>
              setPropertyItem({
                ...propertyItem,
                [e.target.name]: e.target.value,
              })
            }
            fullWidth
            margin="normal"
          />
          {props.currentOwner.role === "Admin" ? (
            <FormControl fullWidth margin="normal">
              <InputLabel id="owner-select-label">Owner</InputLabel>
              <Select
                labelId="owner-select-label"
                name="ownerId"
                value={
                  isValidOwner(propertyItem.ownerId) ? propertyItem.ownerId : ""
                } // Validate ownerId
                onChange={(e) =>
                  setPropertyItem({ ...propertyItem, ownerId: e.target.value })
                }
              >
                <MenuItem value="">
                  <em>None</em>
                </MenuItem>
                {owners.map((owner) => (
                  <MenuItem key={owner.id} value={owner.id}>
                    {owner.surname}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          ) : (
            <FormControl fullWidth margin="normal">
              <InputLabel id="owner-select-label">Owner</InputLabel>
              <Select
                labelId="owner-select-label"
                name="ownerId"
                disabled
                value={props.currentOwner.id}
                onChange={(e) =>
                  setPropertyItem({ ...propertyItem, ownerId: e.target.value })
                }
              >
                <MenuItem value="">
                  <em>None</em>
                </MenuItem>
                {owners.map((owner) => (
                  <MenuItem key={owner.id} value={owner.id}>
                    {owner.surname}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          )}
          <Button variant="contained" color="primary" onClick={handleSubmit}>
            Submit
          </Button>
        </form>
      </Container>
    </>
  );
};

export default PropertyItemForm;
