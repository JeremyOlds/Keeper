import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {

  async getKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(k => new Keep(k))
    // logger.log('Appstate keeps', AppState.keeps)
  }
  async getActiveKeep(keep) {

    // FIXME save the keep to the AppState so then this still works for vault keeps
    await api.get(`api/keeps/${keep.id}`)
    keep.views++
    AppState.activeKeep = keep
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
    // logger.log('keeps by vault id appState', AppState.keeps)
  }
  async getMyKeeps(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    AppState.keeps = res.data.map(k => new Keep(k))
    // logger.log(AppState.keeps, 'mykeeps appstate')
  }
  async getProfileKeeps(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    AppState.keeps = res.data.map(k => new Keep(k))
  }
  async removeKeep(keepId) {
    await api.delete(`api/keeps/${keepId}`)
    const foundkeepIndex = AppState.keeps.findIndex(k => k.id == keepId)
    AppState.keeps.splice(foundkeepIndex, 1)
    AppState.activeKeep = {}
  }

}

export const keepsService = new KeepsService()