<template>
  <div class="vault">
    <h1>Vault Page</h1>
    <keep-comp v-for="keep in keeps" :key="keep.id" :keepProp="keep" />
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
  methods: {
    createVault() {
      console.log("create vault");
    },
    deleteVault() {
      console.log("delete vault");
    },
  },
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