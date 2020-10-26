import Vue from "vue";
import VueRouter from "vue-router";
// @ts-ignore
import Home from "../pages/Home.vue";
import Profile from "../pages/Profile.vue";
import Vault from "../pages/Vault.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/profile/:profileId",
    name: "Profile",
    component: Profile,
  },
  {
    path: "/vaults/:vaultId",
    name: "Vault",
    component: Vault,
  }
];

const router = new VueRouter({
  routes,
});

export default router;
