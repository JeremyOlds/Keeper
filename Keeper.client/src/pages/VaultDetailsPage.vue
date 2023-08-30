<template>
  <div class="pt-5 mt-5 d-flex justify-content-center relative-vault">
    <img class="vaultImg rounded" :src="vault.img" alt="">
    <h1 class="absolute-vault mb-0 name">{{ vault.name }}</h1>
    <h1 class="absolute-lock name" v-if="vault.isPrivate == true"><i class="mdi mdi-lock"></i></h1>
  </div>
  <div class="d-flex justify-content-center py-3">
    <p class="bg-info rounded p-2 fw-bold">Keeps {{ keeps.length }}</p>
    <div>
      <button v-if="account.id == vault.creator?.id" @click="removeVault(vault.id)" class="btn btn-danger"
        title="Delete vault">Delete Vault</button>
      <button v-if="account.id == vault.creator?.id" @click="ToggleVaultPrivate()" title="Toggle Private"
        class="btn btn-primary">Toggle Private</button>
    </div>
  </div>
  <div class="masonry-with-columns px-3">
    <div v-for="k in keeps" :key="k.id">
      <KeepComponent :keep="k" />
    </div>
  </div>
</template>


<script>
import { computed, onMounted, onUnmounted } from "vue";
import { AppState } from "../AppState.js";
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";
import { keepsService } from "../services/KeepsService.js";
import { router } from "../router.js";
import KeepComponent from "../components/KeepComponent.vue";
import { appstateService } from "../services/AppstateService.js";

export default {
  setup() {
    const route = useRoute();
    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.vaultId);
      }
      catch (error) {
        Pop.error(error.response.data);
        logger.log(error);
        if (error.response.data.includes('private')) {
          router.push({ name: 'Home' });
        }
      }
    }
    async function getKeepsByVaultId() {
      try {
        await keepsService.getKeepsByVaultId(route.params.vaultId);
      }
      catch (error) {
        Pop.error(error.response.data);
        logger.log(error);
      }
    }

    // watchEffect(() => {
    //   route.params.VaultId;
    //   if (AppState.account.id) {
    //     getVaultById();
    //     getKeepsByVaultId();
    //   }
    // })

    onMounted(() => {
      getVaultById();
      getKeepsByVaultId();

    })

    function clearAppstateVaultPage() {
      try {
        appstateService.clearAppstateVaultPage()
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }
    onUnmounted(() => {
      clearAppstateVaultPage()
    })
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),

      async removeVault(vaultId) {
        try {
          const remove = await Pop.confirm('Are you sure you want to delete this vault?')
          if (remove == false) {
            return
          }
          await vaultsService.removeVault(vaultId)
          router.push({ name: 'Home' })
        } catch (error) {
          Pop.error(error.response?.data || error.message)
          logger.log(error)
        }
      },
      async ToggleVaultPrivate() {
        try {
          let flippedValue = AppState.activeVault.isPrivate
          flippedValue = !flippedValue
          const editable = { isPrivate: flippedValue }
          vaultsService.ToggleVaultPrivate(editable, AppState.activeVault.id)
        } catch (error) {
          Pop.error(error.message)
          logger.log
        }
      }
    };
  },
  components: { KeepComponent }
}
</script>


<style lang="scss" scoped>
.vaultImg {
  width: 30vw;
  height: fit-content;
  object-fit: cover;
  object-position: center;
}

.name {
  color: black;
  text-shadow:
    -1px -1px 0 white,
    1px -1px 0 white,
    -1px 1px 0 white,
    1px 1px 0 white;
}

.relative-vault {
  position: relative;
  text-align: center;
  text-shadow: 0px 0px 5px white;
}

.absolute-vault {
  position: absolute;
  top: 55%;
  left: 50%;
  transform: translate(-50%, -50%);

}

.absolute-lock {
  position: absolute;
  top: 45%;
  left: 50%;
  transform: translate(-50%, -50%);

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
  .vaultImg {
    object-fit: cover;
    object-position: center;
    width: 90vw;
    height: fit-content;
  }

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