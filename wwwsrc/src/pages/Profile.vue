<template>
  <div class="profile container-fluid">
    <div v-if="this.$route.params != profile.id" class="d-flex">
      <div>
        <img :src="searchedProfile.picture" alt="" />
      </div>
      <div>
        <h1>{{ searchedProfile.name }}'s Page</h1>
        <h4>Keeps: {{ keeps.length }}</h4>
        <h4>Vaults: {{ vaults.length }}</h4>
      </div>
    </div>
    <div v-else>
      <div>
        <img :src="profile.picture" alt="" />
      </div>
      <div>
        <h1>{{ profile.name }}'s Page</h1>
        <h4>Keeps: {{ keeps.length }}</h4>
        <h4>Vaults: {{ vaults.length }}</h4>
      </div>
    </div>
    <div class="row">
      <div class="col-12 d-flex">
        <h1>Vaults</h1>
        <i
          class="pluslg fa fa-plus text-success mx-1"
          v-if="profile.id == searchedProfile.id"
          aria-hidden="true"
          @click="creatingVault = true"
        ></i>
      </div>
      <div v-if="creatingVault">
        <div class="create-vault">
          <div class="card">
            <h2>Create A New Vault</h2>
            <form @submit.prevent="createVault">
              <p>
                Name: <input type="text" v-model="newVault.Name" required />
                <br />
                Description:
                <input type="text" v-model="newVault.Description" required />
                <br />
                Private:
                <input type="checkbox" v-model="newVault.IsPrivate" />
                <br />
                <button
                  @click="creatingVault = false"
                  class="mr-3 btn btn-outline-warning"
                >
                  Cancel
                </button>
                <button type="btn" class="btn btn-outline-info">Create</button>
              </p>
            </form>
          </div>
        </div>
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
          @click="creatingKeep = true"
        ></i>
      </div>
      <div v-if="creatingKeep">
        <div class="create-keep">
          <div class="card">
            <h2>Create A New Keep</h2>
            <form @submit.prevent="createKeep">
              <p>
                Name: <input type="text" v-model="newKeep.Name" required />
                <br />
                Description:
                <input type="text" v-model="newKeep.Description" required />
                <br />
                Image URL: <input type="text" v-model="newKeep.Img" required />
                <br />
                <button
                  @click="creatingKeep = false"
                  class="mr-3 btn btn-outline-warning"
                >
                  Cancel
                </button>
                <button type="btn" class="btn btn-outline-info">Create</button>
              </p>
            </form>
          </div>
        </div>
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
import createKeep from "../components/CreateKeep";
export default {
  name: "profile",
  data() {
    return {
      creatingKeep: false,
      creatingVault: false,
      newKeep: {
        Name: "",
        Description: "",
        Img: "",
      },
      newVault: {
        Name: "",
        Description: "",
        IsPrivate: false,
      },
    };
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
  watch: {
    "$route.params.profileId": function (profileId) {
      this.$store.dispatch("getProfileKeeps", this.$route.params.profileId);
      this.$store.dispatch("getSearchedProfile", this.$route.params.profileId);
      this.$store.dispatch("getProfileVaults", this.$route.params.profileId);
    },
  },
  mounted() {
    this.$store.dispatch("getProfileKeeps", this.$route.params.profileId);
    this.$store.dispatch("getSearchedProfile", this.$route.params.profileId);
    this.$store.dispatch("getProfileVaults", this.$route.params.profileId);
  },
  methods: {
    createKeep() {
      this.creatingKeep = false;
      this.$store.dispatch("createKeep", this.newKeep);
    },
    createVault() {
      this.creatingVault = false;
      this.$store.dispatch("createVault", this.newVault);
    },
  },
  components: {
    keepComp,
    vaultComp,
    createKeep,
  },
};
</script>


<style scoped>
.pluslg {
  zoom: 2;
  align-self: center;
}
</style>