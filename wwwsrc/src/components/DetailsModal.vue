<template>
  <div class="modal fade" :id="id" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-lg" role="document">
      <div class="modal-content" :class="color">
        <slot name="body"></slot>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "DetailModal",
  data() {
    return {};
  },
  computed: {},
  methods: {},
  components: {},
  props: {
    id: {
      type: String,
      required: true,
    },
    color: {
      type: String,
      default: "bg-white",
    },
  },
};
</script>


<style scoped>
.modal-lg {
  width: 100% !important;
}
</style>