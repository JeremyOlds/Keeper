<template>
  <div class="modal fade modal-lg" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-dialog-centered">
      <div class="modal-content bg-white text-dark">
        <div class="modal-body p-0">

          <slot name="body"></slot>

        </div>

      </div>
    </div>
  </div>
</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped></style>