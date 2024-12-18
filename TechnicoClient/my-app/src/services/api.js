import axios from "axios";

const apiUrl = "https://localhost:7004";

const api = axios.create({
  baseURL: apiUrl,
});

export const fetchOwners = () => api.get("/api/Owners");
export const fetchOwnerById = (id) => api.get(`/api/Owners/${id}`);
export const createOwner = (data) => api.post("/api/Owners", data);
export const updateOwner = (id, data) => api.put(`/api/Owners/${id}`, data);
export const deleteOwner = (id) => api.delete(`/api/Owners/${id}`);

export const fetchPropertyItems = () => api.get("/api/PropertyItems");
export const fetchPropertyItemById = (id) =>
  api.get(`/api/PropertyItems/${id}`);
export const createPropertyItem = (data) =>
  api.post("/api/PropertyItems", data);
export const updatePropertyItem = (id, data) =>
  api.put(`/api/PropertyItems/${id}`, data);
export const deletePropertyItem = (id) =>
  api.delete(`/api/PropertyItems/${id}`);
export const fetchPropertyItemsByOwnerId = (id) =>
  api.get(`/api/PropertyItems/owner/${id}`);

export const fetchRepairs = () => api.get("/api/Repair");
export const fetchRepairById = (id) => api.get(`/api/Repair/${id}`);
export const createRepair = (data) => api.post("/api/Repair", data);
export const updateRepair = (id, data) => api.put(`/api/Repair/${id}`, data);
export const deleteRepair = (id) => api.delete(`/api/Repair/${id}`);
export const fetchRepairsByPropertyItemId = (id) =>
  api.get(`/api/Repair/propertyItem/${id}`);
export const fetchRepairsByOwnerId = (id) => api.get(`/api/Repair/owner/${id}`);

console.log(api);
export default api;
