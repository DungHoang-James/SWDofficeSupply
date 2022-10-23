<template>
  <div class="card shadow mb-3 p-4" v-if="!isOpenShoppingTable">
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
    v-if="!isOpenShoppingTable"
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
            @click="openShoppingTable"
            >Add new</base-button
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
        :data="productInMenu"
      >
        <template v-slot:columns>
          <th>ID</th>
          <th>Name</th>
          <th>Price</th>
          <th>Quantity</th>
          <th>Status</th>
          <th class="col-action">Action</th>
        </template>

        <template v-slot:default="row">
          <th scope="row">
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.productID }}</span>
              </div>
            </div>
          </th>
          <td>
            <div class="media align-items-center">
              <a class="avatar rounded-circle mr-3">
                <img alt="Image placeholder" :src="row.item.product.imageUrl" />
              </a>
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  row.item.product.name
                }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <input
                  type="number"
                  class="form-control form-control-sm"
                  v-model="row.item.price"
                />
                <!-- <span class="name mb-0 text-sm">{{ row.item.price }}</span> -->
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <input
                  type="number"
                  class="form-control form-control-sm"
                  v-model="row.item.quantity"
                />
                <!-- <span class="name mb-0 text-sm">{{ row.item.quantity }}</span> -->
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
              @click="editProductInMenu(row.item)"
              >Edit</base-button
            >
            <base-button
              size="sm"
              type="danger"
              icon="fas fa-trash-alt"
              v-if="!row.item.isDelete"
              @click="deActivate(row.item)"
              >DeActivate</base-button
            >
            <base-button
              size="sm"
              type="default"
              outline="true"
              icon="fas fa-trash-alt"
              v-else
              @click="activate(row.item)"
              >Activate</base-button
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

  <!-- toast -->
  <div
    class="position-fixed bottom-0 right-0 p-3 mb-4 mr-3"
    style="z-index: 5; background: #b3b3cc; border-radius: 10px"
    v-if="showToast"
  >
    <div
      id="liveToast"
      class="toast hide"
      role="alert"
      aria-live="assertive"
      aria-atomic="true"
      data-delay="2000"
    >
      <div class="toast-header">
        <strong class="mr-auto">Notification</strong>
        <button
          type="button"
          class="ml-2 mb-1 close"
          data-dismiss="toast"
          aria-label="Close"
          @click="showToast = false"
        >
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="toast-body">{{ message }}</div>
    </div>
  </div>

  <ShoppingProductTable
    v-if="isOpenShoppingTable"
    @input="closeShoppingTable"
    :id="menuId"
    title="Product"
  />
</template>

<script>
import ShoppingProductTable from "../Tables/ShoppingProductTable.vue";
import { updateProductInMenu } from "../../services/MenuService";

export default {
  props: {
    type: {
      type: String,
    },
    title: String,
    id: {
      type: Number,
      default: null,
    },
  },

  components: {
    ShoppingProductTable,
  },

  async created() {
    const params = {
      id: this.id,
    };
    await this.$store.dispatch("menu/getProductInMenu", params);

    const productState = this.$store.state.menu.productInMenu;
    this.pagination.currentPage = productState.currentPage;
    this.pagination.totalPage = productState.totalPage;
  },

  data() {
    return {
      isOpenShoppingTable: false,
      menuId: this.id,
      showToast: false,
      message: null,
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
        id: this.id,
        queryString: {
          pageNumber: this.pagination.currentPage,
          name:
            this.searchValue.txtSearchName.length === 0
              ? null
              : this.searchValue.txtSearchName,
        },
      };
      await this.$store.dispatch("menu/getProductInMenu", params);

      const productState = this.$store.state.menu.productInMenu;
      this.pagination.currentPage = productState.currentPage;
      this.pagination.totalPage = productState.totalPage;
    },

    async search() {
      // create params
      const params = {
        id: this.id,
        queryString: {
          name:
            this.searchValue.txtSearchName.length === 0
              ? null
              : this.searchValue.txtSearchName,
        },
      };

      // call action
      await this.$store.dispatch("menu/getProductInMenu", params);

      // paging
      const productState = this.$store.state.menu.productInMenu;
      this.pagination.currentPage = productState.currentPage;
      this.pagination.totalPage = productState.totalPage;
    },

    openShoppingTable() {
      this.isOpenShoppingTable = true;
    },
    closeShoppingTable(value) {
      this.isOpenShoppingTable = value;
    },
    async editProductInMenu(pro) {
      const spData = {
        menuId: pro.menuID,
        data: {
          productID: pro.productID,
          quantity: pro.quantity,
          price: pro.price,
          isDelete: false,
        },
      };

      const result = await updateProductInMenu(spData);

      if (result.isSuccess) {
        this.showToast = true;
        this.message = "Update product in menu success";

        const params = {
          id: this.id,
        };
        await this.$store.dispatch("menu/getProductInMenu", params);

        const productState = this.$store.state.menu.productInMenu;
        this.pagination.currentPage = productState.currentPage;
        this.pagination.totalPage = productState.totalPage;
      } else {
        this.showToast = true;
        this.message = result.messageDetail;
      }
    },

    async deActivate(pro) {
      const spData = {
        menuId: pro.menuID,
        data: {
          productID: pro.productID,
          quantity: pro.quantity,
          price: pro.price,
          isDelete: true,
        },
      };

      const result = await updateProductInMenu(spData);

      if (result.isSuccess) {
        this.showToast = true;
        this.message = "Update product in menu success";

        const params = {
          id: this.id,
        };
        await this.$store.dispatch("menu/getProductInMenu", params);

        const productState = this.$store.state.menu.productInMenu;
        this.pagination.currentPage = productState.currentPage;
        this.pagination.totalPage = productState.totalPage;
      } else {
        this.showToast = true;
        this.message = result.messageDetail;
      }
    },

    async activate(pro) {
      const spData = {
        menuId: pro.menuID,
        data: {
          productID: pro.productID,
          quantity: pro.quantity,
          price: pro.price,
          isDelete: false,
        },
      };

      const result = await updateProductInMenu(spData);

      if (result.isSuccess) {
        this.showToast = true;
        this.message = "Update product in menu success";

        const params = {
          id: this.id,
        };
        await this.$store.dispatch("menu/getProductInMenu", params);

        const productState = this.$store.state.menu.productInMenu;
        this.pagination.currentPage = productState.currentPage;
        this.pagination.totalPage = productState.totalPage;
      } else {
        this.showToast = true;
        this.message = result.messageDetail;
      }
    },
  },

  computed: {
    productInMenu() {
      return this.$store.state.menu.productInMenu.items;
    },
  },
};
</script>