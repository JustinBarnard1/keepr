<template>
  <div class="keep-details">
    <div class="card mb-3" style="max-width: 700px">
      <div class="row no-gutters">
        <div class="col-md-4">
          <img :src="keep.img" class="card-img" alt="..." />
        </div>
        <div class="col-md-8">
          <div class="card-body">
            <span
              >Views: {{ keep.views }} - Keeps: {{ keep.keeps }} - Shares:
              {{ keep.shares }}</span
            >
            <h5 class="card-title">{{ keep.name }}</h5>
            <p class="card-text">{{ keep.description }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "keep-details",
  data() {
    return {};
  },
  computed: {
    keep() {
      return this.$store.state.activeKeep;
    },
  },
  methods: {},
  components: {},
};
</script>


<style scoped>
</style>