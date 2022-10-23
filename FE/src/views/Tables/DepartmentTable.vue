<template>
  <div class="card shadow mb-4 p-3" v-if="!isOpenDetail">
    <div class="row">
      <div class="form-group col-5">
        <label for="pickDate">Name</label>
        <input
          type="text"
          class="form-control"
          id="pickDate"
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
    v-if="!isOpenDetail"
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
        :data="departments"
      >
        <template v-slot:columns>
          <th>ID</th>
          <th>Name</th>
          <th>Phone number</th>
          <th>Email</th>
          <th>Number of employee</th>
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
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  getNumOfEmp(row.item.id)
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
              @click="selectedDepartment(row.item.id)"
              >Edit</base-button
            >
            <base-button
              size="sm"
              type="outline-primary"
              icon="fas fa-search"
              @click="openDetail(row.item.id)"
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

  <!-- info department -->
  <div class="card shadow mb-5" v-if="isOpenDetail">
    <div class="py-3 px-4">
      <p class="font-weight-bold h2">Department</p>
      <div class="row my-1">
        <span class="font-weight-bold col-2">Department:</span
        ><span class="col-9">{{ department.name }}</span>
      </div>
      <div class="row my-1">
        <span class="font-weight-bold col-2">Phone Number:</span
        ><span class="col-9">{{ department.phoneNumber }}</span>
      </div>
      <div class="row my-1">
        <span class="font-weight-bold col-2">Email:</span
        ><span class="col-9">{{ department.email }}</span>
      </div>
      <hr />
      <!-- quota -->
      <p class="font-weight-bold h2">Quota</p>
      <div class="row my-1">
        <span class="font-weight-bold col-2">Name:</span
        ><span class="col-9">{{ currentPeriodOfDepartment.name }}</span>
      </div>
      <div class="row my-1">
        <span class="font-weight-bold col-2">From:</span
        ><span class="col-9">{{ currentPeriodOfDepartment.fromTime }}</span>
      </div>
      <div class="row my-1">
        <span class="font-weight-bold col-2">To:</span
        ><span class="col-9">{{ currentPeriodOfDepartment.toTime }}</span>
      </div>
      <div class="row my-1">
        <span class="font-weight-bold col-2">Quota:</span
        ><span class="col-9 budget"
          >{{ currentPeriodOfDepartment.quota }} VNĐ</span
        >
      </div>
      <div class="row my-1">
        <span class="font-weight-bold col-2">Remaining Quota:</span
        ><span class="col-9 budget"
          >{{ currentPeriodOfDepartment.remainingQuota }} VNĐ</span
        >
      </div>
    </div>
  </div>

  <!-- detail each department -->
  <div
    class="card shadow"
    :class="type === 'dark' ? 'bg-default' : ''"
    v-if="isOpenDetail"
  >
    <div
      class="card-header border-0"
      :class="type === 'dark' ? 'bg-transparent' : ''"
    >
      <div class="row align-items-center">
        <div class="col">
          <h3 class="mb-0" :class="type === 'dark' ? 'text-white' : ''">
            Department: {{ department.name }}
          </h3>
        </div>
        <div class="col text-right">
          <base-button
            outline
            type="success"
            icon="fas fa-plus"
            @click="openNewMemberForm"
            >Add member</base-button
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
        :data="userInDepartment"
      >
        <template v-slot:columns>
          <th>ID</th>
          <th>Name</th>
          <th>Phone number</th>
          <th>Email</th>
          <th>Gender</th>
          <th>Position</th>
          <th>Status</th>
          <!-- <th class="col-action">Action</th> -->
        </template>

        <template v-slot:default="row">
          <th scope="row">
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.id }}</span>
              </div>
            </div>
          </th>
          <th scope="row">
            <div class="media align-items-center">
              <a href="#" class="avatar rounded-circle mr-3">
                <img alt="Image placeholder" :src="row.item.avatarUrl" />
              </a>
              <div class="media-body">
                <span class="name mb-0 text-sm"
                  >{{ row.item.firstname }} {{ row.item.lastname }}</span
                >
              </div>
            </div>
          </th>
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
            <div
              class="media align-items-center"
              v-if="row.item.roleID === MANAGER"
            >
              <div class="media-body">
                <span class="name mb-0 text-sm">Manager</span>
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
              v-if="row.item.roleID === EMPLOYEE"
            >
              <div class="media-body">
                <span class="name mb-0 text-sm">Employye</span>
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

  <DepartmentForm
    v-if="isOpenForm"
    @input="closeForm"
    :departmentId="departmentId"
    :allowDelete="getNumOfEmp(departmentId) > 0 ? false : true"
  />

  <!-- form add member -->
  <div v-if="isOpenNewMember">
    <div class="pop-up-container"></div>
    <div class="pop-up-form">
      <p>New member</p>
      <div class="form-container">
        <div class="form-group">
          <label for="txtEmail">Email</label>
          <input class="form-control" id="txtEmail" v-model="newMember.email" />
        </div>
        <div class="form-check">
          <input
            class="form-check-input"
            type="radio"
            id="rb1"
            value="3"
            v-model="roleId"
          />
          <label class="form-check-label" for="rb1"> Leader </label>
        </div>
        <div class="form-check">
          <input
            class="form-check-input"
            type="radio"
            id="rb2"
            value="4"
            v-model="roleId"
            checked
          />
          <label class="form-check-label" for="rb2"> Employee </label>
        </div>
        <div class="text-center text-danger">{{ errorAddNewMember }}</div>
        <div class="row button-container">
          <button
            type="button"
            class="btn btn-success col"
            @click="addNewMember"
          >
            Save
          </button>
          <button
            type="button"
            class="btn btn-outline-danger col"
            @click="closeNewMemberForm"
          >
            Cannel
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import DepartmentForm from "../../components/forms/DepartmentForm.vue";
import { MANAGER, LEADER, EMPLOYEE } from "../../data/constants/AppConstant";
import { addUserToDepartment } from "../../services/UserService";

