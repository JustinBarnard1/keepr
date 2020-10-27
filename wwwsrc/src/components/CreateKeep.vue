<template>
  <div class="create-keep">
    <div class="card">
      <h2>Create A New Keep</h2>
      <form @submit.prevent="createKeep, (creatingKeep = false)">
        <p>
          Name: <input type="text" v-model="newKeep.Name" required />
          <br />
          Description:
          <input type="text" v-model="newKeep.Description" required />
          <br />
          Image URL: <input type="text" v-model="newKeep.Img" required />
          <br />
          Private: <input type="checkbox" v-model="newKeep.isPrivate" />
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
</template>

<script>
export default {
  name: "createKeep",
  data() {
    return {
      newKeep: {
        Name: "",
        Description: "",
        Img: "",
        isPrivate: false,
      },
    };
  },
  methods: {
    createKeep() {
      this.$store.dispatch("createKeep", this.newKeep);
    },
  },
  computed: {},
};
</script>