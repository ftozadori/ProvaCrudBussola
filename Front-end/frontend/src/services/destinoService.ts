import api from "../api/axios";
import type { Destino } from "../types/Destino";

export const destinoService = {
  async getAll(): Promise<Destino[]> {
    const response = await api.get("/Destino");
    return response.data;
  },

  async getById(id: string): Promise<Destino> {
    const response = await api.get(`/Destino/${id}`);
    return response.data;
  },

  async create(destino: Destino): Promise<Destino> {
    const response = await api.post("/Destino", destino);
    return response.data;
  },

  async update(id: string, destino: Destino): Promise<Destino> {
    const response = await api.put(`/Destino/${id}`, destino);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await api.delete(`/Destino/${id}`);
  },
};