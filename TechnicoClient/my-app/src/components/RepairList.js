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
  fetchRepairs,
  deleteRepair,
  fetchRepairsByOwnerId,
} from "../services/api";

const RepairList = (props) => {
  const [repairs, setRepairs] = useState([]);
  const [filter, setFilter] = useState("");
  const currentOwner = props.currentOwner;
  useEffect(() => {
    if (currentOwner.role === "Admin") {
      fetchRepairs().then((response) => setRepairs(response.data));
    } else {
      fetchRepairsByOwnerId(currentOwner.id).then((response) =>
        setRepairs(response.data)
      );
    }
  }, []);

  const handleDelete = (id) => {
    deleteRepair(id).then(() => {
      setRepairs(repairs.filter((repair) => repair.id !== id));
    });
  };

  const filteredRepairs = repairs.filter(
    (repair) =>
      repair.type.toLowerCase().includes(filter.toLowerCase()) ||
      repair.propertyItemTitle.toLowerCase().includes(filter.toLowerCase()) ||
      repair.ownerName.toLowerCase().includes(filter.toLowerCase())
  );

  return (
    <>
      <h1>Repairs</h1>
      <Button
        sx={{ mt: 5, mb: 2 }}
        variant="contained"
        color="primary"
        component={Link}
        to="/repairs/new"
        style={{ marginBottom: "16px" }}
      >
        Add Repair
      </Button>

      <TextField
        sx={{ mb: 5, mt: 5, ml: 5 }}
        label="Filter by Type, Property, or Owner Name"
        variant="outlined"
        value={filter}
        onChange={(e) => setFilter(e.target.value)}
      />

      <Table>
        <TableHead>
          <TableRow>
            <TableCell>ID</TableCell>
            <TableCell>Type</TableCell>
            <TableCell>Scheduled Date</TableCell>
            <TableCell>Status</TableCell>
            <TableCell>Property</TableCell>
            <TableCell>Owner Name</TableCell>
            <TableCell>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {filteredRepairs.map((repair) => (
            <TableRow key={repair.id}>
              <TableCell>{repair.id}</TableCell>
              <TableCell>{repair.type}</TableCell>
              <TableCell>
                {new Date(repair.scheduledDate).toLocaleString()}
              </TableCell>
              <TableCell
                style={{
                  color:
                    repair.status === "Pending"
                      ? "red"
                      : repair.status === "In Progress"
                      ? "orange"
                      : "green",
                }}
              >
                {repair.status}
              </TableCell>
              <TableCell>{repair.propertyItemTitle}</TableCell>
              <TableCell>{repair.ownerName}</TableCell>
              <TableCell>
                <Button
                  component={Link}
                  to={`/repairs/${repair.id}`}
                  color="primary"
                >
                  Edit
                </Button>
                <Button
                  color="secondary"
                  onClick={() => handleDelete(repair.id)}
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

export default RepairList;
