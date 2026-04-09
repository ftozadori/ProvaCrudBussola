import api from "../api/axios";
import type { Viagem } from "../types/Viagem";

export const viagemService = {
  async getAll(): Promise<Viagem[]> {
    const response = await api.get("/Viagem");
    return response.data;
  },

  async create(viagem: Viagem): Promise<Viagem> {
    const response = await api.post("/Viagem", viagem);
    return response.data;
  },

  async update(id: string, viagem: Viagem): Promise<Viagem> {
    const response = await api.put(`/Viagem/${id}`, viagem);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await api.delete(`/Viagem/${id}`);
  },
};