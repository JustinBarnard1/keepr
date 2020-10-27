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
    profileVaults: [],
    activeKeep: []
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
    },
    setActiveKeep(state, keep) {
      state.activeKeep = keep;
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
        dispatch("getProfileKeeps", res.data.creatorId)
      } catch (e) {
        console.error(e)
      }
    },
    async createVault({ commit, dispatch }, vaultData) {
      try {
        let res = await api.post("vaults", vaultData)
        dispatch("getProfileVaults", res.data.creatorId)
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
    async deleteVault({ commit, dispatch }, vaultId) {
      try {
        await api.delete("vaults/" + vaultId)
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
    },
    async getKeepsByVaultId({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId + "/keeps")
        commit("setKeeps", res.data)
      } catch (e) {
        console.error(e)
      }
    },
    async getActiveKeep({ commit, dispatch }, keepId) {
      try {
        let res = await api.get("keeps/" + keepId)
        commit("setActiveKeep", res.data)
      } catch (e) {
        console.error(e)
      }
    },
    async addKeepToVault({ commit, dispatch }, data) {
      let res = await api.post("vaultkeeps/", data)
    }
  },
});
