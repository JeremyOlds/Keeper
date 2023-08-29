<template>
  <div class="container">
    <div v-for="k in keeps" :key="k.id">
      <KeepComponent :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js"
import { AppState } from "../AppState.js";
import KeepComponent from "../components/KeepComponent.vue";
import { useRoute } from "vue-router";
import { Account } from "../models/Account.js";
import { vaultsService } from "../services/VaultsService.js";

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
    watchEffect(() => {
      route
      getKeeps()
      if (AppState.account.id) {
        getMyVaults()
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepComponent }
}
</script>

<style scoped lang="scss">
.container {
  display: grid;
  gap: 10px;
  grid-template-columns: repeat(auto-fill, minmax(240px, 1fr));
  grid-template-rows: minmax(10px, 1fr);
}

@media screen and (max-width: 769px) {
  .container {
    display: grid;
    gap: 10px;
    grid-template-columns: repeat(auto-fill, minmax(120px, 1fr));
    grid-template-rows: minmax(10px, 1fr);
  }
}
</style>
