<template>
  <div class="profile container-fluid">
    <div v-if="this.$route.params != profile.id">
      <h1>{{ searchedProfile.name }}'s Page</h1>
    </div>
    <div v-else>
      <h1>{{ profile.name }}'s Page</h1>
    </div>
    <div class="row">
      <div class="col-12 d-flex">
        <h1>Vaults</h1>
        <i
          class="pluslg fa fa-plus text-success mx-1"
          v-if="profile.id == searchedProfile.id"
          aria-hidden="true"
        ></i>
      </div>
    </div>
    <div v-if="vaults != null" class="row">
      <vault-comp
        class="col-3 d-flex flex-column"
        v-for="vault in vaults"
        :key="vault.id"
        :vaultProp="vault"
      />
    </div>
    <div v-else>
      <h1>No Vaults</h1>
    </div>
    <div class="row">
      <div class="col-12 d-flex">
        <h1>Keeps</h1>
        <i
          class="pluslg fa fa-plus text-success mx-1"
          v-if="profile.id == searchedProfile.id"
          aria-hidden="true"
        ></i>
      </div>
    </div>
    <div class="row">
      <keep-comp v-for="keep in keeps" :key="keep.id" :keepProp="keep" />
    </div>
  </div>
</template>


<script>
import vaultComp from "../components/VaultComp";
import keepComp from "../components/KeepComp";
export default {
  name: "profile",
  data() {
    return {};
  },
  computed: {
    keeps() {
      return this.$store.state.profileKeeps;
    },
    searchedProfile() {
      return this.$store.state.searchedProfile;
    },
    profile() {
      return this.$store.state.profile;
    },
    vaults() {
      return this.$store.state.profileVaults;
    },
    // searchedVaults() {
    //   return this.$store.state.searchedVaults;
    // },
  },
  mounted() {
    this.$store.dispatch("getProfile");
    this.$store.dispatch("getProfileKeeps", this.$route.params.profileId);
    this.$store.dispatch("getSearchedProfile", this.$route.params.profileId);
    this.$store.dispatch("getProfileVaults", this.$route.params.profileId);
  },
  methods: {},
  components: {
    keepComp,
    vaultComp,
  },
};
</script>


<style scoped>
.pluslg {
  zoom: 2;
  align-self: center;
}
</style>