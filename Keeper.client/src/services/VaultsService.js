import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {

  async createVault(formData) {
    const res = await api.post('api/vaults', formData)
    // logger.log('res data vault form create', res.data)
    AppState.activeVault = new Vault(res.data)
    return AppState.activeVault
  }
  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.activeVault = new Vault(res.data)
  }
  async getMyVaults() {
    const res = await api.get('account/vaults')
    AppState.vaults = res.data.map(v => new Vault(v))
    // logger.log(AppState.vaults, 'my vaults')
  }
  async getProfileVaults(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    AppState.vaults = res.data.map(v => new Vault(v))
  }
  async removeVault(vaultId) {
    await api.delete(`api/vaults/${vaultId}`)
    const foundVaultIndex = AppState.vaults.findIndex(v => v.id == vaultId)
    AppState.vaults.splice(foundVaultIndex, 1)
    AppState.activeVault = {}
  }
  async ToggleVaultPrivate(formData, vaultId) {
    await api.put(`api/vaults/${vaultId}`, formData)
    AppState.activeVault.isPrivate = !AppState.activeVault.isPrivate
  }

}




export const vaultsService = new VaultsService()