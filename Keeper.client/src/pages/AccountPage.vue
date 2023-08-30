<template>
  <div class="d-flex justify-content-center mt-5">
    <img class="coverImg rounded" :src="account.coverImg" alt="">
  </div>
  <div class="d-flex justify-content-center pt-5">
    <img class="accountImg" :src="account.picture" :alt="account.name">
  </div>
  <div class="d-flex justify-content-center">
    <h1 class="me-3">{{ account.name }}</h1>
    <button data-bs-toggle="modal" title="edit Account" data-bs-target="#accountForm" class="btn btn-primary my-2">Edit
      Account</button>
  </div>
  <div class="d-flex justify-content-center">
    <p>Vaults {{ vaults.length }} | Keeps {{ keeps.length }}</p>
  </div>
  <div class="">
    <div class="border-dark border-bottom mb-3">
      <h2>Vaults</h2>
    </div>
    <div class="masonry-with-columns px-3">
      <div v-for="v in vaults" :key="v.id" class="relative col-md-3 col-12 mb-2 rounded">
        <router-link :to="{ name: 'VaultDetails', params: { vaultId: v.id } }">
          <img class="vault-img" :src="v.img" alt="">
          <p class="absolute fs-5 fw-bold m-0 name">{{ v.name }}</p>
          <p v-if="v.isPrivate == true" class="private fs-5 fw-bold m-0 text-dark name"><i class="mdi mdi-lock"></i></p>
        </router-link>
      </div>
    </div>
  </div>
  <div class="">
    <div class="border-bottom border-dark mb-3">
      <h2>Keeps</h2>
    </div>
    <div class="masonry-with-columns px-3">
      <div v-for="k in keeps" :key="k.id" class="col-md-3 col-12 my-2">
        <KeepComponent :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onUnmounted, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
import { useRoute } from "vue-router";
import KeepComponent from "../components/KeepComponent.vue";
import { appstateService } from "../services/AppstateService.js";
export default {
  setup() {
    const route = useRoute();
    async function getMyVaults() {
      try {
        await vaultsService.getAccountVaults();
      }
      catch (error) {
        Pop.error(error.message);
        logger.log(error);
      }
    }
    async function getMyKeeps() {
      try {
        const profileId = AppState.account.id;
        await keepsService.getMyKeeps(profileId);
      }
      catch (error) {
        Pop.error(error.message);
        logger.log(error);
      }
    }
    function clearAppStateAccountPage() {
      try {
        appstateService.clearProfileAppState()
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }

    watchEffect(() => {
      route;
      getMyKeeps();
      getMyVaults();
    })
    onUnmounted(() => {
      clearAppStateAccountPage()
    })
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults)
    };
  },
  components: { KeepComponent }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 4 200px;
  column-gap: 1rem;


  div {
    width: 150px;
    display: inline-block;
    width: 100%;
    margin-bottom: .5rem;
    margin-top: .5rem;
  }
}

.vault-img {
  width: 100%;
  min-height: 100%;
}

.name {
  color: black;
  text-shadow:
    -1px -1px 0 white,
    1px -1px 0 white,
    -1px 1px 0 white,
    1px 1px 0 white;
}

.accountImg {
  width: 10vw;
  height: fit-content;
  object-fit: cover;
  object-position: center;
  border-radius: 50%;
}

.coverImg {
  max-width: 20%;
  // height:;
}

.vault-div {
  width: 100%;
}

.relative {
  position: relative;
  text-align: center;
  text-shadow: 0px 0px 2px whitesmoke;

}

.absolute {
  position: absolute;
  bottom: 8px;
  left: 16px;
}

.private {
  position: absolute;
  bottom: 8px;
  right: 16px;
}


@media screen and (max-width: 769px) {
  .masonry-with-columns {
    columns: 2 100px;
    column-gap: 1rem;


    div {
      width: 150px;
      display: inline-block;
      width: 100%;
      margin-bottom: .5rem;
      margin-top: .5rem;
      // margin: 0 1rem 1rem 0;
    }
  }
}
</style>
