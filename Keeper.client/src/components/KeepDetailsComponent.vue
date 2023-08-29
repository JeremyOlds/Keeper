<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-6 col-12 px-0">
        <img class="img-fluid rounded" :src="keep.img" alt="">
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
        <div class="row justify-content-around">
          <div class="col-6 d-flex">
            <div class="dropdown">
              <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1"
                data-bs-toggle="dropdown" aria-expanded="false">
                save to vault
              </button>
              <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                <li v-for="v in vaults" :key="v.id"><a class="dropdown-item" @click="createVaultKeep(v.id)">{{ v.name
                }}</a></li>

              </ul>
            </div>
          </div>
          <div class="col-6 d-flex text-center pb-2">
            <img class="avatar" :src="keep.creator?.picture" :alt="keep.creator?.name">
            <p class="m-0 d-flex align-items-center ps-2">{{ keep.creator?.name }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js"

export default {
  setup() {



    return {
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.myVaults),

      async createVaultKeep(vaultId) {
        try {
          const formData = { vaultId: vaultId, keepId: AppState.activeKeep.id }
          await vaultKeepsService.createVaultKeep(formData)
          AppState.activeKeep.kept++
        } catch (error) {
          Pop.error(error.response?.data)
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