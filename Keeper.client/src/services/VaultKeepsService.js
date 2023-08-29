import { api } from "./AxiosService.js"

class VaultKeepsService {
  async createVaultKeep(formData) {
    await api.post(`api/vaultKeeps`, formData)

  }
}


export const vaultKeepsService = new VaultKeepsService()