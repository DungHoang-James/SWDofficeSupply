<template>
  <div>
    <div class="pop-up-container"></div>
    <div class="pop-up-form">
      <p>Company Infomation</p>
      <div class="d-flex flex-row">
        <div class="form-container">
          <div class="form-group">
            <label for="txtName">Name</label>
            <input
              type="text"
              class="form-control"
              id="txtName"
              v-model="company.name"
            />
          </div>
          <div class="form-group" v-if="company.id">
            <label for="txtWallet">Wallet</label>
            <input
              type="text"
              class="form-control"
              id="txtWallet"
              v-model="company.wallet"
            />
          </div>
          <div class="form-group">
            <label for="txtAddress">Address</label>
            <input
              class="form-control"
              id="txtAddress"
              v-model="company.address"
            />
          </div>
          <div class="form-group">
            <label for="txtPhoneNumber">Phone Number</label>
            <input
              class="form-control"
              id="txtPhoneNumber"
              v-model="company.phoneNumber"
            />
          </div>
          <div class="row button-container">
            <button type="button" class="btn btn-success col" @click="save">
              Save
            </button>
            <button
              type="button"
              class="btn btn-outline-danger col"
              @click="closeForm"
            >
              Cannel
            </button>
          </div>
        </div>

        <!-- form 2 -->
        <div class="form-container">
          <div class="form-group">
            <label for="txtEmail">Email</label>
            <input class="form-control" id="txtEmail" v-model="company.email" />
          </div>
          <div class="form-group">
            <label>Area</label>
            <select class="form-control" v-model="company.areaID">
              <option v-for="area in areasMin" :key="area.id" :value="area.id">
                {{ area.name }}
              </option>
            </select>
          </div>
          <div class="form-group" v-if="company.id">
            <label>Manager</label>
            <select class="form-control" v-model="company.managerID">
              <option v-for="user in usersMin" :key="user.id" :value="user.id">
                {{ user.firstname }} {{ user.lastname }} - {{ user.email }}
              </option>
            </select>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { createCompany, updateCompany } from "../../services/CompanyService";

export default {
  name: "company-form",
  async created() {
    const params = { all: true };
    await this.$store.dispatch("area/getAreasMin", params);

    if (this.id) {
      await this.$store.dispatch("company/getCompany", this.id);
      await this.$store.dispatch("user/getUsersMin", { all: true });

      this.company = { ...this.companyData };
    }
  },
  props: {
    id: {
      type: Number,
      default: null,
    },
  },
  data() {
    return {
      company: {
        id: null,
        name: null,
        wallet: null,
        address: null,
        phoneNumber: null,
        email: null,
        areaID: null,
        managerID: null,
        isDelete: null,
      },
    };
  },
  computed: {
    areasMin() {
      return this.$store.state.area.areasMin;
    },
    companyData() {
      return this.$store.state.company.company;
    },
    usersMin() {
      return this.$store.state.user.usersMin;
    },
  },
  methods: {
    closeForm() {
      this.$emit("input", false);
    },
    async save() {
      if (this.company.id) {
        const result = await updateCompany(this.company);
        if (result) {
          await this.$store.dispatch("company/getCompanies");

          this.$emit("input", false);
        }
      } else {
        const result = await createCompany(this.company);

        if (result.isSuccess) {
          await this.$store.dispatch("company/getCompanies");

          this.$emit("input", false);
        }
      }
    },
  },
};
</script>

<style scoped>
.pop-up-container {
  width: 100vw;
  height: 100vh;
  background: #5551515d;
  position: fixed;
  top: 0;
  left: 0;
  z-index: 3;
}
.pop-up-form {
  position: fixed;
  width: 1100px;
  height: 700px;
  background: #ffffff;
  border-radius: 15px;
  top: 3%;
  left: 20%;
  z-index: 4;
}

.pop-up-form p {
  text-align: center;
  margin-top: 30px;
  font-size: 30px;
  font-weight: bold;
  text-transform: uppercase;
}

.form-container {
  padding: 10px 30px 0 30px;
  width: 550px;
}

.button-container {
  width: 26.5rem;
  margin: 0 auto;
  margin-top: 50px;
}
</style>