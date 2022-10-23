<template>
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
        :data="companies"
      >
        <template v-slot:columns>
          <th>ID</th>
          <th>Name</th>
          <th>Wallet</th>
          <th>Phone number</th>
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
          <td class="budget">
            {{ row.item.wallet }}
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
              @click="selectedCompany(row.item.id)"
              >Detail</base-button
            >
            <!-- <base-button
              size="sm"
              type="danger"
              icon="fas fa-trash-alt"
              v-if="!row.item.isDelete"
              >Delete</base-button
            >
            <base-button
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

  <CompanyForm v-if="isOpenForm" @input="closeForm" :id="companyId" />
</template>

<script>
import CompanyForm from "../../components/forms/CompanyForm.vue";

export default {
  props: {
    type: {
      type: String,
    },
    title: String,
  },

  components: { CompanyForm },

  data() {
    return {
      pagination: {
        totalPage: null,
        currentPage: null,
      },
      isOpenForm: false,
      companyId: null,
    };
  },

  async created() {
    await this.$store.dispatch("company/getCompanies");

    const state = this.$store.state.company.companies;
    this.pagination.totalPage = state.totalPage;
    this.pagination.currentPage = state.currentPage;
  },

  computed: {
    companies() {
      return this.$store.state.company.companies.items;
    },
  },

  methods: {
    async changePage(item) {
      this.pagination.currentPage = item;

      // create params
      const params = {
        // name: this.searchValue.length === 0 ? null : this.searchValue,
        pageNumber: this.pagination.currentPage,
      };

      // call action
      await this.$store.dispatch("company/getCompanies", params);

      // paging
      const state = this.$store.state.company.companies;
      this.pagination.totalPage = state.totalPage;
      this.pagination.currentPage = state.currentPage;
    },
    openForm() {
      this.companyId = null;
      this.isOpenForm = true;
    },
    closeForm(value) {
      this.isOpenForm = value;
    },
    selectedCompany(id) {
      this.companyId = id;
      this.isOpenForm = true;
    },
  },
};
</script>