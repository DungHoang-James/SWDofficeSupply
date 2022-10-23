<template>
  <!-- Card stats -->
  <div class="row form-container">
    <div class="col-xl-4 col-lg-6">
      <div class="form-group">
        <label for="searchName" class="text-white">Name</label>
        <input
          type="text"
          class="form-control"
          id="searchName"
          placeholder="Search..."
          v-model="searchValue"
        />
      </div>
      <div class="form-group">
        <!-- <base-button type="primary" icon="fas fa-search" @click="searchCategory"
          >Search</base-button
        > -->
        <div class="btn btn-primary" @click="searchCategory">
          <i class="fas fa-search"></i> Search
        </div>
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
        :data="categoryData"
      >
        <template v-slot:columns>
          <th>ID</th>
          <th>Name</th>
          <th>Number of product</th>
          <th>Status</th>
          <th class="col-action">Action</th>
        </template>

        <template v-slot:default="row">
          <th scope="row">
            <div class="media align-items-center">
              <!-- <a href="#" class="avatar rounded-circle mr-3">
                <img alt="Image placeholder" :src="row.item.img" />
              </a> -->
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.id }}</span>
              </div>
            </div>
          </th>
          <td>
            <div class="media align-items-center">
              <!-- <a href="#" class="avatar rounded-circle mr-3">
                <img alt="Image placeholder" :src="row.item.img" />
              </a> -->
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.name }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  getNumberOfProduct(row.item.id)
                }}</span>
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
          <!-- <td>
            <div class="avatar-group">
              <a
                href="#"
                class="avatar avatar-sm rounded-circle"
                data-toggle="tooltip"
                data-original-title="Ryan Tompson"
              >
                <img
                  alt="Image placeholder"
                  src="img/theme/team-1-800x800.jpg"
                />
              </a>
              <a
                href="#"
                class="avatar avatar-sm rounded-circle"
                data-toggle="tooltip"
                data-original-title="Romina Hadid"
              >
                <img
                  alt="Image placeholder"
                  src="img/theme/team-2-800x800.jpg"
                />
              </a>
              <a
                href="#"
                class="avatar avatar-sm rounded-circle"
                data-toggle="tooltip"
                data-original-title="Alexander Smith"
              >
                <img
                  alt="Image placeholder"
                  src="img/theme/team-3-800x800.jpg"
                />
              </a>
              <a
                href="#"
                class="avatar avatar-sm rounded-circle"
                data-toggle="tooltip"
                data-original-title="Jessica Doe"
              >
                <img
                  alt="Image placeholder"
                  src="img/theme/team-4-800x800.jpg"
                />
              </a>
            </div>
          </td> -->
          <td class="col-action">
            <base-button
              size="sm"
              type="primary"
              icon="fas fa-pencil-alt"
              @click="selectedCategory(row.item.id)"
              >Edit</base-button
            >
            <!-- <base-button
              size="sm"
              type="danger"
              icon="fas fa-trash-alt"
              v-if="!row.item.isDelete"
              @click="deleteCategory(row.item.id)"
              >InActive</base-button
            > -->
            <!-- getNumberOfProduct(row.item.id).numOfProduct === 0 -->
            <!-- <base-button
              size="sm"
              type="default"
              outline="true"
              icon="fas fa-trash-alt"
              v-else
              >InActive</base-button
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

  <CategoryForm
    v-if="isOpenForm"
    @input="closeForm"
    :id="categoryId"
    :allowDelete="getNumberOfProduct(categoryId) > 0 ? false : true"
  />
</template>
<script>
import CategoryForm from "../../components/forms/CategoryForm.vue";

export default {
  name: "category-table",
  components: { CategoryForm },
  async created() {
    await this.$store.dispatch("category/getCategoryAction");

    await this.$store.dispatch("statistics/getCategoryStatistics");

    // this.categoryStatistics = { ...this.categoryStatisticsData };

    const categoriesState = this.$store.state.category.categories;
    this.pagination.totalPage = categoriesState.totalPage;
    this.pagination.currentPage = categoriesState.currentPage;
  },
  data() {
    return {
      pagination: {
        totalPage: null,
        currentPage: null,
      },
      isOpenForm: false,
      categoryId: null,
      searchValue: "",
      categoryStatistics: null,
    };
  },
  props: {
    type: {
      type: String,
    },
    title: String,
  },
  computed: {
    categoryData() {
      return this.$store.state.category.categories.items;
    },
    categoryStatisticsData() {
      return this.$store.state.statistics.categoryStatistics;
    },
  },
  methods: {
    async changePage(item) {
      this.pagination.currentPage = item;

      // create params
      const params = {
        name: this.searchValue.length === 0 ? null : this.searchValue,
        pageNumber: this.pagination.currentPage,
      };

      // call action
      await this.$store.dispatch("category/getCategoryAction", params);

      // paging
      const categoriesState = this.$store.state.category.categories;
      this.pagination.totalPage = categoriesState.totalPage;
      this.pagination.currentPage = categoriesState.currentPage;
    },
    openForm() {
      this.categoryId = null;
      this.isOpenForm = true;
    },
    closeForm(value) {
      this.isOpenForm = value;
    },
    selectedCategory(id) {
      this.categoryId = id;
      this.isOpenForm = true;
    },
    async searchCategory() {
      // create params
      const params = {
        name: this.searchValue.length === 0 ? null : this.searchValue,
        pageNumber: 1,
      };

      // call action
      await this.$store.dispatch("category/getCategoryAction", params);

      // paging
      const categoriesState = this.$store.state.category.categories;
      this.pagination.totalPage = categoriesState.totalPage;
      this.pagination.currentPage = categoriesState.currentPage;
    },
    async deleteCategory(id) {
      await this.$store.dispatch("category/deleteCategory", id);
    },
    getNumberOfProduct(cateId) {
      const result = this.categoryStatisticsData.find((ct) => ct.id === cateId);
      if (!result) {
        return 1;
      }
      return result.numOfProduct;
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