import React from "react";
import { AppBar, Toolbar, Button, Typography, Box } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import { Container } from "@mui/material";
import HomeOutlinedIcon from "@mui/icons-material/HomeOutlined";
import ManageAccountsOutlinedIcon from "@mui/icons-material/ManageAccountsOutlined";
import BusinessOutlinedIcon from "@mui/icons-material/BusinessOutlined";
import BuildOutlinedIcon from "@mui/icons-material/BuildOutlined";
import LogoutOutlinedIcon from "@mui/icons-material/LogoutOutlined";

const NavBar = (props) => {
  const navigate = useNavigate();

  const handleLogout = () => {
    navigate("/logout");
  };

  return (
    <AppBar position="static">
      <Container>
        <Toolbar>
          {/* "Technico" Label */}
          <Typography variant="h6" sx={{ flexGrow: 1 }}>
            Technico
          </Typography>
          {/* Navigation Buttons */}
          {props.currentOwner.role === "Admin" ? (
            <Button
              color="inherit"
              component={Link}
              to="/owners"
              startIcon={<ManageAccountsOutlinedIcon />}
            >
              Owners
            </Button>
          ) : (
            <Button
              color="inherit"
              component={Link}
              to="/profile"
              startIcon={<ManageAccountsOutlinedIcon />}
            >
              My Profile
            </Button>
          )}
          <Button
            color="inherit"
            component={Link}
            to="/property-items"
            startIcon={<BusinessOutlinedIcon />}
          >
            Properties
          </Button>
          <Button
            color="inherit"
            component={Link}
            to="/repairs"
            startIcon={<BuildOutlinedIcon />}
          >
            Repairs
          </Button>

          {/* Logout Button */}
          <Box sx={{ marginLeft: "auto" }}>
            <Button
              color="inherit"
              onClick={handleLogout}
              startIcon={<LogoutOutlinedIcon />}
            >
              <Typography variant="span" sx={{ flexGrow: 1 }}>
                {props.currentOwner.name} {props.currentOwner.surname} (
                {props.currentOwner.role})
              </Typography>
            </Button>
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
};

export default NavBar;
