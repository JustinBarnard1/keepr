<template>
  <div class="home container-fluid">
    <div class="row d-flex flex-column">
      <h1 class="col text-center">Welcome</h1>
    </div>
    <div class="row d-flex">
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
