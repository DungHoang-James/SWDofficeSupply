<template>
  <div class="card shadow mb-3 p-4" v-if="!menuDetail">
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
    v-if="!menuDetail"
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
        :data="menusData"
      >
        <template v-slot:columns>
          <th>ID</th>
          <th>Name</th>
          <!-- <th>Status</th> -->
          <th class="col-action">Action</th>
        </template>

        <template v-slot:default="row">
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.id }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.name }}</span>
              </div>
            </div>
          </td>
          <!-- <td>
            <badge class="badge-dot" :type="'danger'" v-if="row.item.isDelete">
              <i :class="`bg-danger`"></i>
              <span class="status">Deleted</span>
            </badge>
            <badge class="badge-dot" :type="'success'" v-else>
              <i :class="`bg-success`"></i>
              <span class="status">Active</span>
            </badge>
          </td> -->
          <td class="col-action">
            <base-button
              size="sm"
              type="primary"
              icon="fas fa-pencil-alt"
              @click="selectedMenu(row.item.id)"
              >Edit</base-button
            >
            <base-button
              size="sm"
              type="primary"
              icon="fas fa-search"
              @click="detailMenu(row.item.id)"
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

  <MenuForm v-if="isOpenForm" @input="closeForm" :id="menuId" />
  <ProductInMenuTable v-if="menuDetail" :id="menuDetail" title="Product" />
</template>
<script>
import MenuForm from "../../components/forms/MenuForm.vue";
import ProductInMenuTable from "../Tables/ProductInMenuTable.vue";

export default {
  name: "category-table",

  async created() {
    const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
    const params = { adminID: userInfo.id };

    await this.$store.dispatch("menu/getMenus", params);

    const menuState = this.$store.state.menu.menus;
    this.pagination.totalPage = menuState.totalPage;
    this.pagination.currentPage = menuState.currentPage;
  },
  components: { MenuForm, ProductInMenuTable },
  data() {
    return {
      pagination: {
        totalPage: null,
        currentPage: null,
      },
      isOpenForm: false,
      menuId: null,
      menuDetail: null,
      searchValue: {
        txtSearchName: "",
      },
    };
  },
  props: {
    type: {
      type: String,
    },
    title: String,
  },
  computed: {
    menusData() {
      return this.$store.state.menu.menus.items;
    },
    company() {
      return this.$store.state.company.company;
    },
  },
  methods: {
    async changePage(item) {
      this.pagination.currentPage = item;

      // create params
      const params = {
        name:
          this.searchValue.txtSearchName.length === 0
            ? null
            : this.searchValue.txtSearchName,
        pageNumber: this.pagination.currentPage,
      };

      // call action
      await this.$store.dispatch("category/getCategoryAction", params);

      // paging
      const menuState = this.$store.state.menu.menus;
      this.pagination.totalPage = menuState.totalPage;
      this.pagination.currentPage = menuState.currentPage;
    },

    async search() {
      // create params
      const params = {
        name:
          this.searchValue.txtSearchName.length === 0
            ? null
            : this.searchValue.txtSearchName,
      };

      // call action
      await this.$store.dispatch("category/getCategoryAction", params);

      // paging
      const menuState = this.$store.state.menu.menus;
      this.pagination.totalPage = menuState.totalPage;
      this.pagination.currentPage = menuState.currentPage;
    },

    openForm() {
      this.menuId = null;
      this.isOpenForm = true;
    },
    closeForm(value) {
      this.isOpenForm = value;
    },
    selectedMenu(id) {
      this.menuId = id;
      this.isOpenForm = true;
    },
    detailMenu(id) {
      this.menuDetail = id;
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