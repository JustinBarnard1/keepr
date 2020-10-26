<template>
  <div
    class="media m-1 keep-comp card col-3 justify-content-center"
    data-toggle="modal"
    :data-target="'#' + modalId"
    @click="setActiveKeep"
  >
    <i
      class="fa fa-times text-danger"
      v-if="profile.id == keepProp.creatorId"
      @click="deleteKeep"
      aria-hidden="true"
    ></i>
    <img :src="keepProp.img" alt="" />
    <h2>{{ keepProp.name }}</h2>
    <p>{{ keepProp.description }}</p>
    <img :src="keepProp.creator.picture" @click="viewProfile" alt="" />
    <details-modal :id="modalId" color="bg-danger">
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
      this.$store.dispatch("setActiveKeep", this.keepProp.id);
    },
  },
  props: ["keepProp"],
  components: {
    DetailsModal,
    KeepDetails,
  },
};
</script>


<style scoped>
</style>