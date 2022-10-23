<template>
  <div class="card shadow mb-5">
    <div class="py-3 px-4">
      <p class="font-weight-bold h2">Company</p>
      <div class="row my-1">
        <span class="font-weight-bold col-2">Name:</span
        ><span class="col-9">{{ company.name }}</span>
      </div>
      <div class="row my-1">
        <span class="font-weight-bold col-2">Email:</span
        ><span class="col-9">{{ company.email }}</span>
      </div>
      <div class="row my-1">
        <span class="font-weight-bold col-2">Phone Number:</span
        ><span class="col-9">{{ company.phoneNumber }}</span>
      </div>
      <div class="row my-1">
        <span class="font-weight-bold col-2">Address:</span
        ><span class="col-9">{{ company.address }}</span>
      </div>
      <div class="row my-1">
        <span class="font-weight-bold col-2">Wallet:</span
        ><span class="col-9">{{ company.wallet }} VNĐ</span>
      </div>
    </div>
  </div>

  <div class="card shadow mb-3 p-4">
    <div class="row">
      <div class="form-group col-5">
        <label for="pickDate">Department</label>
        <select class="form-control" v-model="depId">
          <option value="0">All</option>
          <option v-for="dep in departments" :key="dep.id" :value="dep.id">
            {{ dep.name }}
          </option>
        </select>
      </div>

      <div class="form-group col-2 d-flex flex-column justify-content-end">
        <button class="btn btn-primary" @click="search">Search</button>
      </div>
    </div>
  </div>

  <div class="card shadow" :class="type === 'dark' ? 'bg-default' : ''">
    <div
      class="card-header border-0"
      :class="type === 'dark' ? 'bg-transparent' : ''"
    >
      <div class="row align-items-center">
        <div class="col">
          <h3 class="mb-0" :class="type === 'dark' ? 'text-white' : ''">
            {{ title }}
          </h3>
        </div>
        <div class="col text-right">
          <base-button
            outline
            type="success"
            icon="fas fa-plus"
            @click="openForm"
            >Create</base-button
          >
        </div>
      </div>
    </div>

    <div class="table-responsive">
      <base-table
        class="table align-items-center table-flush"
        :class="type === 'dark' ? 'table-dark' : ''"
        :thead-classes="type === 'dark' ? 'thead-dark' : 'thead-light'"
        tbody-classes="list"
        :data="periodsData"
      >
        <template v-slot:columns>
          <th>Name</th>
          <th>Quota</th>
          <th>Remaining Quota</th>
          <th>Department</th>
          <th>From Time</th>
          <th>To Time</th>
          <th>Status</th>
          <th class="col-action">Action</th>
        </template>

        <template v-slot:default="row">
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.name }}</span>
              </div>
            </div>
          </td>
          <td class="budget">
            {{ row.item.quota }}
          </td>
          <td class="budget">
            {{ row.item.remainingQuota }}
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  row.item.department.name
                }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  displayDate(row.item.fromTime)
                }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  displayDate(row.item.toTime)
                }}</span>
              </div>
            </div>
          </td>
          <td>
            <badge class="badge-dot" :type="'danger'" v-if="row.item.isExpired">
              <i :class="`bg-danger`"></i>
              <span class="status">Expired</span>
            </badge>
            <badge class="badge-dot" :type="'success'" v-else>
              <i :class="`bg-success`"></i>
              <span class="status">Available</span>
            </badge>
          </td>
          <td class="col-action">
            <base-button
              size="sm"
              type="primary"
              icon="fas fa-pencil-alt"
              @click="selectedPeriod(row.item)"
              >Edit</base-button
            >
          </td>
        </template>
      </base-table>
    </div>

    <div
      class="card-footer d-flex justify-content-end"
      :class="type === 'dark' ? 'bg-transparent' : ''"
    >
      <base-pagination
        :pageCount="pagination.totalPage"
        :value="pagination.currentPage"
        @input="changePage"
      ></base-pagination>
    </div>
  </div>

  <PeriodForm v-if="isOpenForm" @input="closeForm" :periodProp="periodData" />
</template>
<script>
import { pipeDatetime } from "../../helper/MyTools";
import PeriodForm from "../../components/forms/PeriodForm.vue";
export default {
  name: "category-table",

  async created() {
    const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
    const params_period = { companyID: userInfo.companyID };

    const params = { userID: userInfo.id, all: true };
    await this.$store.dispatch("department/getDepartments", params);

    await this.$store.dispatch("period/getPeriods", params_period);

    const periodState = this.$store.state.period.periods;
    this.pagination.totalPage = periodState.totalPage;
    this.pagination.currentPage = periodState.currentPage;

    await this.$store.dispatch("company/getCompany", userInfo.companyID);
  },
  components: { PeriodForm },
  data() {
    return {
      pagination: {
        totalPage: null,
        currentPage: null,
      },
      isOpenForm: false,
      periodData: null,
      searchValue: "",
      depId: 0,
    };
  },
  props: {
    type: {
      type: String,
    },
    title: String,
  },
  computed: {
    periodsData() {
      return this.$store.state.period.periods.items;
    },
    company() {
      return this.$store.state.company.company;
    },
    departments() {
      return this.$store.state.department.departments;
    },
  },
  methods: {
    async changePage(item) {
      this.pagination.currentPage = item;

      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));

      // create params
      const params_period = {
        companyID: userInfo.companyID,
        pageNumber: this.pagination.currentPage,
        departmentID: this.depId == 0 ? null : this.depId,
      };

      // call action
      await this.$store.dispatch("period/getPeriods", params_period);

      // paging
      const periodState = this.$store.state.period.periods;
      this.pagination.totalPage = periodState.totalPage;
      this.pagination.currentPage = periodState.currentPage;
    },
    async search() {
      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
      console.log(this.depId);
      const params_period = {
        companyID: userInfo.companyID,
        departmentID: this.depId == 0 ? null : this.depId,
      };

      await this.$store.dispatch("period/getPeriods", params_period);

      const periodState = this.$store.state.period.periods;
      this.pagination.totalPage = periodState.totalPage;
      this.pagination.currentPage = periodState.currentPage;
    },
    openForm() {
      this.periodData = null;
      this.isOpenForm = true;
    },
    closeForm(value) {
      this.isOpenForm = value;
    },
    selectedPeriod(period) {
      this.periodData = period;
      this.isOpenForm = true;
    },
    displayDate(date) {
      return pipeDatetime(date);
    },
  },
};
</script>

<style scoped>
.col-action {
  width: 20rem;
}

.form-container {
  margin-bottom: 30px;
}
</style>