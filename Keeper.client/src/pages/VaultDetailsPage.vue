<template>
  <div class="pt-5 mt-5 d-flex justify-content-center relative">
    <img class="vaultImg rounded" :src="vault.img" alt="">
    <h1 class="absolute mb-0">{{ vault.name }}</h1>
  </div>
  <div class="d-flex justify-content-center py-3">
    <p class="bg-info rounded p-2 fw-bold">Keeps {{ keeps.length }}</p>
  </div>
  <div>

  </div>
</template>


<script>
import { computed, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";
import { keepsService } from "../services/KeepsService.js";
import { router } from "../router.js";

export default {
  setup() {
    const route = useRoute()

    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.vaultId)
      } catch (error) {
        Pop.error(error.response.data)
        logger.log(error)
        if (error.response.data.includes('private')) {
          router.push({ name: 'Home' })
        }
      }
    }
    async function getKeepsByVaultId() {
      try {
        await keepsService.getKeepsByVaultId(route.params.vaultId)
      } catch (error) {
        Pop.error(error.response.data)
        logger.log(error)
      }
    }

    watchEffect(() => {
      route.params.VaultId
      getVaultById()
      getKeepsByVaultId()
    })


    return {

      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
    }
  }
}
</script>


<style lang="scss" scoped>
.vaultImg {
  width: 30vw;
  height: fit-content;
  object-fit: cover;
  object-position: center;
}

.relative {
  position: relative;
  text-align: center;
  text-shadow: 0px 0px 5px white;
}

.absolute {
  position: absolute;
  top: 55%;
  left: 50%;
  transform: translate(-50%, -50%);

}

@media screen and (max-width: 769px) {
  .vaultImg {
    object-fit: cover;
    object-position: center;
    width: 90vw;
    height: fit-content;
  }
}
</style>