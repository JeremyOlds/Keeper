import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  async createVaultKeep(formData) {
    await api.post(`api/vaultKeeps`, formData)

  }
  async removeVaultKeep(vaultKeepId) {
    await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    const keepIndex = AppState.keeps.findIndex(k => k.vaultKeepId == vaultKeepId)
    AppState.keeps.splice(keepIndex, 1)
    AppState.activeKeep = {}
  }
}


export const vaultKeepsService = new VaultKeepsService()