<template>
  <div>
    <div class="pop-up-container"></div>
    <div class="pop-up-form" v-if="!periodProp">
      <p>Period</p>
      <div class="form-container">
        <div class="form-group">
          <label for="txtName">Name Period</label>
          <input
            type="text"
            class="form-control"
            id="txtName"
            v-model="period.name"
          />
        </div>
        <div class="form-group">
          <label>Department</label>
          <select class="form-control" v-model="period.departmentID">
            <option v-for="dep in departments" :key="dep.id" :value="dep.id">
              {{ dep.name }}
            </option>
          </select>
        </div>
        <div class="form-row">
          <div class="form-group col">
            <label for="txtFromTime">From Time</label>
            <input
              class="form-control"
              id="txtFromTime"
              type="date"
              v-model="period.fromTime"
            />
          </div>
          <div class="form-group col">
            <label for="txtToTime">To Time</label>
            <input
              class="form-control"
              id="txtToTime"
              type="date"
              v-model="period.toTime"
            />
          </div>
        </div>
        <div class="form-group">
          <label for="txtQuota">Quota</label>
          <input
            class="form-control"
            id="txtQuota"
            type="number"
            v-model="period.quota"
          />
        </div>
        <div class="text-danger text-center">{{ createError }}</div>
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

    <!-- edit period -->
    <div class="pop-up-form" v-else>
      <p>Period</p>
      <div class="form-container">
        <div class="form-group">
          <label for="txtName">Name Period</label>
          <input
            type="text"
            class="form-control"
            id="txtName"
            v-model="periodData.name"
          />
        </div>
        <div class="form-group">
          <label>Department</label>
          <select
            class="form-control"
            v-model="periodData.departmentID"
            :disabled="periodProp"
          >
            <option v-for="dep in departments" :key="dep.id" :value="dep.id">
              {{ dep.name }}
            </option>
          </select>
        </div>
        <div class="form-row">
          <div class="form-group col">
            <label for="txtFromTime">From Time</label>
            <input
              class="form-control"
              id="txtFromTime"
              type="date"
              v-model="periodData.fromTime"
            />
          </div>
          <div class="form-group col">
            <label for="txtToTime">To Time</label>
            <input
              class="form-control"
              id="txtToTime"
              type="date"
              v-model="periodData.toTime"
            />
          </div>
        </div>
        <!-- edit -->
        <div class="form-row">
          <label class="mr-3">Reset Quota</label>
          <base-switch v-model="switches"></base-switch>
        </div>
        <div class="form-group">
          <label for="txtQuota">Quota</label>
          <input
            class="form-control"
            id="txtQuota"
            type="number"
            v-model="periodData.quota"
            :disabled="!switches"
          />
        </div>
        <!-- new -->
        <!-- <div class="form-row" v-if="!periodProp">
          <label class="mr-3">Reset Quota</label>
          <base-switch v-model="switches"></base-switch>
        </div> -->
        <!-- <div class="form-group" v-if="!periodProp">
          <label for="txtQuota">Quota</label>
          <input
            class="form-control"
            id="txtQuota"
            type="number"
            v-model="periodData.quota"
          />
        </div> -->
        <!--  -->
        <div class="text-danger text-center">{{ createError }}</div>
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
import { createPeriod, updatePeriod } from "../../services/PeriodService";
import { pipeDatetime } from "../../helper/MyTools";

export default {
  name: "period-form",

  async created() {
    const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
    const params = { userID: userInfo.id, all: true };
    await this.$store.dispatch("department/getDepartments", params);

    if (this.periodProp) {
      this.periodData.fromTime = pipeDatetime(this.periodData.fromTime);
      this.periodData.toTime = pipeDatetime(this.periodData.toTime);
    }
  },

  data() {
    return {
      period: {
        name: null,
        departmentID: null,
        fromTime: null,
        toTime: null,
        quota: null,
      },
      createError: "",
      periodData: { ...this.periodProp },
      switches: false,
    };
  },

  computed: {
    departments() {
      return this.$store.state.department.departments;
    },
  },

  props: {
    periodProp: {
      type: Object,
      default: null,
    },
  },

  methods: {
    closeForm() {
      this.$emit("input", false);
    },

    async save() {
      if (this.periodProp) {
        this.periodData.resetQuota = this.switches;

        const result = await updatePeriod(this.periodData);

        if (result.isSuccess) {
          const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
          const params_period = { companyID: userInfo.companyID };

          await this.$store.dispatch("period/getPeriods", params_period);
          await this.$store.dispatch("company/getCompany", userInfo.companyID);

          this.$emit("input", false);
        } else {
          this.createError = result.messageDetail;
        }
      } else {console.log(this.period)
        const result = await createPeriod(this.period);
console.log(result);
        if (result.isSuccess) {
          const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
          const params_period = { companyID: userInfo.companyID };

          await this.$store.dispatch("period/getPeriods", params_period);
          await this.$store.dispatch("company/getCompany", userInfo.companyID);

          this.$emit("input", false);
        } else {
          this.createError = result.messageDetail;
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
  width: 500px;
  height: 700px;
  background: #ffffff;
  border-radius: 15px;
  top: 3%;
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