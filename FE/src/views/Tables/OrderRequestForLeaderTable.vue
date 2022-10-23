<template>
  <div class="card shadow mb-4 p-3" v-if="!isOpenOrderDetail">
    <div class="row">
      <div class="form-group col">
        <label for="pickDate">From Time</label>
        <input
          type="date"
          class="form-control"
          id="pickDate"
          v-model="dateSearch.fromTime"
        />
      </div>

      <div class="form-group col">
        <label for="pickDate">To Time</label>
        <input
          type="date"
          class="form-control"
          id="pickDate"
          v-model="dateSearch.toTime"
        />
      </div>

      <div class="form-group col d-flex flex-column justify-content-end">
        <button class="btn btn-primary" @click="search">Search</button>
      </div>
    </div>
  </div>

  <div
    class="card shadow"
    :class="type === 'dark' ? 'bg-default' : ''"
    v-if="!isOpenOrderDetail"
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
        :data="orders"
      >
        <template v-slot:columns>
          <th>ID</th>
          <th>Create Time</th>
          <th>Approve Time</th>
          <th>Ordered By</th>
          <!-- <th>Approved By</th> -->
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
                <span class="name mb-0 text-sm">{{ row.item.createTime }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  row.item.approveTime
                }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm"
                  >{{ row.item.userOrder.firstname }}
                  {{ row.item.userOrder.lastname }}</span
                >
              </div>
            </div>
          </td>
          <!-- <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm"
                  >{{
                    row.item.userApprove ? row.item.userApprove.firstname : ""
                  }}
                  {{
                    row.item.userApprove ? row.item.userApprove.lastname : ""
                  }}</span
                >
              </div>
            </div>
          </td> -->
          <td>
            <badge
              class="badge-dot"
              :type="'info'"
              v-if="row.item.orderStatusID === WAITING"
            >
              <i :class="`bg-info`"></i>
              <span class="status">Waiting</span>
            </badge>
            <badge
              class="badge-dot"
              :type="'info'"
              v-if="row.item.orderStatusID === LEADER_APPROVE"
            >
              <i :class="`bg-info`"></i>
              <span class="status">Leader Approve</span>
            </badge>
            <badge
              class="badge-dot"
              :type="'danger'"
              v-if="row.item.orderStatusID === CANCEL"
            >
              <i :class="`bg-danger`"></i>
              <span class="status">Rejected</span>
            </badge>
            <badge
              class="badge-dot"
              :type="'success'"
              v-if="row.item.orderStatusID === MANAGER_APPROVE"
            >
              <i :class="`bg-success`"></i>
              <span class="status">Manager Accepted</span>
            </badge>
          </td>
          <td class="col-action">
            <base-button
              size="sm"
              type="primary"
              icon="ni ni-cart"
              @click="selectedOrder(row.item)"
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

  <!-- order detail -->
  <!-- info department -->
  <div class="card shadow mb-5" v-if="isOpenOrderDetail">
    <div class="py-3 px-4">
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

  <div
    class="card shadow"
    :class="type === 'dark' ? 'bg-default' : ''"
    v-if="isOpenOrderDetail"
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
        :data="orderDetail"
      >
        <template v-slot:columns>
          <!-- <th>ID</th> -->
          <th>Product</th>
          <th>Category</th>
          <th>Quantity</th>
          <th>Price</th>
          <!-- <th class="col-action">Action</th> -->
        </template>

        <template v-slot:default="row">
          <!-- <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  row.item.orderId
                }}</span>
              </div>
            </div>
          </td> -->
          <td>
            <div class="media align-items-center">
              <a class="avatar rounded-circle mr-3">
                <img
                  alt="Image placeholder"
                  :src="row.item.productInMenu.product.imageUrl"
                />
              </a>
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  row.item.productInMenu.product.name
                }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{
                  row.item.productInMenu.product.category.name
                }}</span>
              </div>
            </div>
          </td>
          <td>
            <div class="media align-items-center">
              <div class="media-body">
                <span class="name mb-0 text-sm">{{ row.item.quantity }}</span>
              </div>
            </div>
          </td>
          <td class="budget">{{ row.item.price }} VNĐ</td>
        </template>
      </base-table>
      <!-- table total -->
      <table class="table">
        <thead>
          <tr>
            <th scope="col"></th>
            <th scope="col"></th>
            <th scope="col"></th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td colspan="3" class="font-weight-bold h3 text-lg">Total price</td>
            <td class="text-lg font-weight-bold">{{ totalPrice }} VNĐ</td>
          </tr>
        </tbody>
      </table>
    </div>

    <div class="text-success font-weight-bold ml-3">{{ message }}</div>
    <div class="text-danger font-weight-bold ml-3">{{ errorMessage }}</div>
    <div
      class="card-footer d-flex justify-content-end"
      :class="type === 'dark' ? 'bg-transparent' : ''"
    >
      <button
        :disabled="isDone"
        class="btn btn-outline-danger"
        @click="rejectOrderRequest"
      >
        Reject
      </button>
      <button
        :disabled="isDone"
        class="btn btn-success"
        @click="acceptOrderRequest"
      >
        Accept
      </button>
    </div>
  </div>
