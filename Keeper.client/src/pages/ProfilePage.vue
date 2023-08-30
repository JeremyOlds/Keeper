<template>
  <div class="d-flex justify-content-center mt-5">
    <img class="coverImg rounded" :src="profile.coverImg" alt="">
  </div>
  <div class="d-flex justify-content-center pt-5">
    <img class="accountImg" :src="profile.picture" :alt="profile.name">
  </div>
  <div class="d-flex justify-content-center">
    <h1 class="me-3">{{ profile.name }}</h1>
  </div>
  <div class="d-flex justify-content-center">
    <p>Vaults {{ vaults.length }} | Keeps {{ keeps.length }}</p>
  </div>
  <div class="">
    <div class="border-dark border-bottom mb-3">
      <h2>Vaults</h2>
    </div>
    <div class="masonry-with-columns">
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
    <div class="masonry-with-columns">
      <div v-for="k in keeps" :key="k.id" class="col-md-3 col-12 my-2">
        <KeepComponent :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onUnmounted, watchEffect } from "vue";
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { profilesService } from "../services/ProfilesService.js"
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import { appstateService } from "../services/AppstateService.js";
import { AppState } from "../AppState.js";

export default {
  setup() {

    const route = useRoute()

    async function getProfileById() {
      try {
        const profileId = route.params.profileId
        await profilesService.getProfileById(profileId)
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }
    async function getProfileKeeps() {
      try {
        const profileId = route.params.profileId
        await keepsService.getProfileKeeps(profileId)
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }
    async function getProfileVaults() {
      try {
        const profileId = route.params.profileId
        await vaultsService.getProfileVaults(profileId)
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }
    function clearProfileAppState() {
      try {
        appstateService.clearProfileAppState()
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }

    watchEffect(() => {
      route.params.profileId
      getProfileById()
      getProfileKeeps()
      getProfileVaults()
    })
    onUnmounted(() => {
      clearProfileAppState()
    })
    return {
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      profile: computed(() => AppState.activeProfile)
    }
  }
}
</script>


<style lang="scss" scoped>
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
  // height: 40vh;
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