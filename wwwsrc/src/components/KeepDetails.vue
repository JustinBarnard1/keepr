<template>
  <div class="keep-details">
    <div id="rightSize" class="card">
      <div class="row no-gutters">
        <div class="col-md-4">
          <img id="" :src="keep.img" class="card-img" alt="..." />
        </div>
        <div id="rightSide" class="col-md-8">
          <div class="card-body text-center mt-2 mb-3">
            <span>Views: {{ keep.views }} - Keeps: {{ keep.keeps }}</span>
            <h3 class="my-3 card-title text-center">{{ keep.name }}</h3>
            <span class="card-text">{{ keep.description }}</span>
          </div>
          <div class="d-flex align-items-stretch align-items-end">
            <div v-if="this.$route.name != 'Vault'" class="dropdown">
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
            <div v-else>
              <button
                @click="removeKeepFromVault(keep)"
                type="button"
                class="btn btn-danger"
              >
                Remove From Vault
              </button>
            </div>
            <div>
              <a
                v-if="$store.state.profile.id == keep.creatorId"
                class="mx-3 fa fa-trash text-danger plusSz"
                @click="deleteKeep"
              ></a>
              <a v-else class="mx-3 fa fa-trash text-gray plusSz"></a>
              <img
                v-if="keep.creator"
                style="max-width: 50px"
                :src="keep.creator.picture"
                alt=""
              />
              <div>
                <span v-if="this.keep.creator" class="ml-2"
                  >Created By: {{ this.keep.creator.name }}</span
                >
              </div>
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
    keeps() {
      return this.$store.state.keeps;
    },
  },
  methods: {
    addToVault(id) {
      let createPayload = {};
      createPayload.keepId = this.keep.id;
      createPayload.vaultId = id;
      this.$store.dispatch("addKeepToVault", createPayload);
      $(".modal-backdrop").hide();
      $(".modal").hide();
    },
    deleteKeep() {
      this.$store.dispatch("deleteKeep", this.keep.id);
      $(".modal-backdrop").hide();
      $(".modal").hide();
    },
    removeKeepFromVault(keep) {
      console.log(keep);
      let c = confirm("Are you sure you want to remove this?");
      if (c == true) {
        let payload = {};
        payload.keep = keep;
        payload.route = this.$route.params.vaultId;
        this.$store.dispatch("removeKeepFromVault", payload);
        $(".modal-backdrop").hide();
        $(".modal").hide();
      }
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
#keepImg {
  height: 60vh;
  width: 60vw;
}

#rightSide {
  height: 100%;
}
</style>