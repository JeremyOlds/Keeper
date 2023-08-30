import { AppState } from "../AppState.js"

class AppstateService {
  clearProfileAppState() {
    AppState.activeProfile = {}
    AppState.activeKeep = {}
    AppState.keeps = []
    AppState.vaults = []
  }
  clearAppstateVaultPage() {
    AppState.activeVault = {}
    AppState.keeps = []
    AppState.activeKeep = {}
  }

}

export const appstateService = new AppstateService()