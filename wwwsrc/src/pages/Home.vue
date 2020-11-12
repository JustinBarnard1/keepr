<template>
  <div class="home container-fluid bg-secondary">
    <div class="row d-flex flex-column">
      <h1 class="mt-3 col text-center">Welcome</h1>
    </div>
    <div class="row my-3 d-flex justify-content-around">
      <keep-comp v-for="keep in keeps" :key="keep.id" :keepProp="keep" />
    </div>
  </div>
</template>

<script>
import keepComp from "../components/KeepComp";
export default {
  name: "home",
  data() {
    return {
      keepData: {},
    };
  },
  computed: {
    keeps() {
      return this.$store.state.keeps;
    },
    profile() {
      return this.$store.state.profile;
    },
  },
  methods: {},
  components: {
    keepComp,
  },
  mounted() {
    this.$store.dispatch("getKeeps");
  },
};
</script>
<style scoped>
.home {
  flex-grow: 1;
}
</style>