</template>

<script>
import {
  LEADER_APPROVE,
  WAITING,
  CANCEL,
  MANAGER_APPROVE,
} from "../../data/constants/AppConstant";
import { updateOrder } from "../../services/OrderService";

export default {
  props: {
    type: {
      type: String,
    },
    title: String,
  },

  async created() {
    const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
    await this.$store.dispatch("order/getOrders", { userID: userInfo.id });

    const orderState = this.$store.state.order.orders;
    this.pagination.totalPage = orderState.totalPage;
    this.pagination.currentPage = orderState.currentPage;
  },

  computed: {
    orders() {
      return this.$store.state.order.orders.items;
    },
    orderDetail() {
      return this.$store.state.orderDetail.orderDetail;
    },
    currentPeriodOfDepartment() {
      return this.$store.state.period.currentPeriodOfDepartment;
    },
  },

  methods: {
    async changePage(item) {
      this.pagination.currentPage = item;

      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));

      const params = {
        userID: userInfo.id,
        pageNumber: this.pagination.currentPage,
        fromTime: this.dateSearch.fromTime,
        toTime: this.dateSearch.toTime,
      };

      await this.$store.dispatch("order/getOrders", params);

      const orderState = this.$store.state.order.orders;
      this.pagination.totalPage = orderState.totalPage;
      this.pagination.currentPage = orderState.currentPage;
    },

    async search() {
      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));

      const params = {
        userID: userInfo.id,
        pageNumber: this.pagination.currentPage,
        fromTime: this.dateSearch.fromTime,
        toTime: this.dateSearch.toTime,
      };

      await this.$store.dispatch("order/getOrders", params);

      const orderState = this.$store.state.order.orders;
      this.pagination.totalPage = orderState.totalPage;
      this.pagination.currentPage = orderState.currentPage;
    },

    async selectedOrder(item) {
      this.isDone = false;
      this.orderId = item.id;

      await this.$store.dispatch("orderDetail/getOrderDetail", item.id);
      await this.$store.dispatch(
        "period/getCurrentPeriodOfDepartment",
        item.userOrder.departmentID
      );

      this.calculateTotalPrice();

      if (
        item.orderStatusID === 2 ||
        item.orderStatusID === 3 ||
        item.orderStatusID === 4
      ) {
        this.isDone = true;
      }

      // open table
      this.isOpenOrderDetail = true;
    },

    async rejectOrderRequest() {
      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
      const data = {
        userApproveID: userInfo.id,
        orderID: this.orderId,
        isApprove: false,
        description: "",
      };

      const result = await updateOrder(data);

      if (result) {
        this.message = "Order status update successful";
        this.$router.push({ path: "/orderrequest" });
        this.isDone = true;
      } else {
        this.errorMessage = "Something wrong, try again or come back later";
      }
    },

    async acceptOrderRequest() {
      const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
      const data = {
        userApproveID: userInfo.id,
        orderID: this.orderId,
        isApprove: true,
        description: "",
      };

      const result = await updateOrder(data);

      if (result) {
        this.message = "Order status update successful";
        this.$router.push({ path: "/orderrequest" });
        this.isDone = true;
      } else {
        this.errorMessage = "Something wrong, try again or come back later";
      }
    },

    calculateTotalPrice() {
      this.totalPrice = 0;

      // calculate total price
      this.orderDetail.forEach((o) => {
        this.totalPrice += o.quantity * o.price;
      });
    },
  },

  data() {
    return {
      WAITING,
      LEADER_APPROVE,
      CANCEL,
      MANAGER_APPROVE,
      pagination: {
        totalPage: 1,
        currentPage: 1,
      },
      errorMessage: "",
      message: "",
      isOpenOrderDetail: false,
      isDone: false,
      totalPrice: 0,
      orderId: null,
      dateSearch: {
        fromTime: null,
        toTime: null,
      },
    };
  },
};
</script>

<style scoped>
.form-container {
  margin-bottom: 30px;
}
</style>