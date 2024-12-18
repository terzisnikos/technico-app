import React, { useEffect, useState } from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  Button,
  TextField,
} from "@mui/material";
import { Link } from "react-router-dom";
import {
  fetchPropertyItems,
  deletePropertyItem,
  fetchPropertyItemsByOwnerId,
} from "../services/api";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const PropertyItemList = (props) => {
  const [propertyItems, setPropertyItems] = useState([]);
  const [filter, setFilter] = useState("");
  useEffect(() => {
    if (props.currentOwner.role === "Admin") {
      fetchPropertyItems()
        .then((response) => setPropertyItems(response.data))
        .catch(() => toast.error("Failed to fetch property items!"));
    } else {
      fetchPropertyItemsByOwnerId(props.currentOwner.id)
        .then((response) => setPropertyItems(response.data))
        .catch(() => toast.error("Failed to fetch property items!"));
    }
  }, []);

  const handleDelete = async (id) => {
    try {
      await deletePropertyItem(id);
      setPropertyItems(propertyItems.filter((item) => item.id !== id));
      toast.success("Property item deleted successfully!");
    } catch (error) {
      toast.error(
        "Failed to delete property item. Please try to delete the depended Repairs first."
      );
    }
  };

  const filteredPropertyItems = propertyItems.filter(
    (item) =>
      item.name.toLowerCase().includes(filter.toLowerCase()) ||
      item.ownerName.toLowerCase().includes(filter.toLowerCase())
  );

  return (
    <>
      <ToastContainer />
      <h1>Properties</h1>
      <Button
        sx={{ mt: 5, mb: 2 }}
        variant="contained"
        color="primary"
        component={Link}
        to="/property-items/new"
      >
        Add Property Item
      </Button>
      <TextField
        sx={{ mb: 5, mt: 5, ml: 5 }}
        label="Filter by Address or Owner Name"
        variant="outlined"
        value={filter}
        onChange={(e) => setFilter(e.target.value)}
      />
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>#</TableCell>
            <TableCell>Name</TableCell>
            <TableCell>Address</TableCell>
            <TableCell>Owner Name</TableCell>
            <TableCell>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {filteredPropertyItems.map((item, index) => (
            <TableRow key={item.id}>
              <TableCell>{index + 1}</TableCell>
              <TableCell>{item.name}</TableCell>
              <TableCell>{item.address}</TableCell>
              <TableCell>{item.ownerName}</TableCell>
              <TableCell>
                <Button component={Link} to={`/property-items/${item.id}`}>
                  Edit
                </Button>
                <Button color="secondary" onClick={() => handleDelete(item.id)}>
                  Delete
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </>
  );
};

export default PropertyItemList;
