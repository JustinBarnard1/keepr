import Vue from "vue";
import Vuex from "vuex";
import { api } from "../services/AxiosService.js";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    profile: {},
    keeps: [],
    profileKeeps: [],
    searchedProfile: {},
    profileVaults: []
  },
  mutations: {
    setProfile(state, profile) {
      state.profile = profile;
    },
    setKeeps(state, keeps) {
      state.keeps = keeps;
    },
    setSearchedProfile(state, profile) {
      state.searchedProfile = profile;
    },
    setProfileKeeps(state, keeps) {
      state.profileKeeps = keeps;
    },
    setProfileVaults(state, vaults) {
      state.profileVaults = vaults;
    }
  },
  actions: {
    async getProfile({ commit }) {
      try {
        let res = await api.get("profiles");
        commit("setProfile", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getSearchedProfile({ commit, dispatch }, profileId) {
      try {
        let res = await api.get("profiles/" + profileId)
        commit("setSearchedProfile", res.data)
      } catch (e) {
        console.error(e)
      }
    },
    async getKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps")
        commit("setKeeps", res.data)
      } catch (e) {
        console.error(e)
      }
    },
    async createKeep({ commit, dispatch }, keepData) {
      try {
        let res = await api.post("keeps", keepData)
        dispatch("getKeeps")
      } catch (e) {
        console.error(e)
      }
    },
    async EditKeep({ commit, dispatch }, keepData) {
      try {
        let res = await api.put("keeps", keepData)
        dispatch("getKeeps")
      } catch (e) {
        console.error(e)
      }
    },
    async deleteKeep({ commit, dispatch }, keepId) {
      try {
        await api.delete("keeps/" + keepId)
        dispatch("getKeeps")
      } catch (e) {
        console.error(e)
      }
    },
    async getProfileKeeps({ commit, dispatch }, profileId) {
      try {
        let res = await api.get("profiles/" + profileId + "/keeps")
        commit("setProfileKeeps", res.data)
      } catch (e) {
        console.error(e)
      }
    },
    async getProfileVaults({ commit, dispatch }, profileId) {
      try {
        let res = await api.get("profiles/" + profileId + "/vaults")
        commit("setProfileVaults", res.data)
      } catch (e) {
        console.error(e)
      }
    }
  },
});
