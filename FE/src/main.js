/*!

=========================================================
* Vue Argon Dashboard - v2.0.1
=========================================================

* Product Page: https://www.creative-tim.com/product/vue-argon-dashboard
* Copyright 2021 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/vue-argon-dashboard/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import ArgonDashboard from "./plugins/argon-dashboard";
import "element-plus/lib/theme-chalk/index.css";
import store from "./store";

import { initializeApp } from 'firebase/app'
// import "firebase/storage";

const firebaseConfig = {
    apiKey: "AIzaSyAOOcOUf9WmAAqu7uEiqqpAVZ92nfRJgZ8",
    authDomain: "testproject-1ec77.firebaseapp.com",
    projectId: "testproject-1ec77",
    storageBucket: "testproject-1ec77.appspot.com",
    messagingSenderId: "246356743472",
    appId: "1:246356743472:web:6016ec6e8dcc1ddc6064f5"
};

initializeApp(firebaseConfig);

const appInstance = createApp(App).use(store);
appInstance.use(router);
appInstance.use(ArgonDashboard);
appInstance.mount("#app");
