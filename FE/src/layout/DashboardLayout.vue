<template>
  <div class="wrapper" :class="{ 'nav-open': $sidebar.showSidebar }">
    <side-bar
      :background-color="sidebarBackground"
      short-title="Office Supply"
      title="Office Supply"
      v-if="user.roleID === ADMIN"
    >
      <template v-slot:links>
        <!-- <sidebar-item
          :link="{
            name: 'Dashboard',
            icon: 'ni ni-tv-2 text-primary',
            path: '/dashboard',
          }"
        /> -->

        <!-- <sidebar-item
          :link="{
            name: 'Icons',
            icon: 'ni ni-planet text-blue',
            path: '/icons',
          }"
        /> -->
        <!-- <sidebar-item
          :link="{
            name: 'Maps',
            icon: 'ni ni-pin-3 text-orange',
            path: '/maps',
          }"
        /> -->
        <!-- <sidebar-item
          :link="{
            name: 'User Profile',
            icon: 'ni ni-single-02 text-yellow',
            path: '/profile',
          }"
        /> -->
        <!-- <sidebar-item
          :link="{
            name: 'Tables',
            icon: 'ni ni-bullet-list-67 text-red',
            path: '/tables',
          }"
        /> -->
        <!-- <sidebar-item
          :link="{
            name: 'Login',
            icon: 'ni ni-key-25 text-info',
            path: '/login',
          }"
        />
        <sidebar-item
          :link="{
            name: 'Register',
            icon: 'ni ni-circle-08 text-pink',
            path: '/register',
          }"
        /> -->
        <!--  -->
        <sidebar-item
          :link="{
            name: 'Company',
            icon: 'ni ni-building text-orange',
            path: '/company',
          }"
        />
        <sidebar-item
          :link="{
            name: 'Product',
            icon: 'ni ni-app text-primary',
            path: '/product',
          }"
        />
        <sidebar-item
          :link="{
            name: 'Menu',
            icon: 'ni ni-collection text-red',
            path: '/menu',
          }"
        />
        <sidebar-item
          :link="{
            name: 'User',
            icon: 'ni ni-circle-08 text-green',
            path: '/user',
          }"
        />
        <sidebar-item
          :link="{
            name: 'Supplier',
            icon: 'fas fa-parachute-box text-orange',
            path: '/supplier',
          }"
        />
        <sidebar-item
          :link="{
            name: 'Area',
            icon: 'ni ni-pin-3 text-orange',
            path: '/area',
          }"
        />
        <sidebar-item
          :link="{
            name: 'Category',
            icon: 'ni ni-bullet-list-67 text-blue',
            path: '/category',
          }"
        />
      </template>
    </side-bar>

    <side-bar
      :background-color="sidebarBackground"
      short-title="Office Supply"
      title="Office Supply"
      v-if="user.roleID === LEADER"
    >
      <template v-slot:links>
        <sidebar-item
          :link="{
            name: 'Order Request',
            icon: 'fas fa-file-invoice-dollar text-primary',
            path: '/orderrequest',
          }"
        />
                <sidebar-item
          :link="{
            name: 'Staff',
            icon: 'fas fa-users text-blue',
            path: '/staff',
          }"
        />
      </template>
    </side-bar>

    <side-bar
      :background-color="sidebarBackground"
      short-title="Office Supply"
      title="Office Supply"
      v-if="user.roleID === MANAGER"
    >
      <template v-slot:links>
        <sidebar-item
          :link="{
            name: 'Order Request',
            icon: 'fas fa-file-invoice-dollar text-orange',
            path: '/orderrequest',
          }"
        />
        <sidebar-item
          :link="{
            name: 'Department',
            icon: 'fas fa-building text-primary',
            path: '/department',
          }"
        />
        <sidebar-item
          :link="{
            name: 'Period',
            icon: 'fas fa-file-invoice-dollar text-red',
            path: '/period',
          }"
        />
        <sidebar-item
          :link="{
            name: 'Employee',
            icon: 'fas fa-users text-blue',
            path: '/employee',
          }"
        />
      </template>
    </side-bar>

    <div class="main-content" :data="sidebarBackground">
      <dashboard-navbar></dashboard-navbar>

      <div @click="toggleSidebar">
        <!-- your content here -->
        <router-view></router-view>
        <!-- <content-footer v-if="!$route.meta.hideFooter"></content-footer> -->
      </div>
    </div>
  </div>
</template>
<script>
import { MANAGER, ADMIN, LEADER } from "../data/constants/AppConstant";
import DashboardNavbar from "./DashboardNavbar.vue";
// import ContentFooter from "./ContentFooter.vue";

export default {
  components: {
    DashboardNavbar,
  }, //ContentFooter,

  created() {
    this.user = JSON.parse(sessionStorage.getItem("userInfo"));
    // if (this.user.roleID === LEADER) {
    //   this.$router.push({ path: "/orderrequest" });
    // } else if (this.user.roleID == MANAGER) {
    //   this.$router.push({ path: "/department" });
    // }
  },
  data() {
    return {
      sidebarBackground: "vue", //vue|blue|orange|green|red|primary
      MANAGER,
      ADMIN,
      LEADER,
      user: null,
    };
  },
  methods: {
    toggleSidebar() {
      if (this.$sidebar.showSidebar) {
        this.$sidebar.displaySidebar(false);
      }
    },
  },
};
</script>