export default {
  name: "category-table",
  components: { DepartmentForm },

  async created() {
    const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
    const params = {
      userID: userInfo.id,
    };

    await this.$store.dispatch("statistics/getDepartmentStatistics");
    console.log(this.$store.state.statistics.departmentStatistics);

    // call
    this.loadDepartments(params);
  },
  data() {
    return {
      pagination: {
        totalPage: null,
        currentPage: null,
      },
      newMember: {
        email: null,
        departmentID: null,
        roleID: null,
      },
      isOpenForm: false,
      departmentId: null,
      isOpenDetail: false,
      isOpenNewMember: false,
      roleId: null,
      MANAGER,
      LEADER,
      EMPLOYEE,
      errorAddNewMember: "",
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
    departments() {
      return this.$store.state.department.departments.items;
    },
    userInDepartment() {
      return this.$store.state.department.userInDepartment.items;
    },
    department() {
      return this.$store.state.department.department;
    },
    currentPeriodOfDepartment() {
      return this.$store.state.period.currentPeriodOfDepartment;
    },
    departmentStatistics() {
      return this.$store.state.statistics.departmentStatistics;
    },
  },
  methods: {
    async changePage(item) {
      this.pagination.currentPage = item;

      // create params
      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
      const params = {
        pageNumber: this.pagination.currentPage,
        userID: userInfo.id,
        name:
          this.searchValue.txtSearchName.length === 0
            ? null
            : this.searchValue.txtSearchName,
      };

      // call
      if (this.isOpenDetail) {
        const resource = {
          id: this.departmentId,
          params: {
            pageNumber: this.pagination.currentPage,
          },
        };
        this.loadUserInDepartment(resource);
      }
      this.loadDepartments(params);
    },

    async search() {
      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));

      const params = {
        userID: userInfo.id,
        name:
          this.searchValue.txtSearchName.length === 0
            ? null
            : this.searchValue.txtSearchName,
      };
      await this.loadDepartments(params);
    },

    getNumOfEmp(id) {
      const result = this.departmentStatistics.find((dep) => dep.id === id);
      if (!result) {
        return 1;
      }
      return result.numOfEmp;
    },

    openForm() {
      this.departmentId = null;
      this.isOpenForm = true;
    },
    closeForm(value) {
      this.isOpenForm = value;
      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
      const params = {
        pageNumber: this.pagination.currentPage,
        userID: userInfo.id,
      };
      this.loadDepartments(params);
    },
    selectedDepartment(id) {
      this.departmentId = id;
      this.isOpenForm = true;
    },
    async openDetail(id) {
      this.departmentId = id;
      const resource = { id: this.departmentId, params: {} };

      await this.$store.dispatch("department/getDepartment", this.departmentId);
      await this.$store.dispatch(
        "period/getCurrentPeriodOfDepartment",
        this.departmentId
      );

      this.loadUserInDepartment(resource);
      this.isOpenDetail = true;
    },
    async addNewMember() {
      this.newMember.departmentID = this.departmentId;
      this.newMember.roleID = this.roleId;

      const result = await addUserToDepartment(this.newMember);

      const resource = { id: this.departmentId, params: {} };

      if (result.isSuccess) {
        this.loadUserInDepartment(resource);
        this.isOpenNewMember = false;
      } else {
        this.errorAddNewMember = result.messageDetail;
      }
    },
    openNewMemberForm() {
      this.roleId = null;
      this.newMember.email = null;
      this.errorAddNewMember = "";
      this.isOpenNewMember = true;
    },
    closeNewMemberForm() {
      this.isOpenNewMember = false;
    },
    async loadDepartments(params) {
      // call action
      await this.$store.dispatch("department/getDepartments", params);

      // paging
      const departmentState = this.$store.state.department.departments;
      this.pagination.totalPage = departmentState.totalPage;
      this.pagination.currentPage = departmentState.currentPage;
    },
    async loadUserInDepartment(resource) {
      // call action
      await this.$store.dispatch("department/getUserInDepartment", resource);

      // pagin
      const uidepState = this.$store.state.department.userInDepartment;
      this.pagination.totalPage = uidepState.totalPage;
      this.pagination.currentPage = uidepState.currentPage;
    },
  },
};
</script>

<style scoped>
.col-action {
  width: 20rem;
}

/* .form-container {
  margin-bottom: 30px;
} */

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
  height: 600px;
  background: #ffffff;
  border-radius: 15px;
  top: 5%;
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