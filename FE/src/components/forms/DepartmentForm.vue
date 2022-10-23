<template>
  <div>
    <div class="pop-up-container"></div>
    <div class="pop-up-form">
      <p>Department</p>
      <div class="form-container">
        <div class="form-group">
          <label for="txtName">Name Department</label>
          <input
            type="text"
            class="form-control"
            id="txtName"
            v-model="department.name"
          />
        </div>
        <div class="form-group">
          <label for="txtPhoneNumber">Phone number</label>
          <input
            class="form-control"
            id="txtPhoneNumber"
            v-model="department.phoneNumber"
          />
        </div>
        <div class="form-group">
          <label for="txtEmail">Email</label>
          <input
            class="form-control"
            id="txtEmail"
            v-model="department.email"
          />
        </div>
        <div class="form-row" v-if="allowDelete">
          <label class="mr-3">Deleted:</label>
          <base-switch
            v-model="department.isDelete"
            :value="department.isDelete"
          ></base-switch>
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
    </div>
  </div>
</template>

<script>
import {
  createDepartment,
  updateDepartment,
} from "../../services/DepartmentService";
export default {
  name: "department-form",

  async created() {
    if (this.departmentId) {
      await this.$store.dispatch("department/getDepartment", this.departmentId);
    }
  },

  computed: {
    department() {
      if (this.departmentId) {
        return this.$store.state.department.department;
      } else {
        return {
          id: null,
          name: null,
          phoneNumber: null,
          email: null,
          companyID: null,
          isDelete: null,
        };
      }
    },
  },

  props: {
    departmentId: {
      type: Number,
      default: null,
    },
    allowDelete: {
      type: Boolean,
      default: null,
    },
  },

  data() {
    return {
      // allowDelete: null,
    };
  },

  methods: {
    closeForm() {
      this.$emit("input", false);
    },

    async save() {
      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
      this.department.companyID = userInfo.companyID;

      // const params = {
      //   userID: userInfo.id,
      // };

      if (this.departmentId) {
        await updateDepartment(this.department);
      } else {
        await createDepartment(this.department);
      }

      this.$emit("input", false);
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
  width: 500px;
  height: 600px;
  background: #ffffff;
  border-radius: 15px;
  top: 5%;
  left: 35%;
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
  width: 500px;
}

.button-container {
  width: 26.5rem;
  margin: 0 auto;
  margin-top: 50px;
}
</style>