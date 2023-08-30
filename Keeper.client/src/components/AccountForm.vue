<template>
  <form @submit.prevent="editAccount()">
    <div>
      <label for="name">Name</label>
      <input id="name" v-model="editable.name" class="form-control" type="text" maxlength="50">
    </div>
    <div>
      <label for="picture">Picture</label>
      <input id="picture" v-model="editable.picture" class="form-control" type="url" maxlength="500">
    </div>
    <div>
      <label for="coverImg">Cover Image</label>
      <input id="coverImg" v-model="editable.coverImg" class="form-control" type="url" maxlength="500">
    </div>
    <div class="mt-2">
      <input type="submit" class="btn btn-success">
    </div>
  </form>
</template>


<script>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { accountService } from "../services/AccountService.js";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,

      async editAccount() {
        try {
          const formData = editable.value
          await accountService.editAccount(formData)
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