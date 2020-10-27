<template>
  <div
    v-bind:style="{
      backgroundImage: 'url(' + this.keepProp.img + ')',
      backgroundSize: 'cover',
      backgroundPosition: 'center center',
      backgroundRepeat: 'no-repeat',
    }"
    class="media m-2 keep-comp card"
    style="min-height: 20vh; min-width: 20vw"
    data-toggle="modal"
    :data-target="'#' + modalId"
    @click="setActiveKeep"
  >
    <div>
      <h3 class="text-center text-light">{{ keepProp.name }}</h3>
    </div>
    <div>
      <img
        style="max-width: 50px"
        :src="keepProp.creator.picture"
        @click="viewProfile"
        alt=""
      />
    </div>
    <details-modal class="my-modal" :id="modalId" color="bg-danger">
      <template v-slot:body>
        <keep-details />
      </template>
    </details-modal>
  </div>
</template>


<script>
import DetailsModal from "../components/DetailsModal";
import KeepDetails from "../components/KeepDetails";
export default {
  name: "keep-comp",
  data() {
    return {};
  },
  computed: {
    profile() {
      return this.$store.state.profile;
    },
    modalId() {
      return "modal" + this.keepProp.id;
    },
  },
  methods: {
    deleteKeep() {
      this.$store.dispatch("deleteKeep", this.keepProp.id);
    },
    viewProfile() {
      this.$router.push({
        name: "Profile",
        params: { profileId: this.keepProp.creator.id },
      });
    },
    setActiveKeep() {
      if (this.keepProp.id != this.$store.state.activeKeep.id) {
        this.$store.dispatch("getActiveKeep", this.keepProp.id);
      }
      this.$store.dispatch("getProfileVaults", this.profile.id);
    },
  },
  props: ["keepProp"],
  components: {
    DetailsModal,
    KeepDetails,
  },
};
</script>


<style>
.card-columns {
  column-count: 4;
}

div.modal.my-modal .modal-dialog {
  width: 700px;
}

.fillit {
  display: flex;
  flex-direction: column;
  flex-grow: 1;
}
</style>