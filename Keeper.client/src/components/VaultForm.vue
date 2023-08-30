<template>
  <form @submit.prevent="createVault()">
    <div>
      <label for="vaultName">Name</label>
      <input title="Vault Name" v-model="editable.name" id="vaultName" type="text" required class="form-control"
        maxlength="50">
    </div>
    <div>
      <label for="vaultImg">Img</label>
      <input title="Vault Image" v-model="editable.img" class="form-control" type="url" required id="vaultImg"
        maxlength="500">
    </div>
    <div>
      <label for="vaultDescription">Description</label>
      <textarea title="aultDescription" v-model="editable.description" class="form-control" name="vaultDescription"
        id="vaultDescription" maxlength="500" cols="30" rows="10"></textarea>
    </div>
    <div>
      <label for="isPrivate">Is Private?</label>
      <input title="is the Vault Private?" v-model="editable.isPrivate" type="checkbox" id="isPrivate">
    </div>
    <div>
      <input type="submit" class="btn btn-success">
    </div>
  </form>
</template>


<script>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js"
import { router } from "../router.js";
import { Modal } from "bootstrap";

export default {


  setup() {
    const editable = ref({})


    return {
      editable,

      async createVault() {
        try {
          const formData = editable.value
          if (formData.isPrivate == null) {
            formData.isPrivate = false
          }
          const vault = await vaultsService.createVault(formData)
          router.push({ name: 'VaultDetails', params: { vaultId: vault.id } })
          Modal.getOrCreateInstance('#vaultForm').hide()
        } catch (error) {
          Pop.error(error.message)
          logger.log(error)
        }
      }

    }
  }
}
</script>


<style lang="scss" scoped></style>