import React, { useState, useEffect } from "react";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  useLocation,
  Navigate,
} from "react-router-dom";
import NavBar from "./components/NavBar";
import OwnerList from "./components/OwnerList";
import OwnerForm from "./components/OwnerForm";
import PropertyItemList from "./components/PropertyItemList";
import PropertyItemForm from "./components/PropertyItemForm";
import RepairList from "./components/RepairList";
import RepairForm from "./components/RepairForm";
import Register from "./components/Register";
import Login from "./components/Login";
import axios from "axios";
import { Container, CircularProgress } from "@mui/material";

const App = () => {
  const location = useLocation();

  const hideNavBarRoutes = ["/", "/logout"];
  const shouldShowNavBar = !hideNavBarRoutes.includes(location.pathname);

  const [currentOwner, setCurrentOwner] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchCurrentOwner = async () => {
      const token = localStorage.getItem("token");
      if (token) {
        try {
          const response = await axios.get(
            "https://localhost:7004/api/Auth/me",
            {
              headers: {
                Authorization: `Bearer ${token}`,
              },
            }
          );
          setCurrentOwner(response.data);
          console.log("current Owner data:", response.data);
        } catch (error) {
          console.error("Error fetching current owner:", error);
          localStorage.removeItem("token");
          setCurrentOwner(null);
        }
      }
      setLoading(false);
    };

    fetchCurrentOwner();
  }, []);

  if (loading) {
    return (
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "100vh",
        }}
      >
        <CircularProgress />
      </div>
    );
  }

  if (!currentOwner && !["/", "/register"].includes(location.pathname)) {
    return <Navigate to="/" />;
  }

  return (
    <>
      {shouldShowNavBar && <NavBar currentOwner={currentOwner} />}
      <Container>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/logout" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route
            path="/owners"
            element={
              <OwnerList currentOwner={currentOwner} isEditing={false} />
            }
          />
          <Route
            path="/profile"
            element={<OwnerForm currentOwner={currentOwner} isEditing={true} />}
          />
          <Route
            path="/owners/new"
            element={
              <OwnerForm currentOwner={currentOwner} isEditing={false} />
            }
          />
          <Route
            path="/owners/:id"
            element={<OwnerForm currentOwner={currentOwner} isEditing={true} />}
          />
          <Route
            path="/property-items"
            element={
              <PropertyItemList currentOwner={currentOwner} isEditing={false} />
            }
          />
          <Route
            path="/property-items/new"
            element={<PropertyItemForm currentOwner={currentOwner} />}
          />
          <Route
            path="/property-items/:id"
            element={
              <PropertyItemForm currentOwner={currentOwner} isEditing={true} />
            }
          />
          <Route
            path="/repairs"
            element={
              <RepairList currentOwner={currentOwner} isEditing={false} />
            }
          />
          <Route
            path="/repairs/new"
            element={<RepairForm currentOwner={currentOwner} />}
          />
          <Route
            path="/repairs/:id"
            element={
              <RepairForm currentOwner={currentOwner} isEditing={true} />
            }
          />
        </Routes>
      </Container>
    </>
  );
};

const AppWrapper = () => (
  <Router>
    <App />
  </Router>
);

export default AppWrapper;
