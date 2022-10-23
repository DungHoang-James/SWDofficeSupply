<template>
  <div class="card shadow mb-3 p-4" v-if="!isOpenUserDetail">
    <div class="row">
      <div class="form-group col-5">
        <label>Name</label>
        <input
          type="text"
          class="form-control"
          v-model="searchValue.txtSearchName"
        />
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
        :data="users"
      >
        <template v-slot:columns>
          <th>ID</th>
          <th>Name</th>
          <th>Email</th>
          <th>Role</th>
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
            <div
              class="media align-items-center"
              v-if="row.item.roleID === VISITOR"
            >
              <div class="media-body">
                <span class="name mb-0 text-sm">Visitor</span>
              </div>
            </div>
            <div
              class="media align-items-center"
              v-if="row.item.roleID === EMPLOYEE"
            >
              <div class="media-body">
                <span class="name mb-0 text-sm">Employee</span>
              </div>
            </div>
            <div
              class="media align-items-center"
              v-if="row.item.roleID === LEADER"
            >
              <div class="media-body">
                <span class="name mb-0 text-sm">Leader</span>
              </div>
            </div>
            <div
              class="media align-items-center"
              v-if="row.item.roleID === MANAGER"
            >
              <div class="media-body">
                <span class="name mb-0 text-sm">Manger</span>
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
            <!-- <base-button
              size="sm"
              type="danger"
              icon="fas fa-trash-alt"
              v-if="!row.item.isDelete"
              >Delete</base-button
            > -->
            <!-- <base-button
              size="sm"
              type="default"
              outline="true"
              icon="fas fa-trash-alt"
              v-else
              >Delete</base-button
            > -->
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
import {
  VISITOR,
  EMPLOYEE,
  LEADER,
  MANAGER,
} from "../../data/constants/AppConstant";

export default {
  props: {
    type: {
      type: String,
    },
    title: String,
  },

  components: { UserDetail },

  async created() {
    await this.$store.dispatch("user/getUsers");

    const userState = this.$store.state.user.users;
    this.pagination.currentPage = userState.currentPage;
    this.pagination.totalPage = userState.totalPage;
  },

  data() {
    return {
      VISITOR,
      EMPLOYEE,
      LEADER,
      MANAGER,
      isOpenUserDetail: false,
      userId: null,
      pagination: {
        totalPage: null,
        currentPage: null,
      },
      searchValue: {
        txtSearchName: "",
      },
    };
  },

  methods: {
    async changePage(item) {
      this.pagination.currentPage = item;

      const params = {
        pageNumber: this.pagination.currentPage,
        name:
          this.searchValue.txtSearchName.length === 0
            ? null
            : this.searchValue.txtSearchName,
      };

      await this.$store.dispatch("user/getUsers", params);

      const userState = this.$store.state.user.users;
      this.pagination.currentPage = userState.currentPage;
      this.pagination.totalPage = userState.totalPage;
    },

    async search() {
      const params = {
        name:
          this.searchValue.txtSearchName.length === 0
            ? null
            : this.searchValue.txtSearchName,
      };

      await this.$store.dispatch("user/getUsers", params);

      const userState = this.$store.state.user.users;
      this.pagination.currentPage = userState.currentPage;
      this.pagination.totalPage = userState.totalPage;
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

  computed: {
    users() {
      return this.$store.state.user.users.items;
    },
  },
};
</script>