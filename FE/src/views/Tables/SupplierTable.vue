<template>
  <div class="card shadow mb-3 p-4" v-if="!isOpenForm">
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
        :data="suppliers"
      >
        <template v-slot:columns>
          <th>ID</th>
          <th>Name</th>
          <th>Phone Number</th>
          <th>Email</th>
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
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.name }}</span>
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
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.email }}</span>
              </div>
            </div>
          </td>
          <td>
            <badge class="badge-dot" :type="'danger'" v-if="row.item.isDelete">
              <i :class="`bg-danger`"></i>
              <span class="status">InActive</span>
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
              @click="selectedSupplier(row.item.id)"
              >Edit</base-button
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

  <SupplierForm v-if="isOpenForm" @input="closeForm" :id="supplierId" />
</template>

<script>
import SupplierForm from "../../components/forms/SupplierForm.vue";

export default {
  props: {
    type: {
      type: String,
    },
    title: String,
  },

  components: { SupplierForm },

  async created() {
    await this.$store.dispatch("supplier/getSuppliers");

    const supplierState = this.$store.state.supplier.suppliers;
    this.pagination.currentPage = supplierState.currentPage;
    this.pagination.totalPage = supplierState.totalPage;
  },

  data() {
    return {
      isOpenForm: false,
      supplierId: null,
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

      await this.$store.dispatch("supplier/getSuppliers", params);

      const supplierState = this.$store.state.supplier.suppliers;
      this.pagination.currentPage = supplierState.currentPage;
      this.pagination.totalPage = supplierState.totalPage;
    },

    async search() {
      const params = {
        name:
          this.searchValue.txtSearchName.length === 0
            ? null
            : this.searchValue.txtSearchName,
      };

      await this.$store.dispatch("supplier/getSuppliers", params);

      const supplierState = this.$store.state.supplier.suppliers;
      this.pagination.currentPage = supplierState.currentPage;
      this.pagination.totalPage = supplierState.totalPage;
    },

    openForm() {
      this.supplierId = null;
      this.isOpenForm = true;
    },
    closeForm(value) {
      this.supplierId = null;
      this.isOpenForm = value;
    },
    selectedSupplier(id) {
      this.supplierId = id;
      this.isOpenForm = true;
    },
  },

  computed: {
    suppliers() {
      return this.$store.state.supplier.suppliers.items;
    },
  },
};
</script>