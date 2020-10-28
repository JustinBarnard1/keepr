<template>
  <div class="vault container-fluid">
    <h1>Vault Page</h1>
    <div v-if="keeps" class="row">
      <keep-comp v-for="keep in keeps" :key="keep.id" :keepProp="keep" />
    </div>
  </div>
</template>


<script>
import keepComp from "../components/KeepComp";
export default {
  name: "vault",
  data() {
    return {};
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
    this.$store.dispatch("getKeepsByVaultId", this.$route.params.vaultId);
  },
};
</script>


<style scoped>
</style>