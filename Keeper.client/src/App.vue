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

  <FormModalComponent id="keepForm">
    <template #header>
      Create a Keep!
    </template>
    <template #body>
      <KeepForm />
    </template>
  </FormModalComponent>

  <FormModalComponent id="vaultForm">
    <template #header>
      Create a Vault!
    </template>
    <template #body>
      <VaultForm />
    </template>
  </FormModalComponent>

  <FormModalComponent id="accountForm">

    <template #header>
      Edit Account!
    </template>
    <template #body>
      <AccountForm />
    </template>

  </FormModalComponent>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import ModalComponent from "./components/ModalComponent.vue"
import KeepDetailsComponent from "./components/KeepDetailsComponent.vue"
import FormModalComponent from "./components/FormModalComponent.vue"
import KeepForm from "./components/KeepForm.vue"
import VaultForm from "./components/VaultForm.vue"
import AccountForm from "./components/AccountForm.vue"

export default {
  setup() {
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
