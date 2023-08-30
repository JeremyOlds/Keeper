<template>
  <form @submit.prevent="createKeep()">
    <div class="">
      <label for="name">Name</label>
      <input title="Keep name" class="form-control" v-model="editable.name" type="text" required maxlength="50">
    </div>
    <div class="">
      <label for="img">Img Url</label>
      <input title="Keep image url" class="form-control" v-model="editable.img" id="img" type="url" maxlength="500"
        required>
    </div>
    <div class="">
      <label for="description">Description</label>
      <textarea title="Keep description" class="form-control mb-2" v-model="editable.description" name="description"
        id="description" cols="30" rows="10"></textarea>
    </div>
    <div>
      <input class="btn btn-success" type="submit">
    </div>
  </form>
</template>


<script>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";

export default {
  setup() {
    const editable = ref({})

    return {
      editable,

      async createKeep() {
        try {
          const formData = editable.value
          await keepsService.createKeep(formData)
          Modal.getOrCreateInstance('#keepForm').hide()
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
