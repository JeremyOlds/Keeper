<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>

  <ModalComponent id="keepDetails">

    <template #body>
      <KeepDetailsComponent />
    </template>
  </ModalComponent>

  <FormModalComponent id="keepForm" aria-labelledby="keepForm">
    <template #header>
      Create a Keep!
    </template>
    <template #body>
      <KeepForm />
    </template>
  </FormModalComponent>

  <FormModalComponent id="vaultForm" aria-labelledby="vaultForm">
    <template #header>
      Create a Vault!
    </template>
    <template #body>
      <VaultForm />
    </template>
  </FormModalComponent>

  <FormModalComponent id="accountForm" aria-labelledby="accountForm">

    <template #header>
      Edit Account!
    </template>
    <template #body>
      <AccountForm />
    </template>

  </FormModalComponent>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import ModalComponent from "./components/ModalComponent.vue"
import KeepDetailsComponent from "./components/KeepDetailsComponent.vue"
import FormModalComponent from "./components/FormModalComponent.vue"
import KeepForm from "./components/KeepForm.vue"
import VaultForm from "./components/VaultForm.vue"
import AccountForm from "./components/AccountForm.vue"
import { vaultsService } from "./services/VaultsService.js"
import Pop from "./utils/Pop.js"
import { logger } from "./utils/Logger.js"

export default {
  setup() {

    watchEffect(() => {
      if (AppState.account.id) {
        getMyVaults()
      }
    })

    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults()
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }

    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, ModalComponent, KeepDetailsComponent, FormModalComponent, KeepForm, VaultForm, AccountForm }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
