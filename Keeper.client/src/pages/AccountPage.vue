<template>
  <div class="d-flex justify-content-center mt-5">
    <img class="coverImg" :src="account.coverImg" alt="">
  </div>
  <div class="d-flex justify-content-center pt-5">
    <img class="accountImg" :src="account.picture" :alt="account.name">
  </div>
  <div class="d-flex justify-content-center">
    <h1 class="me-3">{{ account.name }}</h1>
    <button data-bs-toggle="modal" data-bs-target="#accountForm" class="btn btn-primary my-2">Edit Account</button>
  </div>
  <div class="d-flex justify-content-center">
    <p>Vaults {{ vaults.length }} | Keeps {{ keeps.length }}</p>
  </div>
  <div>
    <div v-for="v in vaults" :key="v.id" class="vault-div m-1">

    </div>
  </div>
</template>

<script>
import { computed, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
import { useRoute } from "vue-router";
export default {
  setup() {
    const route = useRoute()

    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults()
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }
    async function getMyKeeps() {
      try {
        const profileId = AppState.account.id
        await keepsService.getMyKeeps(profileId)
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }

    // onMounted(() => {
    //   getMyVaults()
    //   getMyKeeps()
    // })
    watchEffect(() => {
      route
      getMyKeeps()
      getMyVaults()
    })
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.myKeeps),
      vaults: computed(() => AppState.myVaults)

    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.accountImg {
  width: 10vw;
  height: fit-content;
  object-fit: cover;
  object-position: center;
  border-radius: 50%;
}

.coverImg {
  min-width: 50vh;
  height: 40vh;
}

.vault-div {
  width: 20vw;
}
</style>
