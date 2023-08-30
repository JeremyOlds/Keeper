<template>
  <div class="masonry-with-columns px-3">
    <div v-for="k in keeps" :key="k.id">
      <KeepComponent :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, onUnmounted, watchEffect } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js"
import { AppState } from "../AppState.js";
import KeepComponent from "../components/KeepComponent.vue";
import { useRoute } from "vue-router";
import { vaultsService } from "../services/VaultsService.js";
import { appstateService } from "../services/AppstateService.js";

export default {
  setup() {
    const route = useRoute()
    onMounted(() => {
      getKeeps();

    });
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error(error.message);
        logger.log(error);
      }
    }
    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults()
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }
    function clearAppStateHomePage() {
      try {
        appstateService.clearProfileAppState()
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }
    // watchEffect(() => {
    //   route.fullPath
    //   getKeeps()
    //   if (AppState.account.id) {
    //     getMyVaults()
    //   }
    // })

    onMounted(() => {
      getKeeps()
    })

    onUnmounted(() => {
      clearAppStateHomePage()
    })
    return {
      keeps: computed(() => AppState.keeps)
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
    // margin: 0 1rem 1rem 0;
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



