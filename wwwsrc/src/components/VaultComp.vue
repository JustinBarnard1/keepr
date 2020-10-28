<template>
  <div
    v-if="
      vaultProp.isPrivate == false ||
      (vaultProp.isPrivate == true &&
        this.$route.params.profileId == vaultProp.creatorId)
    "
    class="vault-comp card col-3 justify-content-center m-3"
  >
    <i
      class="fa fa-times text-danger"
      v-if="profile.id == vaultProp.creatorId"
      @click="deleteVault"
      aria-hidden="true"
    ></i>
    <img :src="vaultProp.img" alt="" />
    <h2 @click="viewVault">{{ vaultProp.name }}</h2>
    <p>{{ vaultProp.description }}</p>
  </div>
</template>


<script>
export default {
  name: "vault-comp",
  data() {
    return {};
  },
  computed: {
    profile() {
      return this.$store.state.profile;
    },
  },
  methods: {
    deleteVault() {
      let c = confirm("Are you sure want to delete this?");
      if (c == true) {
        let payload = {};
        payload.id = this.vaultProp.id;
        payload.route = this.$route.params.profileId;
        this.$store.dispatch("deleteVault", payload);
      }
    },
    viewVault() {
      this.$router.push({
        name: "Vault",
        params: { vaultId: this.vaultProp.id },
      });
      this.$store.dispatch("getKeepsByVaultId", this.$route.params.vaultId);
    },
  },
  components: {},
  props: ["vaultProp"],
};
</script>


<style scoped>
</style>