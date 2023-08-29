import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {

  async getKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(k => new Keep(k))
    logger.log('Appstate keeps', AppState.keeps)
  }
  async getActiveKeep(keepId) {
    const res = await api.get(`api/keeps/${keepId}`)
    // logger.log("getting active Keep from server", res.data)
    AppState.activeKeep = new Keep(res.data)
    // logger.log('appstate active keep', AppState.activeKeep)
  }
  async createKeep(formData) {
    const res = await api.post('api/keeps', formData)
    // logger.log('res data keep create', res.data)
    const keep = new Keep(res.data)
    AppState.keeps.unshift(keep)
  }
  async getKeepsByVaultId(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    AppState.keeps = res.data.map(k => new Keep(k))
  }
  async getMyKeeps(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    AppState.myKeeps = res.data.map(k => new Keep(k))
    logger.log(AppState.myKeeps, 'mykeeps appstate')
  }

}

export const keepsService = new KeepsService()