<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-6 col-12 px-0">
        <img class="img-fluid rounded" style="height: 100%;" :src="keep.img" alt="">
      </div>
      <div class="col-md-6 col-12 d-flex flex-column justify-content-between">
        <div class="row pt-3">
          <div class="col-6 text-end"><span><i class="mdi mdi-eye"></i></span> {{ keep.views }}</div>
          <div class="col-6 text-start"><span><i class="mdi mdi-alpha-k-box-outline"></i></span> {{ keep.kept }}</div>
        </div>
        <div class="row">
          <div>
            <h1 class="text-center">{{ keep.name }}</h1>
          </div>
          <div>
            <p>{{ keep.description }}</p>
          </div>
        </div>
        <div class="row">
          <div v-if="account.id == keep.creatorId" class="col-6 d-flex justify-content-end ">
            <button @click="removeKeep(keep.id)" title="Remove Keep" class="btn btn-danger my-1 mx-1">Remove Keep</button>
          </div>
          <div v-if="activeVault.id && account.id == activeVault.creator.id" class="col-6 d-flex justify-content-start">
            <button @click="removeVaultKeep(keep.vaultKeepId)" class="btn btn-danger mx-1 my-1">Remove from Vault</button>
          </div>
        </div>
        <div class="row justify-content-around">
          <div class="col-6 d-flex">
            <div class="dropdown">
              <button v-if="account.id" title="save to vault" class="btn btn-secondary dropdown-toggle" type="button"
                id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                save to vault
              </button>
              <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                <li v-for="v in vaults" :key="v.id"><a class="dropdown-item selectable" @click="createVaultKeep(v.id)">{{
                  v.name
                }}</a></li>

              </ul>
            </div>

          </div>
          <div class="col-6 d-flex text-center pb-2">
            <img @click="routeToAccount()" class="avatar selectable" :src="keep.creator?.picture"
              :alt="keep.creator?.name">
            <p class="m-0 d-flex align-items-center ps-2">{{ keep.creator?.name }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, } from "vue";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js"
import { router } from "../router.js";
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";

export default {
  setup() {

    return {
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.myVaults),
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),

      async createVaultKeep(vaultId) {
        try {
          const formData = { vaultId: vaultId, keepId: AppState.activeKeep.id }
          await vaultKeepsService.createVaultKeep(formData)
          AppState.activeKeep.kept++
        } catch (error) {
          Pop.error(error.response?.data)
          logger.log(error)
        }
      },
      async removeKeep(keepId) {
        try {
          const remove = await Pop.confirm('Are you sure you want to delete this keep?')
          if (remove == false) {
            return
          }
          await keepsService.removeKeep(keepId)
          Modal.getOrCreateInstance('#keepDetails').hide()
        } catch (error) {
          Pop.error(error.response?.data || error.message)
          logger.log(error)
        }
      },

      routeToAccount() {
        try {
          router.push({ name: 'Profile', params: { profileId: AppState.activeKeep.creatorId } })
          Modal.getOrCreateInstance('#keepDetails').hide()
        } catch (error) {
          Pop.error(error.message)
          logger.log(error)

        }
      },
      async removeVaultKeep(vaultKeepId) {
        try {
          const remove = await Pop.confirm('are you sure you want to remove this keep from your vault?')
          if (remove == false) {
            return
          }
          await vaultKeepsService.removeVaultKeep(vaultKeepId)
          Modal.getOrCreateInstance('#keepDetails').hide()
        } catch (error) {
          Pop.error(error.response.data || error.message)
          logger.log(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.avatar {
  border-radius: 50%;
  height: 5vh;
  width: 5vh;
}
</style>