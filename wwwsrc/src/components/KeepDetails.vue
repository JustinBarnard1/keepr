<template>
  <div class="keep-details">
    <div class="card mb-3" style="max-width: 700px">
      <div class="row no-gutters">
        <div class="col-md-4">
          <img :src="keep.img" class="card-img" alt="..." />
        </div>
        <div class="col-md-8">
          <div class="card-body text-center mt-2 mb-3">
            <span
              >Views: {{ keep.views }} - Keeps: {{ keep.keeps }} - Shares:
              {{ keep.shares }}</span
            >
            <h3 class="card-title text-center">{{ keep.name }}</h3>
            <p class="card-text">{{ keep.description }}</p>
          </div>
          <div class="d-flex">
            <div class="dropdown">
              <button
                class="ml-1 btn btn-secondary dropdown-toggle"
                type="button"
                id="dropdownMenuButton"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                Add To Vault
              </button>
              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                <a
                  v-for="vault in vaults"
                  :key="vault.id"
                  class="dropdown-item"
                  href="#"
                  @click="addToVault(vault.id)"
                  >{{ vault.name }}</a
                >
              </div>
            </div>
            <div>
              <a
                v-if="$store.state.profile.id == keep.creatorId"
                class="mx-3 fa fa-trash text-danger plusSz"
                @click="deleteKeep"
              ></a>
              <a v-else class="mx-3 fa fa-trash text-gray plusSz"></a>
            </div>
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
    vaults() {
      return this.$store.state.profileVaults;
    },
  },
  methods: {
    addToVault(id) {
      let createPayload = {};
      createPayload.keepId = this.keep.id;
      createPayload.vaultId = id;
      this.$store.dispatch("addKeepToVault", createPayload);
    },
    deleteKeep() {
      this.$store.dispatch("deleteKeep", this.keep.id);
    },
  },
  components: {},
};
</script>


<style scoped>
.plusSz {
  zoom: 2;
  align-self: center;
}
</style>