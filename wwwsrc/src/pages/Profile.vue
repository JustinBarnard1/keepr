<template>
  <div class="profile container-fluid">
    <h1 v-if="this.$route.params != profile.id">
      {{ searchedProfile.name }}'s Page
    </h1>
    <h1 v-else>{{ profile.name }}'s Page</h1>
    <div class="row">
      <div class="col-12 d-flex">
        <h1>Vaults</h1>
        <i
          class="pluslg fa fa-plus text-success mx-1"
          v-if="profile.id == searchedProfile.id"
          @click="addVault"
          aria-hidden="true"
        ></i>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <vault-comp
          v-for="vault in vaults"
          :key="vault.creatorId"
          :vaultProp="vault"
        />
      </div>
    </div>
    <div class="row">
      <div class="col-12 d-flex">
        <h1>Keeps</h1>
        <i
          class="pluslg fa fa-plus text-success mx-1"
          v-if="profile.id == searchedProfile.id"
          @click="addVault"
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
import vaultcomp from "../components/VaultComp";
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
      return this.$store.state.vaults;
    },
    searchedVaults() {
      return this.$store.state.searchedVaults;
    },
  },
  mounted() {
    this.$store.dispatch("getProfileKeeps", this.$route.params.profileId);
    this.$store.dispatch("getSearchedProfile", this.$route.params.profileId);
    this.$store.dispatch("getProfileVaults", this.$route.params.profileId);
    console.log(this.$route.params);
    console.log(profile.id);
  },
  methods: {},
  components: {
    keepComp,
  },
};
</script>


<style scoped>
.pluslg {
  zoom: 2;
  align-self: center;
}
</style>