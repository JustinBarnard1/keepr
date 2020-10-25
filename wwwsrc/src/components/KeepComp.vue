<template>
  <div class="keep-comp card col-3 justify-content-center m-3">
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
  </div>
</template>


<script>
export default {
  name: "keep-comp",
  data() {
    return {};
  },
  computed: {
    profile() {
      return this.$store.state.profile;
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
  },
  props: ["keepProp"],
  components: {},
};
</script>


<style scoped>
</style>