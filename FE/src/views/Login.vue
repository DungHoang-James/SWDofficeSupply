<template>
  <div class="row justify-content-center">
    <div class="col-lg-5 col-md-7">
      <div class="card bg-secondary shadow border-0">
        <div class="card-header bg-transparent pb-5">
          <div class="text-muted text-center mt-2 mb-3">
            <small>Sign in with</small>
          </div>
          <div class="btn-wrapper text-center">
            <a href="#" class="btn btn-neutral btn-icon">
              <span class="btn-inner--icon"
                ><img src="img/icons/common/github.svg"
              /></span>
              <span class="btn-inner--text">Github</span>
            </a>

            <!-- login with google -->
            <a class="btn btn-neutral btn-icon" @click="login">
              <span class="btn-inner--icon"
                ><img src="img/icons/common/google.svg"
              /></span>
              <span class="btn-inner--text">Google</span>
            </a>
          </div>
        </div>
        <div class="card-body px-lg-5 py-lg-5">
          <div class="text-center text-muted mb-4">
            <small>Or sign in with credentials</small>
          </div>
          <form role="form">
            <base-input
              formClasses="input-group-alternative mb-3"
              placeholder="Email"
              addon-left-icon="ni ni-email-83"
              v-model="model.email"
            >
            </base-input>

            <base-input
              formClasses="input-group-alternative mb-3"
              placeholder="Password"
              type="password"
              addon-left-icon="ni ni-lock-circle-open"
              v-model="model.password"
            >
            </base-input>

            <base-checkbox class="custom-control-alternative">
              <span class="text-muted">Remember me</span>
            </base-checkbox>
            <div class="text-center">
              <base-button type="primary" class="my-4">Sign in</base-button>
            </div>
          </form>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-6">
          <a href="#" class="text-light"><small>Forgot password?</small></a>
        </div>
        <div class="col-6 text-right">
          <router-link to="/register" class="text-light"
            ><small>Create new account</small></router-link
          >
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { getAuth, signInWithPopup, GoogleAuthProvider } from "firebase/auth";
import { getToken } from "../services/UserService";
import { LEADER, MANAGER, ADMIN } from "../data/constants/AppConstant";
export default {
  name: "login",
  created() {
    const jwt = sessionStorage.getItem("jwtToken");
    if (jwt) {
      this.$router.push({ path: "/main" });
    }
  },
  data() {
    return {
      model: {
        email: "",
        password: "",
      },
    };
  },
  methods: {
    async login() {
      const provider = new GoogleAuthProvider();

      const auth = getAuth();

      try {
        const userCredential = await signInWithPopup(auth, provider);
        const user = userCredential.user;
        const token = user.accessToken;
        console.log(token);

        const data = await getToken(token);
        sessionStorage.setItem("jwtToken", JSON.stringify(data));

        await this.$store.dispatch("user/getUser", data.id);

        const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));

        if (userInfo.roleID === LEADER) {
          this.$router.push({ path: "/orderrequest" });
        } else if (userInfo.roleID === MANAGER) {
          this.$router.push({ path: "/orderrequest" });
        } else if (userInfo.roleID === ADMIN) {
          this.$router.push({ path: "/main" });
        } else {
          this.$router.push({ path: "/visitor" });
        }
      } catch (error) {
        console.log(error);

        // const errorCode = error.code;
        // const errorMessage = error.message;

        // const email = error.email;

        // const credential = GoogleAuthProvider.credentialFromError(error);
      }
    },
  },
};
</script>
<style></style>
