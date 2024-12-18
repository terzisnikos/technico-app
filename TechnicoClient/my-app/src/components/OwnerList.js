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
import { fetchOwners, deleteOwner } from "../services/api";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const OwnerList = () => {
  const [owners, setOwners] = useState([]);
  const [filter, setFilter] = useState("");
  useEffect(() => {
    fetchOwners()
      .then((response) => setOwners(response.data))
      .catch((error) => toast.error("Failed to fetch owners!"));
  }, []);

  const handleDelete = async (id) => {
    try {
      await deleteOwner(id);
      setOwners(owners.filter((owner) => owner.id !== id));
      toast.success("Owner deleted successfully!");
    } catch (error) {
      toast.error(
        "Failed to delete owner. Please try to delete the depended Properties first."
      );
    }
  };

  const filteredOwners = owners.filter(
    (owner) =>
      !filter || owner.vatNumber?.toLowerCase().includes(filter.toLowerCase())
  );

  return (
    <>
      <ToastContainer />
      <h1>Owners</h1>
      <Button
        sx={{ mt: 5, mb: 2 }}
        variant="contained"
        color="primary"
        component={Link}
        to="/owners/new"
      >
        Add Owner
      </Button>
      <TextField
        sx={{ mb: 5, mt: 5, ml: 5 }}
        label="Filter by VAT Number"
        variant="outlined"
        value={filter}
        onChange={(e) => setFilter(e.target.value)}
      />
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>ID</TableCell>
            <TableCell>Name</TableCell>
            <TableCell>Surname</TableCell>
            <TableCell>Email</TableCell>
            <TableCell>vatNumber</TableCell>
            <TableCell>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {filteredOwners.map((owner) => (
            <TableRow key={owner.id}>
              <TableCell>{owner.id}</TableCell>
              <TableCell>{owner.name}</TableCell>
              <TableCell>{owner.surname}</TableCell>
              <TableCell>{owner.email}</TableCell>
              <TableCell>{owner.vatNumber}</TableCell>
              <TableCell>
                <Button component={Link} to={`/owners/${owner.id}`}>
                  Edit
                </Button>
                <Button
                  color="secondary"
                  onClick={() => handleDelete(owner.id)}
                >
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

export default OwnerList;
