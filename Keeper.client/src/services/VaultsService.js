import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {

  async createVault(formData) {
    const res = await api.post('api/vaults', formData)
    logger.log('res data vault form create', res.data)
    AppState.activeVault = new Vault(res.data)
    return AppState.activeVault
  }
  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.activeVault = new Vault(res.data)
  }
  async getMyVaults() {
    const res = await api.get('account/vaults')
    AppState.myVaults = res.data.map(v => new Vault(v))
    logger.log(AppState.myVaults, 'my vaults')
  }

}




export const vaultsService = new VaultsService()