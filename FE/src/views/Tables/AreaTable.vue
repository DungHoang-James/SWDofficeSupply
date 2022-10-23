<template>
  <!-- Card stats -->
  <!-- <div class="row form-container">
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
        <div class="btn btn-primary" @click="searchCategory">
          <i class="fas fa-search"></i> Search
        </div>
      </div>
    </div>
  </div> -->

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
        :data="areas"
      >
        <template v-slot:columns>
          <th>ID</th>
          <th>Name</th>
          <th>Menu</th>
          <th>Number of company</th>
          <th>Location</th>
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
                  getMenuName(row.item.menuID)
                }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  getNumComOfArea(row.item.id)
                }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.location }}</span>
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
              @click="selectedArea(row.item.id)"
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

  <AreaForm
    v-if="isOpenForm"
    @input="closeForm"
    :id="areaId"
    :allowDelete="getNumComOfArea(areaId) > 0 ? false : true"
  />
</template>

<script>
import AreaForm from "../../components/forms/AreaForm.vue";

export default {
  props: {
    type: {
      type: String,
    },
    title: String,
  },

  data() {
    return {
      isOpenForm: false,
      areaId: null,
      pagination: {
        currentPage: null,
        totalPage: null,
      },
    };
  },

  components: { AreaForm },

  async created() {
    const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));

    const params = {
      adminID: userInfo.id,
      all: true,
    };

    await this.$store.dispatch("menu/getMenusMin", params);
    await this.$store.dispatch("area/getAreas");
    await this.$store.dispatch("statistics/getAreaStatistics");

    const areaState = this.$store.state.area.areas;
    this.pagination.currentPage = areaState.currentPage;
    this.pagination.totalPage = areaState.totalPage;
  },

  methods: {
    async changePage(item) {
      this.pagination.currentPage = item;

      const params = {
        pageNumber: this.pagination.currentPage,
      };

      await this.$store.dispatch("area/getAreas", params);

      const areaState = this.$store.state.area.areas;
      this.pagination.currentPage = areaState.currentPage;
      this.pagination.totalPage = areaState.totalPage;
    },

    openForm() {
      this.areaId = null;
      this.isOpenForm = true;
    },

    closeForm(value) {
      this.isOpenForm = value;
    },
    selectedArea(id) {
      this.areaId = id;
      this.isOpenForm = true;
    },
    getMenuName(id) {
      return this.menusMin.find((m) => m.id === id).name;
    },
    getNumComOfArea(id) {
      const result = this.areaStatistics.find((a) => a.id === id);
      if (!result) {
        return 1;
      }

      return result.numOfCompany;
    },
  },

  computed: {
    areas() {
      return this.$store.state.area.areas.items;
    },
    menusMin() {
      return this.$store.state.menu.menusMin;
    },
    areaStatistics() {
      return this.$store.state.statistics.areaStatistics;
    },
  },
};
</script>