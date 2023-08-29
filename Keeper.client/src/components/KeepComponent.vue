<template>
  <div @click="getActiveKeep(keep.id)" class="relative elevation-3 selectable" data-bs-toggle="modal"
    data-bs-target="#keepDetails">
    <img class="img-fluid" style="width: 100%;" :src="keep.img" :alt="keep.name">
    <p class="absolute fs-5 fw-bold m-0 ">{{ keep.name }}</p>
    <img class="avatar" :title="keep.creator.name" :src="keep.creator.picture" :alt="keep.creator.name">
  </div>
</template>


<script>
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: {
    keep: { type: Keep, required: true }
  },

  setup() {


    return {

      async getActiveKeep(keepId) {
        try {
          await keepsService.getActiveKeep(keepId)
        } catch (error) {
          Pop.error(error.message)
          logger.log(error)
        }
      }

    }
  }
}
</script>


<style lang="scss" scoped>
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

.avatar {
  object-fit: cover;
  position: absolute;
  bottom: 8px;
  right: 16px;
  height: 5vh;
  width: 5vh;
  border-radius: 50%;
}

.name {
  text-shadow: 5px 5px blue;
}
</style>