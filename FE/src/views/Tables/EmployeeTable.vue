<template>
  <div class="card shadow mb-3 p-4" v-if="!isOpenUserDetail">
    <div class="row">
      <div class="form-group col-5">
        <label>Name</label>
        <input type="text" class="form-control" v-model="searchValue" />
      </div>

      <div class="form-group col-2 d-flex flex-column justify-content-end">
        <button class="btn btn-primary" @click="search">Search</button>
      </div>
    </div>
  </div>

  <div
    class="card shadow"
    :class="type === 'dark' ? 'bg-default' : ''"
    v-if="!isOpenUserDetail"
  >
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
        <!-- <div class="col text-right">
          <base-button
            outline
            type="success"
            icon="fas fa-plus"
            @click="openForm"
            >Create</base-button
          >
        </div> -->
      </div>
    </div>

    <div class="table-responsive">
      <base-table
        class="table align-items-center table-flush"
        :class="type === 'dark' ? 'table-dark' : ''"
        :thead-classes="type === 'dark' ? 'thead-dark' : 'thead-light'"
        tbody-classes="list"
        :data="usersData"
      >
        <template v-slot:columns>
          <th>ID</th>
          <th>Name</th>
          <th>Email</th>
          <th>Phone Number</th>
          <th>Gender</th>
          <th>Birth Day</th>
          <th>Status</th>
          <th class="col-action">Action</th>
        </template>

        <template v-slot:default="row">
          <th scope="row">
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.id }}</span>
              </div>
            </div>
          </th>
          <td>
            <div class="media align-items-center">
              <a class="avatar rounded-circle mr-3">
                <img alt="Image placeholder" :src="row.item.avatarUrl" />
              </a>
              <div class="media-body">
                <span class="name mb-0 text-sm"
                  >{{ row.item.firstname }} {{ row.item.lastname }}</span
                >
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.email }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  row.item.phoneNumber
                }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center" v-if="row.item.isMale">
              <div class="media-body">
                <span class="name mb-0 text-sm">Male</span>
              </div>
            </div>
            <div class="media align-items-center" v-else>
              <div class="media-body">
                <span class="name mb-0 text-sm">Female</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  row.item.dateOfBirth
                }}</span>
              </div>
            </div>
          </td>
          <td>
            <badge class="badge-dot" :type="'danger'" v-if="row.item.isDelete">
              <i :class="`bg-danger`"></i>
              <span class="status">Delete</span>
            </badge>
            <badge class="badge-dot" :type="'success'" v-else>
              <i :class="`bg-success`"></i>
              <span class="status">Active</span>
            </badge>
          </td>
          <td class="col-action">
            <base-button
              size="sm"
              type="primary"
              icon="fas fa-pencil-alt"
              @click="selectedUser(row.item.id)"
              >Detail</base-button
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

  <UserDetail v-if="isOpenUserDetail" :id="userId" @input="closeForm" />
</template>

<script>
import UserDetail from "../User/UserDetail.vue";

export default {
  name: "category-table",

  async created() {
    const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
    const params = { id: userInfo.companyID, params: {} };

    await this.$store.dispatch("user/getUsersOfCompany", params);

    const userState = this.$store.state.user.usersOfCompany;
    this.pagination.totalPage = userState.totalPage;
    this.pagination.currentPage = userState.currentPage;
  },
  components: { UserDetail },
  data() {
    return {
      pagination: {
        totalPage: null,
        currentPage: null,
      },
      isOpenForm: false,
      isOpenUserDetail: false,
      userId: null,
      categoryId: null,
      searchValue: "",
    };
  },
  props: {
    type: {
      type: String,
    },
    title: String,
  },
  computed: {
    usersData() {
      return this.$store.state.user.usersOfCompany.items;
    },
  },
  methods: {
    async changePage(item) {
      this.pagination.currentPage = item;

      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));

      // create params
      const params = {
        id: userInfo.companyID,
        params: {
          pageNumber: this.pagination.currentPage,
          name: this.searchValue.length === 0 ? null : this.searchValue,
        },
      };

      // call action
      await this.$store.dispatch("user/getUsersOfCompany", params);

      // paging
      const userState = this.$store.state.user.usersOfCompany;
      this.pagination.totalPage = userState.totalPage;
      this.pagination.currentPage = userState.currentPage;
    },
    async search() {
      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
      const params = {
        id: userInfo.companyID,
        params: {
          name: this.searchValue.length === 0 ? null : this.searchValue,
        },
      };

      await this.$store.dispatch("user/getUsersOfCompany", params);

      const userState = this.$store.state.user.usersOfCompany;
      this.pagination.totalPage = userState.totalPage;
      this.pagination.currentPage = userState.currentPage;
    },
    openForm() {
      this.userId = null;
      this.isOpenUserDetail = true;
    },
    closeForm(value) {
      this.userId = null;
      this.isOpenUserDetail = value;
    },
    selectedUser(id) {
      this.userId = id;
      this.isOpenUserDetail = true;
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