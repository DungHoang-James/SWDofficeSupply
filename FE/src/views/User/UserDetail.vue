<template>
  <div>
    <!-- <div class="container-fluid mt-4"> -->
      <div class="row">
        <div class="col-xl-4 order-xl-2 mb-5 mb-xl-0">
          <div class="card card-profile shadow">
            <div class="row justify-content-center">
              <div class="col-lg-3 order-lg-2">
                <div class="card-profile-image">
                  <a>
                    <img
                      :src="user.avatarUrl"
                      class="rounded-circle"
                      style="width: 200px; height: 200px"
                    />
                  </a>
                </div>
              </div>
            </div>
            <div
              class="card-header text-center border-0 pt-8 pt-md-4 pb-0 pb-md-4"
            ></div>
            <div class="card-body pt-0 pt-md-4">
              <div class="row">
                <div class="col">
                  <div
                    class="
                      card-profile-stats
                      d-flex
                      justify-content-center
                      mt-md-5
                    "
                  ></div>
                </div>
              </div>
              <div class="text-center">
                <h3>{{ user.firstname }} {{ user.lastname }}</h3>
                <div class="h5 font-weight-300">
                  <i class="fas fa-location-arrow mr-2"></i
                  >{{ user.address ?? "None" }}
                </div>
                <div class="h5 mt-4">
                  <i class="fas fa-user-tie mr-2"></i
                  >{{ getRole(user.roleID) ?? "None" }} -
                  {{ department.name ?? "None" }}
                </div>
                <div>
                  <i class="ni ni-building mr-2"></i
                  >{{ company.name ?? "None" }}
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="col-xl-8 order-xl-1">
          <card shadow type="secondary">
            <template v-slot:header>
              <div class="bg-white border-0">
                <div class="row align-items-center">
                  <div class="col-8">
                    <h3 class="mb-0">
                      {{ user.firstname }} {{ user.lastname }}
                    </h3>
                  </div>
                  <div class="col-4 text-right">
                    <a class="btn btn-sm btn-primary" @click="closeDetail"
                      >Back</a
                    >
                  </div>
                </div>
              </div>
            </template>

            <form>
              <h6 class="heading-small text-muted mb-4">User information</h6>
              <div class="pl-lg-4">
                <div class="row">
                  <div class="col-lg-6">
                    <div class="form-group">
                      <label>First Name</label>
                      <input
                        type="text"
                        readonly
                        class="form-control"
                        v-model="user.firstname"
                      />
                    </div>
                  </div>
                  <div class="col-lg-6">
                    <div class="form-group">
                      <label>Last Name</label>
                      <input
                        type="text"
                        readonly
                        class="form-control"
                        v-model="user.lastname"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-lg-6">
                    <div class="form-group">
                      <label>Date of birth</label>
                      <input
                        type="text"
                        readonly
                        class="form-control"
                        v-model="user.dateOfBirth"
                      />
                    </div>
                  </div>
                  <div class="col-lg-6">
                    <div class="form-group">
                      <label>Gender</label>
                      <input
                        type="text"
                        readonly
                        class="form-control"
                        :value="getGender(user.isMale)"
                      />
                    </div>
                  </div>
                </div>
              </div>
              <hr class="my-4" />
              <!-- Address -->
              <h6 class="heading-small text-muted mb-4">Contact information</h6>
              <div class="pl-lg-4">
                <div class="row">
                  <div class="col-lg-6">
                    <div class="form-group">
                      <label>Email</label>
                      <input
                        type="text"
                        readonly
                        class="form-control"
                        v-model="user.email"
                      />
                    </div>
                  </div>
                  <div class="col-lg-6">
                    <div class="form-group">
                      <label>Phone number</label>
                      <input
                        type="text"
                        readonly
                        class="form-control"
                        v-model="user.phoneNumber"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-12">
                    <div class="form-group">
                      <label>Address</label>
                      <input
                        type="text"
                        readonly
                        class="form-control"
                        v-model="user.address"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </form>
          </card>
        </div>
      </div>
    <!-- </div> -->
  </div>
</template>

<script>
import { EMPLOYEE, LEADER, MANAGER } from "../../data/constants/AppConstant";

export default {
  name: "user-profile",

  async created() {
    await this.$store.dispatch("user/getUserPicked", this.id);
    this.user = { ...this.userData };

    // get company
    if (this.user.companyID) {
      await this.$store.dispatch("company/getCompany", this.user.companyID);
      this.company = { ...this.companyData };
    }

    // get department
    if (this.user.departmentID) {
      await this.$store.dispatch(
        "department/getDepartment",
        this.user.departmentID
      );
      this.department = { ...this.departmentData };
    }
    console.log(this.$store.state.user.user);
  },

  data() {
    return {
      EMPLOYEE,
      LEADER,
      MANAGER,
      user: {
        address: null,
        avatarUrl: null,
        companyID: null,
        dateOfBirth: null,
        departmentID: null,
        email: null,
        firstname: null,
        id: null,
        isDelete: null,
        isMale: null,
        lastname: null,
        menuID: null,
        phoneNumber: null,
        roleID: null,
      },
      company: {
        address: null,
        areaID: null,
        email: null,
        id: null,
        isDelete: null,
        managerID: null,
        name: null,
        phoneNumber: null,
        wallet: null,
      },
      department: {
        id: null,
        name: null,
        phoneNumber: null,
        email: null,
        companyID: null,
        isDelete: null,
      },
    };
  },

  methods: {
    getRole(roleId) {
      if (roleId === EMPLOYEE) {
        return "Employee";
      } else if (roleId === LEADER) {
        return "Leader";
      } else if (roleId === MANAGER) {
        return "Manager";
      }
    },

    getGender(isMale) {
      if (isMale) {
        return "Male";
      } else {
        return "Female";
      }
    },

    closeDetail() {
      this.$emit("input", false);
    },
  },

  props: {
    id: {
      type: Number,
      default: null,
    },
  },

  computed: {
    userData() {
      return this.$store.state.user.userPicked;
    },
    companyData() {
      return this.$store.state.company.company;
    },
    departmentData() {
      return this.$store.state.department.department;
    },
  },
};
</script>
<style></style>
